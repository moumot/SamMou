using System.Collections.Generic;
using GraphQL.Types;
using SamMou.Api.Models;

namespace SamMou.Api.GraphQL
{
    public sealed class WeatherForecastObject : ObjectGraphType<WeatherForecast>
    {
        public WeatherForecastObject() 
        {
            Name = nameof(WeatherForecast);
            Description = "A WeatherForecast in the collection";

            Field(m  => m.ID).Description("Identifier of the WeatherForecast");
            Field(m => m.Summary).Description("Summary of the WeatherForecast");
            Field(m => m.Date).Description("Date of the WeatherForecast");
            Field(m => m.TemperatureC).Description("Temperature C of the WeatherForecast");
        }
    }

    public sealed class WeatherForecastInputObject : InputObjectGraphType<WeatherForecast>
    {
        public WeatherForecastInputObject()
        {
            Name = "WeatherForecastInput";
            Description = "A WeatherForecastInput in the collection";

            Field(m => m.ID).Description("Identifier of the WeatherForecast");
            Field(m => m.Summary).Description("Summary of the WeatherForecast");
            Field(m => m.Date).Description("Date of the WeatherForecast");
            Field(m => m.TemperatureC).Description("Temperature C of the WeatherForecast");
        }
    }
}
