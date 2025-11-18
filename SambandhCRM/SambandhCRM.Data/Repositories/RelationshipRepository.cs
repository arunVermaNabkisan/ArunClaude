using Dapper;
using SambandhCRM.Core.Interfaces;
using SambandhCRM.Core.Models;
using SambandhCRM.Data.Context;

namespace SambandhCRM.Data.Repositories;

public class RelationshipRepository : IRelationshipRepository
{
    private readonly DapperContext _context;

    public RelationshipRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<IndividualOrganization>> GetAllAsync()
    {
        var query = "SELECT * FROM IndividualOrganizations ORDER BY CreatedDate DESC";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<IndividualOrganization>(query);
    }

    public async Task<IndividualOrganization?> GetByIdAsync(int id)
    {
        var query = "SELECT * FROM IndividualOrganizations WHERE IndividualOrganizationId = @Id";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<IndividualOrganization>(query, new { Id = id });
    }

    public async Task<IEnumerable<IndividualOrganization>> GetByCustomerIdAsync(int customerId)
    {
        var query = @"SELECT io.*, i.FullName, i.MobileNumber, i.Email, i.PANNumber, c.EntityName as OrganizationName
                     FROM IndividualOrganizations io
                     INNER JOIN Individuals i ON io.IndividualId = i.IndividualId
                     INNER JOIN Customers c ON io.CustomerId = c.CustomerId
                     WHERE io.CustomerId = @CustomerId
                     ORDER BY io.IsStillActive DESC, io.RoleStartDate DESC";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<IndividualOrganization>(query, new { CustomerId = customerId });
    }

    public async Task<IEnumerable<IndividualOrganization>> GetByIndividualIdAsync(int individualId)
    {
        var query = @"SELECT io.*, i.FullName, i.MobileNumber, i.Email, c.EntityName as OrganizationName
                     FROM IndividualOrganizations io
                     INNER JOIN Individuals i ON io.IndividualId = i.IndividualId
                     INNER JOIN Customers c ON io.CustomerId = c.CustomerId
                     WHERE io.IndividualId = @IndividualId
                     ORDER BY io.IsStillActive DESC, io.RoleStartDate DESC";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<IndividualOrganization>(query, new { IndividualId = individualId });
    }

    public async Task<IEnumerable<IndividualOrganization>> GetActiveRelationshipsAsync()
    {
        var query = @"SELECT io.*, i.FullName, i.MobileNumber, i.Email, c.EntityName as OrganizationName
                     FROM IndividualOrganizations io
                     INNER JOIN Individuals i ON io.IndividualId = i.IndividualId
                     INNER JOIN Customers c ON io.CustomerId = c.CustomerId
                     WHERE io.IsStillActive = 1
                     ORDER BY io.CreatedDate DESC";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<IndividualOrganization>(query);
    }

    public async Task<int> AddAsync(IndividualOrganization entity)
    {
        var query = @"INSERT INTO IndividualOrganizations
                     (IndividualId, CustomerId, RoleInOrganization, RoleSpecification, RoleStartDate,
                      IsStillActive, RoleEndDate, IsDecisionMaker, IsPreferredContact, CreatedDate, CreatedBy)
                     VALUES (@IndividualId, @CustomerId, @RoleInOrganization, @RoleSpecification, @RoleStartDate,
                             @IsStillActive, @RoleEndDate, @IsDecisionMaker, @IsPreferredContact, @CreatedDate, @CreatedBy);
                     SELECT CAST(SCOPE_IDENTITY() as int)";
        using var connection = _context.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(query, entity);
    }

    public async Task<bool> UpdateAsync(IndividualOrganization entity)
    {
        var query = @"UPDATE IndividualOrganizations
                     SET RoleInOrganization = @RoleInOrganization,
                         RoleSpecification = @RoleSpecification,
                         RoleStartDate = @RoleStartDate,
                         IsStillActive = @IsStillActive,
                         RoleEndDate = @RoleEndDate,
                         IsDecisionMaker = @IsDecisionMaker,
                         IsPreferredContact = @IsPreferredContact,
                         ModifiedDate = @ModifiedDate,
                         ModifiedBy = @ModifiedBy
                     WHERE IndividualOrganizationId = @IndividualOrganizationId";
        using var connection = _context.CreateConnection();
        var result = await connection.ExecuteAsync(query, entity);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var query = "DELETE FROM IndividualOrganizations WHERE IndividualOrganizationId = @Id";
        using var connection = _context.CreateConnection();
        var result = await connection.ExecuteAsync(query, new { Id = id });
        return result > 0;
    }
}
