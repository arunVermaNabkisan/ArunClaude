using Dapper;
using SambandhCRM.Core.Interfaces;
using SambandhCRM.Core.Models;
using SambandhCRM.Data.Context;

namespace SambandhCRM.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DapperContext _context;

    public UserRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var query = "SELECT * FROM Users WHERE IsActive = 1";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<User>(query);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        var query = "SELECT * FROM Users WHERE UserId = @Id AND IsActive = 1";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<User>(query, new { Id = id });
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        var query = "SELECT * FROM Users WHERE UserName = @UserName AND IsActive = 1";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<User>(query, new { UserName = userName });
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var query = "SELECT * FROM Users WHERE Email = @Email AND IsActive = 1";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
    }

    public async Task<int> AddAsync(User entity)
    {
        var query = @"
            INSERT INTO Users (UserName, Email, PasswordHash, FirstName, LastName, MobileNumber, IsActive, CreatedDate, CreatedBy)
            VALUES (@UserName, @Email, @PasswordHash, @FirstName, @LastName, @MobileNumber, @IsActive, @CreatedDate, @CreatedBy);
            SELECT CAST(SCOPE_IDENTITY() as int)";

        using var connection = _context.CreateConnection();
        return await connection.QuerySingleAsync<int>(query, entity);
    }

    public async Task<bool> UpdateAsync(User entity)
    {
        var query = @"
            UPDATE Users
            SET UserName = @UserName,
                Email = @Email,
                FirstName = @FirstName,
                LastName = @LastName,
                MobileNumber = @MobileNumber,
                IsActive = @IsActive,
                ModifiedDate = @ModifiedDate,
                ModifiedBy = @ModifiedBy
            WHERE UserId = @UserId";

        using var connection = _context.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(query, entity);
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var query = "UPDATE Users SET IsActive = 0 WHERE UserId = @Id";
        using var connection = _context.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
        return affectedRows > 0;
    }

    public async Task<bool> ValidateCredentialsAsync(string userName, string passwordHash)
    {
        var query = "SELECT COUNT(1) FROM Users WHERE UserName = @UserName AND PasswordHash = @PasswordHash AND IsActive = 1";
        using var connection = _context.CreateConnection();
        var count = await connection.QuerySingleAsync<int>(query, new { UserName = userName, PasswordHash = passwordHash });
        return count > 0;
    }

    public async Task<IEnumerable<Role>> GetUserRolesAsync(int userId)
    {
        var query = @"
            SELECT r.*
            FROM Roles r
            INNER JOIN UserRoles ur ON r.RoleId = ur.RoleId
            WHERE ur.UserId = @UserId AND r.IsActive = 1";

        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Role>(query, new { UserId = userId });
    }
}
