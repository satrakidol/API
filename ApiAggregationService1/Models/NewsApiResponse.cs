using System.Collections.Generic;

namespace ApiAggregationService.Models
{
    public class NewsApiResponse
    {
        public string Status { get; set; }
        public int TotalResults { get; set; }
        public List<NewsArticle> Articles { get; set; }
    }

    
}
