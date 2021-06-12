using System;
using System.Collections.Generic;
using GraphQL;
using GraphQL.Types;
using SamMou.Api.Models;
using SamMou.Api.Services.Interface;

namespace SamMou.Api.GraphQL
{
    public class QueryObject : ObjectGraphType<object>
    {
        public QueryObject(IWeatherForecastService service)
        {
            try
            {
                Name = "Queries";
                Description = "The base query for all the entities in our object graph.";

                FieldAsync<WeatherForecastObject, WeatherForecast>(
                    "singleweatherforecast",
                    "Gets weatherforecast",
                    new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id",
                        Description = "The unique GUID of the movie."
                    }),
                    context => service.GetForecast(context.GetArgument("id", Guid.Empty)));

                FieldAsync<ListGraphType<WeatherForecastObject>, List<WeatherForecast>>(
                    "allweatherforecast",
                    "Gets weatherforecast",
                    null,
                    context => service.GetAllForecast());
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
