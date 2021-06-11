using System;
using SamMou.Api.DataContext;
using GraphQL.EntityFramework;

namespace SamMou.Api.GraphQL
{
    public class GraphQLQuery : QueryGraphType<SamMouDataContext>
    {
        public GraphQLQuery(IEfGraphQLService<SamMouDataContext> graphQlService) : base(graphQlService)
        {
            AddQueryField(name: "WeatherForecasts", resolve: ctx => ctx.DbContext.WeatherForecasts);
            AddSingleField(name: "WeatherForecast", resolve: ctx => ctx.DbContext.WeatherForecasts);
        }
    }
}
