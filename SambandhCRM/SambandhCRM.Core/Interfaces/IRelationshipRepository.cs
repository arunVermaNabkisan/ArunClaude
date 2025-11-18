using SambandhCRM.Core.Models;

namespace SambandhCRM.Core.Interfaces;

public interface IRelationshipRepository : IRepository<IndividualOrganization>
{
    Task<IEnumerable<IndividualOrganization>> GetByCustomerIdAsync(int customerId);
    Task<IEnumerable<IndividualOrganization>> GetByIndividualIdAsync(int individualId);
    Task<IEnumerable<IndividualOrganization>> GetActiveRelationshipsAsync();
}
