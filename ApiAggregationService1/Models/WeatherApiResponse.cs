using System;
using System.Collections.Generic;

namespace ApiAggregationService.Models
{
    public class WeatherApiResponse
    {
        public CurrentWeather CurrentWeather { get; set; }
        public HourlyWeather Hourly { get; set; }
    }

    public class CurrentWeather
    {
        public DateTime Time { get; set; }
        public double Temperature_2m { get; set; }
        public double Wind_Speed_10m { get; set; }
    }

    public class HourlyWeather
    {
        public List<DateTime> Time { get; set; }
        public List<double> Temperature_2m { get; set; }
        public List<double> Relative_Humidity_2m { get; set; }
        public List<double> Wind_Speed_10m { get; set; }
    }
}
