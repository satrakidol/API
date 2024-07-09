using System;

namespace API_aggregation_service.Models
{
    public class GitHubRepository
    {
        public string name { get; set; }
        public string description { get; set; }
        public string html_url { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime pushed_at { get; set; }
    }

    public class GitHubOwner
    {
        public string login { get; set; }
        public int id { get; set; }
        public string avatar_url { get; set; }
        public string url { get; set; }
    }
}

