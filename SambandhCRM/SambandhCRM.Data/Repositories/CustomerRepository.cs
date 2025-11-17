using Dapper;
using SambandhCRM.Core.Interfaces;
using SambandhCRM.Core.Models;
using SambandhCRM.Data.Context;

namespace SambandhCRM.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly DapperContext _context;

    public CustomerRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        var query = "SELECT * FROM Customers WHERE IsActive = 1 ORDER BY CreatedDate DESC";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Customer>(query);
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        var query = "SELECT * FROM Customers WHERE CustomerId = @Id AND IsActive = 1";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Customer>(query, new { Id = id });
    }

    public async Task<Customer?> GetByCustomerCodeAsync(string customerCode)
    {
        var query = "SELECT * FROM Customers WHERE CustomerCode = @CustomerCode AND IsActive = 1";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Customer>(query, new { CustomerCode = customerCode });
    }

    public async Task<Customer?> GetByPANAsync(string panNumber)
    {
        var query = "SELECT * FROM Customers WHERE PANNumber = @PANNumber AND IsActive = 1";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Customer>(query, new { PANNumber = panNumber });
    }

    public async Task<IEnumerable<Customer>> GetByAssignedUserAsync(int userId)
    {
        var query = "SELECT * FROM Customers WHERE AssignedToUserId = @UserId AND IsActive = 1 ORDER BY CreatedDate DESC";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Customer>(query, new { UserId = userId });
    }

    public async Task<IEnumerable<Customer>> GetByStatusAsync(string status)
    {
        var query = "SELECT * FROM Customers WHERE CustomerStatus = @Status AND IsActive = 1 ORDER BY CreatedDate DESC";
        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Customer>(query, new { Status = status });
    }

    public async Task<IEnumerable<Customer>> SearchAsync(string searchTerm)
    {
        var query = @"
            SELECT * FROM Customers
            WHERE IsActive = 1
            AND (
                EntityName LIKE @SearchTerm
                OR CustomerCode LIKE @SearchTerm
                OR PANNumber LIKE @SearchTerm
                OR MobileNumber LIKE @SearchTerm
                OR PrimaryEmail LIKE @SearchTerm
            )
            ORDER BY CreatedDate DESC";

        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Customer>(query, new { SearchTerm = $"%{searchTerm}%" });
    }

    public async Task<bool> CheckDuplicateAsync(string panNumber, string? registrationNumber, string? mobileNumber)
    {
        var query = @"
            SELECT COUNT(1) FROM Customers
            WHERE IsActive = 1
            AND (
                (@PANNumber IS NOT NULL AND PANNumber = @PANNumber)
                OR (@RegistrationNumber IS NOT NULL AND RegistrationNumber = @RegistrationNumber)
                OR (@MobileNumber IS NOT NULL AND MobileNumber = @MobileNumber)
            )";

        using var connection = _context.CreateConnection();
        var count = await connection.QuerySingleAsync<int>(query, new
        {
            PANNumber = panNumber,
            RegistrationNumber = registrationNumber,
            MobileNumber = mobileNumber
        });
        return count > 0;
    }

    public async Task<int> AddAsync(Customer entity)
    {
        var query = @"
            INSERT INTO Customers (
                CustomerCode, LegalConstitution, EntityName, RegistrationNumber, PANNumber,
                AadhaarNumber, DateOfIncorporation, DateOfBirth, BusinessSegment, PrimaryBusinessActivity,
                Occupation, AnnualTurnover, AnnualIncome, EmployeeCountRange, RegisteredAddress,
                RegisteredPinCode, OfficeAddress, OfficePinCode, CorrespondenceAddress, CorrespondencePinCode,
                OfficePhone, MobileNumber, AlternateNumber, PrimaryEmail, SecondaryEmail,
                Website, LinkedInProfile, TwitterHandle, PrimaryBankName, BankingSinceYear,
                IsExistingCustomer, ExistingProductType, OutstandingAmount, OtherLenderRelationships,
                CustomerStatus, AssignedToUserId, AssignedDate, IsMCAVerified, MCAFetchDate,
                MCAData, IsActive, CreatedDate, CreatedBy
            )
            VALUES (
                @CustomerCode, @LegalConstitution, @EntityName, @RegistrationNumber, @PANNumber,
                @AadhaarNumber, @DateOfIncorporation, @DateOfBirth, @BusinessSegment, @PrimaryBusinessActivity,
                @Occupation, @AnnualTurnover, @AnnualIncome, @EmployeeCountRange, @RegisteredAddress,
                @RegisteredPinCode, @OfficeAddress, @OfficePinCode, @CorrespondenceAddress, @CorrespondencePinCode,
                @OfficePhone, @MobileNumber, @AlternateNumber, @PrimaryEmail, @SecondaryEmail,
                @Website, @LinkedInProfile, @TwitterHandle, @PrimaryBankName, @BankingSinceYear,
                @IsExistingCustomer, @ExistingProductType, @OutstandingAmount, @OtherLenderRelationships,
                @CustomerStatus, @AssignedToUserId, @AssignedDate, @IsMCAVerified, @MCAFetchDate,
                @MCAData, @IsActive, @CreatedDate, @CreatedBy
            );
            SELECT CAST(SCOPE_IDENTITY() as int)";

        using var connection = _context.CreateConnection();
        return await connection.QuerySingleAsync<int>(query, entity);
    }

    public async Task<bool> UpdateAsync(Customer entity)
    {
        var query = @"
            UPDATE Customers
            SET LegalConstitution = @LegalConstitution,
                EntityName = @EntityName,
                RegistrationNumber = @RegistrationNumber,
                PANNumber = @PANNumber,
                AadhaarNumber = @AadhaarNumber,
                DateOfIncorporation = @DateOfIncorporation,
                DateOfBirth = @DateOfBirth,
                BusinessSegment = @BusinessSegment,
                PrimaryBusinessActivity = @PrimaryBusinessActivity,
                Occupation = @Occupation,
                AnnualTurnover = @AnnualTurnover,
                AnnualIncome = @AnnualIncome,
                EmployeeCountRange = @EmployeeCountRange,
                RegisteredAddress = @RegisteredAddress,
                RegisteredPinCode = @RegisteredPinCode,
                OfficeAddress = @OfficeAddress,
                OfficePinCode = @OfficePinCode,
                CorrespondenceAddress = @CorrespondenceAddress,
                CorrespondencePinCode = @CorrespondencePinCode,
                OfficePhone = @OfficePhone,
                MobileNumber = @MobileNumber,
                AlternateNumber = @AlternateNumber,
                PrimaryEmail = @PrimaryEmail,
                SecondaryEmail = @SecondaryEmail,
                Website = @Website,
                LinkedInProfile = @LinkedInProfile,
                TwitterHandle = @TwitterHandle,
                PrimaryBankName = @PrimaryBankName,
                BankingSinceYear = @BankingSinceYear,
                IsExistingCustomer = @IsExistingCustomer,
                ExistingProductType = @ExistingProductType,
                OutstandingAmount = @OutstandingAmount,
                OtherLenderRelationships = @OtherLenderRelationships,
                CustomerStatus = @CustomerStatus,
                AssignedToUserId = @AssignedToUserId,
                AssignedDate = @AssignedDate,
                IsMCAVerified = @IsMCAVerified,
                MCAFetchDate = @MCAFetchDate,
                MCAData = @MCAData,
                ModifiedDate = @ModifiedDate,
                ModifiedBy = @ModifiedBy
            WHERE CustomerId = @CustomerId";

        using var connection = _context.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(query, entity);
        return affectedRows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var query = "UPDATE Customers SET IsActive = 0 WHERE CustomerId = @Id";
        using var connection = _context.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(query, new { Id = id });
        return affectedRows > 0;
    }
}
