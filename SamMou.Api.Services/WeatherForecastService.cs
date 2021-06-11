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

        public async Task<List<WeatherForecast>> GetForecast()
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

        public async Task<bool> InsertForecast(WeatherForecast weatherForecast)
        {
            try
            {
                await _context.WeatherForecasts.AddAsync(weatherForecast);

                var result = await _context.SaveChangesAsync();

                return result > 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
