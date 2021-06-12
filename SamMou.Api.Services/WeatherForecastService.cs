using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SamMou.Api.DataContext;
using SamMou.Api.Models;
using SamMou.Api.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace SamMou.Api.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly SamMouDataContext _context;
        public WeatherForecastService(SamMouDataContext context)
        {
            _context = context;
        }

        public async Task<List<WeatherForecast>> GetAllForecast()
        {
            try
            {
                var query = await (from weather in _context.WeatherForecasts
                            select weather).ToListAsync();

                return query;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public async Task<WeatherForecast> GetForecast(Guid id)
        {
            try
            {
                var query = await (from weather in _context.WeatherForecasts
                                   where weather.ID == id
                                   select weather).FirstOrDefaultAsync();

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<WeatherForecast> InsertUpdateForecast(WeatherForecast weatherForecastInput)
        {
            try
            {
                var weatherForecast = await _context.WeatherForecasts.Where(m => m.ID == weatherForecastInput.ID).FirstOrDefaultAsync();
                if(weatherForecast == null)
                {
                    weatherForecast = new WeatherForecast();
                    weatherForecast = weatherForecastInput;
                    await _context.WeatherForecasts.AddAsync(weatherForecast);
                }
                else
                {
                    weatherForecast.Date = weatherForecastInput.Date;
                    weatherForecast.Summary = weatherForecastInput.Summary;
                    weatherForecast.TemperatureC = weatherForecastInput.TemperatureC;
                }

                var result = await _context.SaveChangesAsync();
                return weatherForecast;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
