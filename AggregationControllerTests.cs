using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using ApiAggregationService.Controllers;
using ApiAggregationService.Services;
using ApiAggregationService.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using API_aggregation_service.Services;
using Microsoft.AspNetCore.Mvc;


namespace ApiAggregationService.Tests.Controllers
{
    public class AggregationControllerTests
    {
        private readonly Mock<INewsService> _newsServiceMock;
        private readonly Mock<IWeatherService> _weatherServiceMock;
        private readonly Mock<IGitHubService> _gitHubServiceMock;
        private readonly Mock<IAggregationService> _aggregationServiceMock;
        private readonly Mock<ILogger<AggregationController>> _loggerMock;
        private readonly AggregationController _controller;

        public AggregationControllerTests()
        {
            _newsServiceMock = new Mock<INewsService>();
            _weatherServiceMock = new Mock<IWeatherService>();
            _gitHubServiceMock = new Mock<IGitHubService>();
            _aggregationServiceMock = new Mock<IAggregationService>();
            _loggerMock = new Mock<ILogger<AggregationController>>();


            _controller = new AggregationController(
            _newsServiceMock.Object,
            _weatherServiceMock.Object,
            _gitHubServiceMock.Object,
            _aggregationServiceMock.Object,
            _loggerMock.Object
        );
        }

        [Fact]
        public async Task GetNews_Returns_OkObjectResult()
        {
            // Arrange
            var mockNewsService = new Mock<INewsService>();
            var mockWeatherService = new Mock<IWeatherService>();
            var mockGitHubService = new Mock<IGitHubService>();
            var mockAggregationService = new Mock<IAggregationService>();
            var mockLogger = new Mock<ILogger<AggregationController>>();

            var controller = new AggregationController(
                mockNewsService.Object,
                mockWeatherService.Object,
                mockGitHubService.Object,
                mockAggregationService.Object,
                mockLogger.Object
            );

            string topic = "tesla"; // Example topic

            // Mock the response of INewsService
            var mockNewsArticles = new List<NewsArticle>
        {
            new NewsArticle { Title = "Test Article", Description = "Test Description" }
        };
            mockNewsService.Setup(s => s.GetNewsAsync(topic)).ReturnsAsync(mockNewsArticles);

            // Act
            var result = await controller.GetNews(topic);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var newsArticles = Assert.IsAssignableFrom<List<NewsArticle>>(okResult.Value);
            Assert.Equal(mockNewsArticles.Count, newsArticles.Count);
        }
    }
}



namespace ApiAggregationService.Tests
{
    internal class AggregationControllerTests
    {
    }
}
