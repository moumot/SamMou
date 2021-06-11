using System;
using GraphQL;
using GraphQL.EntityFramework;
using SamMou.Api.DataContext;
using SamMou.Api.Models;

namespace SamMou.Api.GraphQL
{
    [GraphQLMetadata("WeatherForecast")]
    public class SamMouGraph : EfObjectGraphType<SamMouDataContext, WeatherForecast>
    {
        public SamMouGraph(IEfGraphQLService<SamMouDataContext> graphQlService) : base(graphQlService)
        {
            AutoMap();
        }
    }
}
