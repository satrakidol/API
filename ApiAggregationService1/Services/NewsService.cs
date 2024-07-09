using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApiAggregationService.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApiAggregationService.Services
{
    public class NewsService : INewsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NewsService> _logger;

        public NewsService(HttpClient httpClient, ILogger<NewsService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<NewsArticle>> GetNewsAsync(string topic)
        {
            try
            {
                var apiKey = "43c59bb5054641b19087b3c694439859";
                var apiUrl = $"https://newsapi.org/v2/everything?q={topic}&apiKey={apiKey}";

                var response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var newsApiResponse = JsonConvert.DeserializeObject<NewsApiResponse>(responseBody);

                if (newsApiResponse.Status.ToLower() != "ok")
                {
                    _logger.LogError($"News API returned status: {newsApiResponse.Status}");
                    return GetFallbackNewsData();
                }

                return newsApiResponse.Articles;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Error fetching news data");
                return GetFallbackNewsData();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unexpected error");
                throw;
            }
        }

        private List<NewsArticle> GetFallbackNewsData()
        {
            // Provide some default fallback news data
            return new List<NewsArticle>
            {
                new NewsArticle
                {
                    Title = "Fallback news data",
                    Description = "No news available"
                }
            };
        }
    }
}
