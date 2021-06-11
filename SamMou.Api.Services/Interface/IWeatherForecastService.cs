using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamMou.Api.Models;

namespace SamMou.Api.Services.Interface
{
    public interface IWeatherForecastService
    {
        Task<List<WeatherForecast>> GetForecast();
        Task<bool> InsertForecast(WeatherForecast weatherForecast);
    }
}
