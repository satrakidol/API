using System;

namespace ApiAggregationService.Models
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public string Temperature { get; set; }
        public string Humidity { get; set; }
        public string WindSpeed { get; set; }
    }
}
