using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SamMou.Api.DataContext;
using SamMou.Api.Services;
using SamMou.Api.Services.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SamMou.Api.GraphQL;
using SamMou.Api.Models;
using GraphQL.Utilities;
using GraphQL.EntityFramework;
using GraphQL;
using GraphQL.Types;

namespace SamMou.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();

            services.AddDbContext<SamMouDataContext>(
                options =>
                options.UseCosmos(Configuration.GetValue<string>("SamMouCosmos:Endpoint"), Configuration.GetValue<string>("SamMouCosmos:PrimaryKey"), Configuration.GetValue<string>("SamMouCosmos:DatabaseName"))
                .EnableSensitiveDataLogging(), ServiceLifetime.Scoped
              ) ;

            services.AddControllers();
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();

            // GraphQL            
            GraphTypeTypeRegistry.Register<WeatherForecast, SamMouGraph>();
            EfGraphQLConventions.RegisterInContainer(services, (context) => (context as GraphQLSamMouDataContext)?.Context);
            EfGraphQLConventions.RegisterConnectionTypesInContainer(services);
            services.AddSingleton<IDocumentExecuter, EfDocumentExecuter>();
            services.AddSingleton<GraphQLQuery>();
            services.AddSingleton<GraphQLSchema>();

            // Import also all the other created Graphs
            foreach (Type type in GetGraphQlTypes())
            {
                services.AddScoped(type);
            }

            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var test = Configuration.GetValue<string>("Test");

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SamMou Api");
            });

            app.UseGraphQLPlayground();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<SamMouDataContext>())
                {
                    context.Database.EnsureCreated();
                }
            }
        }

        static IEnumerable<Type> GetGraphQlTypes()
        {
            return typeof(Startup).Assembly
                .GetTypes()
                .Where(x => !x.IsAbstract &&
                            (typeof(IObjectGraphType).IsAssignableFrom(x) ||
                             typeof(IInputObjectGraphType).IsAssignableFrom(x) ||
                             typeof(ScalarGraphType).IsAssignableFrom(x)));
        }
    }
}
