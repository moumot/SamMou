using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamMou.Api.Models;

namespace SamMou.Api.Services.Interface
{
    public interface IWeatherForecastService
    {
        Task<List<WeatherForecast>> GetAllForecast();
        Task<bool> InsertForecast(WeatherForecast weatherForecast);
        Task<WeatherForecast> GetForecast(Guid id);
    }
}
