using System.Collections.Generic;
using System.Threading.Tasks;
using ApiAggregationService.Models;

namespace ApiAggregationService.Services
{
    public interface INewsService
    {
        Task<List<NewsArticle>> GetNewsAsync(string topic);
    }
}
