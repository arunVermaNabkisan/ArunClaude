using SambandhCRM.Core.Models;

namespace SambandhCRM.Core.Interfaces;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetByNameAsync(string roleName);
    Task<IEnumerable<User>> GetRoleUsersAsync(int roleId);
}
