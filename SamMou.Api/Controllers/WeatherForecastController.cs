﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SamMou.Api.Models;
using SamMou.Api.Services.Interface;

namespace SamMou.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var output = await _weatherForecastService.GetForecast();
                if (output != null)
                    return Ok(output);
                else
                    throw new Exception("GetForecast returns null");
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Insert(WeatherForecast weatherForecast)
        {
            try
            {
                var output = await _weatherForecastService.InsertForecast(weatherForecast);
                if (output)
                    return Ok(output);
                else
                    throw new Exception("GetForecast returns null");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return BadRequest(ex.Message);
            }

        }
    }
}
