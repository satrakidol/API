using ApiAggregationService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiAggregationService.Models;
using API_aggregation_service.Services;
using API_aggregation_service.Models;



namespace ApiAggregationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AggregationController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IWeatherService _weatherService;
        private readonly IGitHubService _gitHubService;
        private readonly IAggregationService _aggregationService;
        private readonly ILogger<AggregationController> _logger;

        public AggregationController(INewsService newsService, IWeatherService weatherService, IGitHubService gitHubService, IAggregationService aggregationService, ILogger<AggregationController> logger)
        {
            _newsService = newsService;
            _weatherService = weatherService;
            _gitHubService = gitHubService;
            _aggregationService = aggregationService;
            _logger = logger;
        }

        [HttpGet("news/{topic}")]
        public async Task<ActionResult<List<NewsArticle>>> GetNews(string topic)
        {
            try
            {
                var newsArticles = await _newsService.GetNewsAsync(topic);
                return Ok(newsArticles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving news articles");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("weather/{latitude}/{longitude}")]
        public async Task<ActionResult<WeatherApiResponse>> GetWeather(double latitude, double longitude)
        {
            try
            {
                var weatherData = await _weatherService.GetWeatherAsync(latitude, longitude);
                return Ok(weatherData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving weather data");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("github/repos/{username}")]
        public async Task<ActionResult<List<GitHubRepository>>> GetRepositories(string username)
        {
            try
            {
                var repositories = await _gitHubService.GetRepositoriesAsync(username);
                return Ok(repositories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving GitHub repositories");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("aggregate/{topic}/{latitude}/{longitude}/{username}")]
        public async Task<ActionResult<AggregatedResponse>> GetAggregatedData(string topic, int latitude, int longitude, string username)
        {
            try
            {
                var data = await _aggregationService.GetAggregatedDataAsync(topic, latitude, longitude, username);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving aggregated data");
                return StatusCode(500, "Internal server error: Failed to fetch aggregated data. Please try again later.");
            }
        }
    }



}


