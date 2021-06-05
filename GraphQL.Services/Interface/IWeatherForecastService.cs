using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Models;

namespace GraphQL.Services.Interface
{
    public interface IWeatherForecastService
    {
        Task<List<WeatherForecast>> GetForecast();
    }
}
