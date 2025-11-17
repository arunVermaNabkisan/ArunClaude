using SambandhCRM.Core.Models;

namespace SambandhCRM.Core.Interfaces;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetByCustomerCodeAsync(string customerCode);
    Task<Customer?> GetByPANAsync(string panNumber);
    Task<IEnumerable<Customer>> GetByAssignedUserAsync(int userId);
    Task<IEnumerable<Customer>> GetByStatusAsync(string status);
    Task<IEnumerable<Customer>> SearchAsync(string searchTerm);
    Task<bool> CheckDuplicateAsync(string panNumber, string? registrationNumber, string? mobileNumber);
}
