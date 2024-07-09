using System;
using System.Collections.Generic;
using API_aggregation_service.Models;
using ApiAggregationService.Models;

namespace API_aggregation_service.Models
{
    public class AggregatedResponse
    {
        public List<NewsArticle> News { get; set; }
        public WeatherApiResponse Weather { get; set; }
        public List<GitHubRepository> Repositories { get; set; }
    }
}
