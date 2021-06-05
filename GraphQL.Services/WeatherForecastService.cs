using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.DataContext;
using GraphQL.Models;
using GraphQL.Services.Interface;

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
            string[] Summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            var rng = new Random();
            var weatherList = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                ID = Guid.NewGuid(),
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToList();

            foreach (var weather in weatherList)
            {
                await _context.WeatherForecasts.AddAsync(weather);
            }

            await _context.SaveChangesAsync();

            List<WeatherForecast> result = new List<WeatherForecast>();

            var query = from weather in _context.WeatherForecasts
                        select weather;

            result = query.ToList();

            return result;

        }
    }
}
