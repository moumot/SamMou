using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.DataContext;
using GraphQL.Models;
using GraphQL.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly GraphQLDataContext _context;
        public WeatherForecastService(GraphQLDataContext context)
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
