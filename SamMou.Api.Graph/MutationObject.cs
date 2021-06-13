using System;
using GraphQL;
using GraphQL.Types;
using SamMou.Api.Models;
using SamMou.Api.Services.Interface;

namespace SamMou.Api.GraphQL
{
    public class MutationObject : ObjectGraphType<object>
    {
        public MutationObject(IWeatherForecastService service)
        {
            try
            {
                Name = "Mutations";
                Description = "The base mutation for all the entities in our object graph.";

                FieldAsync<WeatherForecastObject, WeatherForecast>(
                    "addWeatherForecast",
                    "Add weatherforecast",
                    arguments: new QueryArguments(
                        new QueryArgument<NonNullGraphType<WeatherForecastInputObject>>
                        {
                            Name = "Weatherforecast",
                            Description = "Weatherforecast input model"
                        }),
                    resolve: context =>
                    {
                        var weatherForecast = context.GetArgument<WeatherForecast>("Weatherforecast");
                        return service.InsertUpdateForecast(weatherForecast);
                    });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
