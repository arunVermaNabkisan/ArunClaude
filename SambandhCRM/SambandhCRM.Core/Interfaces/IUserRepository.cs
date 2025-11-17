using SambandhCRM.Core.Models;

namespace SambandhCRM.Core.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUserNameAsync(string userName);
    Task<User?> GetByEmailAsync(string email);
    Task<bool> ValidateCredentialsAsync(string userName, string passwordHash);
    Task<IEnumerable<Role>> GetUserRolesAsync(int userId);
}
