using System;
using System.Net.Http;
using System.Threading.Tasks;
using ApiAggregationService.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApiAggregationService.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(HttpClient httpClient, ILogger<WeatherService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<WeatherApiResponse> GetWeatherAsync(double latitude, double longitude)
        {
            try
            {
                var apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,wind_speed_10m&hourly=temperature_2m,relative_humidity_2m,wind_speed_10m";
                var response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var weatherData = JsonConvert.DeserializeObject<WeatherApiResponse>(responseBody);

                return weatherData;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching weather data");
                throw new ApplicationException("Failed to fetch weather data. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while fetching weather data");
                throw;
            }
        }
    }
}
