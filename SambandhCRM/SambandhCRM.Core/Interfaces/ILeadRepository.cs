using SambandhCRM.Core.Models;

namespace SambandhCRM.Core.Interfaces;

public interface ILeadRepository : IRepository<Lead>
{
    Task<Lead?> GetByLeadCodeAsync(string leadCode);
    Task<IEnumerable<Lead>> GetByCustomerIdAsync(int customerId);
    Task<IEnumerable<Lead>> GetByAssignedUserAsync(int userId);
    Task<IEnumerable<Lead>> GetByStatusAsync(string status);
    Task<IEnumerable<Lead>> GetFollowUpsDueAsync(DateTime date);
    Task<string> GenerateLeadCodeAsync();
}
