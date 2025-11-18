using Dapper;
using SambandhCRM.Core.Interfaces;
using SambandhCRM.Core.Models;
using SambandhCRM.Data.Context;

namespace SambandhCRM.Data.Repositories;

public class CommunicationRepository : ICommunicationRepository
{
    private readonly DapperContext _context;

    public CommunicationRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Communication>> GetAllAsync()
    {
        var query = "SELECT * FROM Communications WHERE IsActive = 1 ORDER BY CommunicationDate DESC";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Communication>(query);
    }

    public async Task<Communication?> GetByIdAsync(int id)
    {
        var query = "SELECT * FROM Communications WHERE CommunicationId = @Id AND IsActive = 1";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Communication>(query, new { Id = id });
    }

    public async Task<IEnumerable<Communication>> GetByCustomerIdAsync(int customerId)
    {
        var query = @"SELECT c.*, cust.EntityName as CustomerName
                     FROM Communications c
                     LEFT JOIN Customers cust ON c.CustomerId = cust.CustomerId
                     WHERE c.CustomerId = @CustomerId AND c.IsActive = 1
                     ORDER BY c.CommunicationDate DESC";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Communication>(query, new { CustomerId = customerId });
    }

    public async Task<IEnumerable<Communication>> GetByLeadIdAsync(int leadId)
    {
        var query = @"SELECT c.*, l.LeadCode
                     FROM Communications c
                     LEFT JOIN Leads l ON c.LeadId = l.LeadId
                     WHERE c.LeadId = @LeadId AND c.IsActive = 1
                     ORDER BY c.CommunicationDate DESC";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Communication>(query, new { LeadId = leadId });
    }

    public async Task<IEnumerable<Communication>> GetByCommunicationTypeAsync(string communicationType)
    {
        var query = @"SELECT * FROM Communications
                     WHERE CommunicationType = @CommunicationType
                     AND IsActive = 1
                     ORDER BY CommunicationDate DESC";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Communication>(query, new { CommunicationType = communicationType });
    }

    public async Task<IEnumerable<Communication>> GetByCampaignAsync(string campaignName)
    {
        var query = @"SELECT * FROM Communications
                     WHERE CampaignName = @CampaignName
                     AND IsBulkCommunication = 1
                     AND IsActive = 1
                     ORDER BY CommunicationDate DESC";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Communication>(query, new { CampaignName = campaignName });
    }

    public async Task<IEnumerable<Communication>> GetRecentCommunicationsAsync(int days = 30)
    {
        var query = @"SELECT c.*,
                            CASE
                                WHEN c.CustomerId IS NOT NULL THEN cust.EntityName
                                WHEN c.LeadId IS NOT NULL THEN l.LeadCode
                            END as RelatedEntity
                     FROM Communications c
                     LEFT JOIN Customers cust ON c.CustomerId = cust.CustomerId
                     LEFT JOIN Leads l ON c.LeadId = l.LeadId
                     WHERE c.CommunicationDate >= DATEADD(day, -@Days, GETDATE())
                     AND c.IsActive = 1
                     ORDER BY c.CommunicationDate DESC";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Communication>(query, new { Days = days });
    }

    public async Task<int> AddAsync(Communication entity)
    {
        var query = @"INSERT INTO Communications
                     (CustomerId, LeadId, CommunicationDate, CommunicationType, Direction, Subject, Summary,
                      NextActionRequired, NextActionDate, IsBulkCommunication, CampaignName, TemplateUsed,
                      DeliveryStatus, IsActive, CreatedDate, CreatedBy)
                     VALUES (@CustomerId, @LeadId, @CommunicationDate, @CommunicationType, @Direction, @Subject, @Summary,
                             @NextActionRequired, @NextActionDate, @IsBulkCommunication, @CampaignName, @TemplateUsed,
                             @DeliveryStatus, @IsActive, @CreatedDate, @CreatedBy);
                     SELECT CAST(SCOPE_IDENTITY() as int)";
        using var connection = _context.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(query, entity);
    }

    public async Task<bool> UpdateAsync(Communication entity)
    {
        var query = @"UPDATE Communications
                     SET Subject = @Subject,
                         Summary = @Summary,
                         NextActionRequired = @NextActionRequired,
                         NextActionDate = @NextActionDate,
                         DeliveryStatus = @DeliveryStatus,
                         ModifiedDate = @ModifiedDate,
                         ModifiedBy = @ModifiedBy
                     WHERE CommunicationId = @CommunicationId";
        using var connection = _context.CreateConnection();
        var result = await connection.ExecuteAsync(query, entity);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var query = "UPDATE Communications SET IsActive = 0 WHERE CommunicationId = @Id";
        using var connection = _context.CreateConnection();
        var result = await connection.ExecuteAsync(query, new { Id = id });
        return result > 0;
    }
}
