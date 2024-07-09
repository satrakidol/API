using ApiAggregationService.Models;
using System.Threading.Tasks;

namespace ApiAggregationService.Services
{
    public interface IWeatherService
    {
        Task<WeatherApiResponse> GetWeatherAsync(double latitude, double longitude);
    }
}
