using GraphQL;
using GraphQL.Server;
using GraphQL.SystemTextJson;
using GraphQL.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SamMou.Api.DataContext;
using SamMou.Api.GraphQL;
using SamMou.Api.Services;
using SamMou.Api.Services.Interface;

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

            services.AddDbContext<SamMouDataContext>(
                options =>
                options.UseCosmos(Configuration.GetValue<string>("SamMouCosmos:Endpoint"), Configuration.GetValue<string>("SamMouCosmos:PrimaryKey"), Configuration.GetValue<string>("SamMouCosmos:DatabaseName"))
                .EnableSensitiveDataLogging(), ServiceLifetime.Scoped
              ) ;

            services.AddControllers();
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            services.AddScoped<QueryObject>();
            services.AddScoped<MutationObject>();
            services.AddScoped<GraphQLSchema>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            services.AddApplicationInsightsTelemetry();

            // GraphQL            
            services.AddGraphQL((options, provider) =>{
            // Log errors
            var logger = provider.GetRequiredService<ILogger<Startup>>();
                        options.UnhandledExceptionDelegate = ctx =>
                            logger.LogError("{Error} occurred", ctx.OriginalException.Message);
                    })
                .AddGraphTypes(typeof(GraphQLSchema))
                .AddDataLoader()
                .AddSystemTextJson();

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

            app.UseGraphQLAltair();

            app.UseGraphQL<GraphQLSchema>();

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
    }
}
