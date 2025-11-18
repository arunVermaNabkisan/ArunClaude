using SambandhCRM.Core.Models;

namespace SambandhCRM.Core.Interfaces;

public interface IIndividualRepository : IRepository<Individual>
{
    Task<Individual?> GetByPANAsync(string panNumber);
    Task<Individual?> GetByMobileAsync(string mobileNumber);
    Task<IEnumerable<Individual>> SearchAsync(string searchTerm);
    Task<bool> CheckDuplicateAsync(string mobileNumber, string? panNumber);
}
