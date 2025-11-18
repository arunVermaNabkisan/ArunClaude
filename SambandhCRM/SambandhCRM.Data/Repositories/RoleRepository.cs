using Dapper;
using SambandhCRM.Core.Interfaces;
using SambandhCRM.Core.Models;
using SambandhCRM.Data.Context;

namespace SambandhCRM.Data.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly DapperContext _context;

    public RoleRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        var query = "SELECT * FROM Roles WHERE IsActive = 1 ORDER BY RoleName";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Role>(query);
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        var query = "SELECT * FROM Roles WHERE RoleId = @Id AND IsActive = 1";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Role>(query, new { Id = id });
    }

    public async Task<Role?> GetByNameAsync(string roleName)
    {
        var query = "SELECT * FROM Roles WHERE RoleName = @RoleName AND IsActive = 1";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Role>(query, new { RoleName = roleName });
    }

    public async Task<int> AddAsync(Role entity)
    {
        var query = @"
            INSERT INTO Roles (RoleName, Description, IsActive, CreatedDate, CreatedBy)
            VALUES (@RoleName, @Description, @IsActive, @CreatedDate, @CreatedBy);
            SELECT CAST(SCOPE_IDENTITY() as int)";

        using var connection = _context.CreateConnection();
        return await connection.QuerySingleAsync<int>(query, entity);
    }

    public async Task<bool> UpdateAsync(Role entity)
    {
        var query = @"
            UPDATE Roles
            SET RoleName = @RoleName,
                Description = @Description,
                IsActive = @IsActive,
                ModifiedDate = @ModifiedDate,
                ModifiedBy = @ModifiedBy
            WHERE RoleId = @RoleId";

        using var connection = _context.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(query, entity);
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var query = "UPDATE Roles SET IsActive = 0 WHERE RoleId = @Id";
        using var connection = _context.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
        return affectedRows > 0;
    }

    public async Task<IEnumerable<User>> GetRoleUsersAsync(int roleId)
    {
        var query = @"
            SELECT u.*
            FROM Users u
            INNER JOIN UserRoles ur ON u.UserId = ur.UserId
            WHERE ur.RoleId = @RoleId AND u.IsActive = 1";

        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<User>(query, new { RoleId = roleId });
    }
}
