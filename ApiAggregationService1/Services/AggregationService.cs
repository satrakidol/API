using API_aggregation_service.Models;
using ApiAggregationService.Services;
using System.Threading.Tasks;

namespace API_aggregation_service.Services
{
    public class AggregationService : IAggregationService
    {
        private readonly INewsService _newsService;
        private readonly IWeatherService _weatherService;
        private readonly IGitHubService _gitHubService;

        public AggregationService(INewsService newsService, IWeatherService weatherService, IGitHubService gitHubService)
        {
            _newsService = newsService;
            _weatherService = weatherService;
            _gitHubService = gitHubService;
        }

        public async Task<AggregatedResponse> GetAggregatedDataAsync(string topic, double latitude, double longitude, string username)
        {
            var newsTask = _newsService.GetNewsAsync(topic);
            var weatherTask = _weatherService.GetWeatherAsync(latitude, longitude);
            var gitHubTask = _gitHubService.GetRepositoriesAsync(username);

            await Task.WhenAll(newsTask, weatherTask, gitHubTask);

            return new AggregatedResponse
            {
                News = await newsTask,
                Weather = await weatherTask,
                Repositories = await gitHubTask
            };
        }
    }
}
