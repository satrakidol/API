using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;
using Xunit;
using ApiAggregationService.Services;
using API_aggregation_service.Services;
using API_aggregation_service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiAggregationService.Models;

namespace ApiAggregationService.Tests.Services
{
    public class AggregationServiceTests
    {
        private readonly Mock<INewsService> _newsServiceMock;
        private readonly Mock<IWeatherService> _weatherServiceMock;
        private readonly Mock<IGitHubService> _gitHubServiceMock;
        private readonly AggregationService _service;

        public AggregationServiceTests()
        {
            _newsServiceMock = new Mock<INewsService>();
            _weatherServiceMock = new Mock<IWeatherService>();
            _gitHubServiceMock = new Mock<IGitHubService>();
            _service = new AggregationService(_newsServiceMock.Object, _weatherServiceMock.Object, _gitHubServiceMock.Object);
        }

        [Fact]
        public async Task GetAggregatedDataAsync_ReturnsAggregatedResponse()
        {
            // Arrange
            var topic = "test";
            var latitude = 0.0;
            var longitude = 0.0;
            var username = "testuser";
   

            var mockNewsArticles = new List<NewsArticle>
{
                new NewsArticle
                {
                    Author = "John Doe",
                    Title = "Tesla announces new model",
                    Description = "Tesla unveils its latest electric vehicle.",
                    Url = "https://example.com/news/123",
                    PublishedAt = DateTime.UtcNow.AddDays(-1)
                },
                new NewsArticle
                {
                    Author = "Jane Smith",
                    Title = "Tesla stock rises",
                    Description = "Tesla's stock price reaches all-time high.",
                    Url = "https://example.com/news/456",
                    PublishedAt = DateTime.UtcNow.AddDays(-2)
                }
            };

            var weatherData = new WeatherApiResponse
            {
                CurrentWeather = new CurrentWeather
                {
                    Temperature_2m = 25.5,
                    Wind_Speed_10m = 10.2
                },
                Hourly = new HourlyWeather
                {
                    Time = new List<DateTime>
        {
                        DateTime.UtcNow.AddDays(1), // Example times
                        DateTime.UtcNow.AddDays(2),
                        DateTime.UtcNow.AddDays(3)
                    },
                                Temperature_2m = new List<double>
                    {
                        28.3, // Example temperatures
                        29.1,
                        27.8
                    },
                                Relative_Humidity_2m = new List<double>
                    {
                        65.2, // Example relative humidity values
                        63.8,
                        67.5
                    },
                                Wind_Speed_10m = new List<double>
                    {
                        12.5, // Example wind speeds
                        11.9,
                        13.2
                    }
                            }
            };
            var mockRepositories = new List<GitHubRepository>
{
                new GitHubRepository
                {
                    name = "repo1",
                    description = "Repository 1",
                    html_url = "https://github.com/user/repo1",
                    created_at = DateTime.UtcNow.AddDays(-10),
                    updated_at = DateTime.UtcNow.AddDays(-5),
                    pushed_at = DateTime.UtcNow.AddDays(-2)
                },
                new GitHubRepository
                {
                    name = "repo2",
                    description = "Repository 2",
                    html_url = "https://github.com/user/repo2",
                    created_at = DateTime.UtcNow.AddDays(-15),
                    updated_at = DateTime.UtcNow.AddDays(-7),
                    pushed_at = DateTime.UtcNow.AddDays(-3)
                }
            };


            _newsServiceMock.Setup(service => service.GetNewsAsync(topic)).ReturnsAsync(mockNewsArticles);
            _weatherServiceMock.Setup(service => service.GetWeatherAsync(latitude, longitude)).ReturnsAsync(weatherData);
            _gitHubServiceMock.Setup(service => service.GetRepositoriesAsync(username)).ReturnsAsync(mockRepositories);

            // Act
            var result = await _service.GetAggregatedDataAsync(topic, latitude, longitude, username);

            // Assert
            Assert.Equal(mockNewsArticles, result.News);
            Assert.Equal(weatherData, result.Weather);
            Assert.Equal(mockRepositories, result.Repositories);
        }
    }
}

