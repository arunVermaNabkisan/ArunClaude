using SambandhCRM.Core.Models;

namespace SambandhCRM.Core.Interfaces;

public interface ICommunicationRepository : IRepository<Communication>
{
    Task<IEnumerable<Communication>> GetByCustomerIdAsync(int customerId);
    Task<IEnumerable<Communication>> GetByLeadIdAsync(int leadId);
    Task<IEnumerable<Communication>> GetByCommunicationTypeAsync(string communicationType);
    Task<IEnumerable<Communication>> GetByCampaignAsync(string campaignName);
    Task<IEnumerable<Communication>> GetRecentCommunicationsAsync(int days = 30);
}
