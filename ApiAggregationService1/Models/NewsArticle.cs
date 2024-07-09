using System;

namespace ApiAggregationService.Models
{
    public class NewsArticle
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}
