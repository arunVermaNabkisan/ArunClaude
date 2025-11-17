using Dapper;
using SambandhCRM.Core.Interfaces;
using SambandhCRM.Core.Models;
using SambandhCRM.Data.Context;

namespace SambandhCRM.Data.Repositories;

public class LeadRepository : ILeadRepository
{
    private readonly DapperContext _context;

    public LeadRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Lead>> GetAllAsync()
    {
        var query = "SELECT * FROM Leads WHERE IsActive = 1 ORDER BY CreatedDate DESC";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Lead>(query);
    }

    public async Task<Lead?> GetByIdAsync(int id)
    {
        var query = "SELECT * FROM Leads WHERE LeadId = @Id AND IsActive = 1";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Lead>(query, new { Id = id });
    }

    public async Task<Lead?> GetByLeadCodeAsync(string leadCode)
    {
        var query = "SELECT * FROM Leads WHERE LeadCode = @LeadCode AND IsActive = 1";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Lead>(query, new { LeadCode = leadCode });
    }

    public async Task<IEnumerable<Lead>> GetByCustomerIdAsync(int customerId)
    {
        var query = "SELECT * FROM Leads WHERE CustomerId = @CustomerId AND IsActive = 1 ORDER BY CreatedDate DESC";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Lead>(query, new { CustomerId = customerId });
    }

    public async Task<IEnumerable<Lead>> GetByAssignedUserAsync(int userId)
    {
        var query = "SELECT * FROM Leads WHERE AssignedToUserId = @UserId AND IsActive = 1 ORDER BY NextFollowUpDate, CreatedDate DESC";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Lead>(query, new { UserId = userId });
    }

    public async Task<IEnumerable<Lead>> GetByStatusAsync(string status)
    {
        var query = "SELECT * FROM Leads WHERE LeadStatus = @Status AND IsActive = 1 ORDER BY CreatedDate DESC";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Lead>(query, new { Status = status });
    }

    public async Task<IEnumerable<Lead>> GetFollowUpsDueAsync(DateTime date)
    {
        var query = @"
            SELECT * FROM Leads
            WHERE IsActive = 1
            AND LeadStatus NOT IN ('Dropped', 'Converted')
            AND NextFollowUpDate <= @Date
            ORDER BY NextFollowUpDate";

        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Lead>(query, new { Date = date });
    }

    public async Task<string> GenerateLeadCodeAsync()
    {
        var query = @"
            SELECT TOP 1 LeadCode
            FROM Leads
            WHERE LeadCode LIKE 'LEAD-' + FORMAT(GETDATE(), 'yyyyMM') + '-%'
            ORDER BY LeadCode DESC";

        using var connection = _context.CreateConnection();
        var lastCode = await connection.QueryFirstOrDefaultAsync<string>(query);

        var yearMonth = DateTime.Now.ToString("yyyyMM");
        if (string.IsNullOrEmpty(lastCode))
        {
            return $"LEAD-{yearMonth}-0001";
        }

        var lastNumber = int.Parse(lastCode.Split('-')[2]);
        var newNumber = lastNumber + 1;
        return $"LEAD-{yearMonth}-{newNumber:D4}";
    }

    public async Task<int> AddAsync(Lead entity)
    {
        var query = @"
            INSERT INTO Leads (
                LeadCode, CustomerId, LeadSource, LeadSourceDetails, ProductInterest,
                LoanAmountMin, LoanAmountMax, Priority, LeadStatus, DropReason,
                AssignedToUserId, AssignedDate, AssignedBy, LastContactDate, NextFollowUpDate,
                Notes, HasKYCDocuments, HasFinancialStatements, HasBusinessDocuments, HasOtherDocuments,
                ConvertedDate, ConvertedBy, IsActive, CreatedDate, CreatedBy
            )
            VALUES (
                @LeadCode, @CustomerId, @LeadSource, @LeadSourceDetails, @ProductInterest,
                @LoanAmountMin, @LoanAmountMax, @Priority, @LeadStatus, @DropReason,
                @AssignedToUserId, @AssignedDate, @AssignedBy, @LastContactDate, @NextFollowUpDate,
                @Notes, @HasKYCDocuments, @HasFinancialStatements, @HasBusinessDocuments, @HasOtherDocuments,
                @ConvertedDate, @ConvertedBy, @IsActive, @CreatedDate, @CreatedBy
            );
            SELECT CAST(SCOPE_IDENTITY() as int)";

        using var connection = _context.CreateConnection();
        return await connection.QuerySingleAsync<int>(query, entity);
    }

    public async Task<bool> UpdateAsync(Lead entity)
    {
        var query = @"
            UPDATE Leads
            SET LeadSource = @LeadSource,
                LeadSourceDetails = @LeadSourceDetails,
                ProductInterest = @ProductInterest,
                LoanAmountMin = @LoanAmountMin,
                LoanAmountMax = @LoanAmountMax,
                Priority = @Priority,
                LeadStatus = @LeadStatus,
                DropReason = @DropReason,
                AssignedToUserId = @AssignedToUserId,
                AssignedDate = @AssignedDate,
                LastContactDate = @LastContactDate,
                NextFollowUpDate = @NextFollowUpDate,
                Notes = @Notes,
                HasKYCDocuments = @HasKYCDocuments,
                HasFinancialStatements = @HasFinancialStatements,
                HasBusinessDocuments = @HasBusinessDocuments,
                HasOtherDocuments = @HasOtherDocuments,
                ConvertedDate = @ConvertedDate,
                ConvertedBy = @ConvertedBy,
                ModifiedDate = @ModifiedDate,
                ModifiedBy = @ModifiedBy
            WHERE LeadId = @LeadId";

        using var connection = _context.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(query, entity);
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var query = "UPDATE Leads SET IsActive = 0 WHERE LeadId = @Id";
        using var connection = _context.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
        return affectedRows > 0;
    }
}
