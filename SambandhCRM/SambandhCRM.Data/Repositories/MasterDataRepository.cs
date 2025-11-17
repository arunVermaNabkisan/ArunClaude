using Dapper;
using SambandhCRM.Core.Interfaces;
using SambandhCRM.Core.Models;
using SambandhCRM.Data.Context;

namespace SambandhCRM.Data.Repositories;

public class MasterDataRepository : IMasterDataRepository
{
    private readonly DapperContext _context;

    public MasterDataRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MasterData>> GetAllAsync()
    {
        var query = "SELECT * FROM MasterData WHERE IsActive = 1 ORDER BY Category, DisplayOrder";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<MasterData>(query);
    }

    public async Task<MasterData?> GetByIdAsync(int id)
    {
        var query = "SELECT * FROM MasterData WHERE MasterDataId = @Id";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<MasterData>(query, new { Id = id });
    }

    public async Task<IEnumerable<MasterData>> GetByCategoryAsync(string category)
    {
        var query = "SELECT * FROM MasterData WHERE Category = @Category AND IsActive = 1 ORDER BY DisplayOrder";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<MasterData>(query, new { Category = category });
    }

    public async Task<int> AddAsync(MasterData entity)
    {
        var query = @"
            INSERT INTO MasterData (Category, Value, DisplayOrder, IsActive, IsSystemDefined, CreatedDate, CreatedBy)
            VALUES (@Category, @Value, @DisplayOrder, @IsActive, @IsSystemDefined, @CreatedDate, @CreatedBy);
            SELECT CAST(SCOPE_IDENTITY() as int)";

        using var connection = _context.CreateConnection();
        return await connection.QuerySingleAsync<int>(query, entity);
    }

    public async Task<bool> UpdateAsync(MasterData entity)
    {
        var query = @"
            UPDATE MasterData
            SET Category = @Category,
                Value = @Value,
                DisplayOrder = @DisplayOrder,
                IsActive = @IsActive,
                ModifiedDate = @ModifiedDate,
                ModifiedBy = @ModifiedBy
            WHERE MasterDataId = @MasterDataId AND IsSystemDefined = 0";

        using var connection = _context.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(query, entity);
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var query = "UPDATE MasterData SET IsActive = 0 WHERE MasterDataId = @Id AND IsSystemDefined = 0";
        using var connection = _context.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
        return affectedRows > 0;
    }

    public async Task<bool> DeleteByCategoryAndValueAsync(string category, string value)
    {
        var query = "UPDATE MasterData SET IsActive = 0 WHERE Category = @Category AND Value = @Value AND IsSystemDefined = 0";
        using var connection = _context.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(query, new { Category = category, Value = value });
        return affectedRows > 0;
    }
}
