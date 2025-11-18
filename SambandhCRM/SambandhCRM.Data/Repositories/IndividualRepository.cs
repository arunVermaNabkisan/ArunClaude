using Dapper;
using SambandhCRM.Core.Interfaces;
using SambandhCRM.Core.Models;
using SambandhCRM.Data.Context;

namespace SambandhCRM.Data.Repositories;

public class IndividualRepository : IIndividualRepository
{
    private readonly DapperContext _context;

    public IndividualRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Individual>> GetAllAsync()
    {
        var query = "SELECT * FROM Individuals WHERE IsActive = 1 ORDER BY CreatedDate DESC";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Individual>(query);
    }

    public async Task<Individual?> GetByIdAsync(int id)
    {
        var query = "SELECT * FROM Individuals WHERE IndividualId = @Id AND IsActive = 1";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Individual>(query, new { Id = id });
    }

    public async Task<Individual?> GetByPANAsync(string panNumber)
    {
        var query = "SELECT * FROM Individuals WHERE PANNumber = @PANNumber AND IsActive = 1";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Individual>(query, new { PANNumber = panNumber });
    }

    public async Task<Individual?> GetByMobileAsync(string mobileNumber)
    {
        var query = "SELECT * FROM Individuals WHERE MobileNumber = @MobileNumber AND IsActive = 1";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Individual>(query, new { MobileNumber = mobileNumber });
    }

    public async Task<IEnumerable<Individual>> SearchAsync(string searchTerm)
    {
        var query = @"SELECT * FROM Individuals
                     WHERE IsActive = 1
                     AND (FullName LIKE @SearchTerm
                          OR MobileNumber LIKE @SearchTerm
                          OR PANNumber LIKE @SearchTerm
                          OR Email LIKE @SearchTerm)
                     ORDER BY FullName";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Individual>(query, new { SearchTerm = $"%{searchTerm}%" });
    }

    public async Task<bool> CheckDuplicateAsync(string mobileNumber, string? panNumber)
    {
        var query = @"SELECT COUNT(1) FROM Individuals
                     WHERE IsActive = 1
                     AND (MobileNumber = @MobileNumber
                          OR (PANNumber = @PANNumber AND @PANNumber IS NOT NULL))";
        using var connection = _context.CreateConnection();
        var count = await connection.ExecuteScalarAsync<int>(query, new { MobileNumber = mobileNumber, PANNumber = panNumber });
        return count > 0;
    }

    public async Task<int> AddAsync(Individual entity)
    {
        var query = @"INSERT INTO Individuals
                     (FullName, MobileNumber, Email, PANNumber, DINNumber, LinkedInProfile, AlternatePhone, Address, IsActive, CreatedDate, CreatedBy)
                     VALUES (@FullName, @MobileNumber, @Email, @PANNumber, @DINNumber, @LinkedInProfile, @AlternatePhone, @Address, @IsActive, @CreatedDate, @CreatedBy);
                     SELECT CAST(SCOPE_IDENTITY() as int)";
        using var connection = _context.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(query, entity);
    }

    public async Task<bool> UpdateAsync(Individual entity)
    {
        var query = @"UPDATE Individuals
                     SET FullName = @FullName,
                         MobileNumber = @MobileNumber,
                         Email = @Email,
                         PANNumber = @PANNumber,
                         DINNumber = @DINNumber,
                         LinkedInProfile = @LinkedInProfile,
                         AlternatePhone = @AlternatePhone,
                         Address = @Address,
                         ModifiedDate = @ModifiedDate,
                         ModifiedBy = @ModifiedBy
                     WHERE IndividualId = @IndividualId";
        using var connection = _context.CreateConnection();
        var result = await connection.ExecuteAsync(query, entity);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var query = "UPDATE Individuals SET IsActive = 0 WHERE IndividualId = @Id";
        using var connection = _context.CreateConnection();
        var result = await connection.ExecuteAsync(query, new { Id = id });
        return result > 0;
    }
}
