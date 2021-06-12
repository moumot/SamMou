using System;
using System.ComponentModel.DataAnnotations;

namespace SamMou.Api.Models
{
    public class WeatherForecast
    {
        [Key]
        public Guid ID { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }
    }
}
