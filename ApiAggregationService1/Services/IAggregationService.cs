using System.Threading.Tasks;
using API_aggregation_service.Models;

namespace API_aggregation_service.Services
{
    public interface IAggregationService
    {
        Task<AggregatedResponse> GetAggregatedDataAsync(string topic, double latitude, double longitude, string username);
    }
}
