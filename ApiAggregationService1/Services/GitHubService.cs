using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using API_aggregation_service.Models;
using API_aggregation_service.Services;
using ApiAggregationService.Models;

namespace ApiAggregationService.Services
{
    public class GitHubService : IGitHubService
    {
        private readonly HttpClient _httpClient;

        public GitHubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GitHubRepository>> GetRepositoriesAsync(string username)
        {
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");

            var response = await _httpClient.GetAsync($"https://api.github.com/users/{username}/repos");

            if (!response.IsSuccessStatusCode)
            {
                return new List<GitHubRepository>();
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var repositories = JsonSerializer.Deserialize<List<GitHubRepository>>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return repositories;
        }
    }
}
