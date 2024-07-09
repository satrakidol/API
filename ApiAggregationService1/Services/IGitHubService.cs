using System.Collections.Generic;
using System.Threading.Tasks;
using API_aggregation_service.Models;

namespace API_aggregation_service.Services
{
    public interface IGitHubService
    {
        Task<List<GitHubRepository>> GetRepositoriesAsync(string username);
    }
}
