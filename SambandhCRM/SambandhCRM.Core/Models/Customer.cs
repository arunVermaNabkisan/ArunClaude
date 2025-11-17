using System.ComponentModel.DataAnnotations;
using SambandhCRM.Core.Enums;

namespace SambandhCRM.Core.Models;

public class Customer
{
    public int CustomerId { get; set; }

    [Required]
    [StringLength(20)]
    public string CustomerCode { get; set; } = string.Empty;

    // Basic Information
    [Required]
    [StringLength(50)]
    public string LegalConstitution { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string EntityName { get; set; } = string.Empty;

    [StringLength(100)]
    public string? RegistrationNumber { get; set; }

    [StringLength(10)]
    public string? PANNumber { get; set; }

    [StringLength(12)]
    public string? AadhaarNumber { get; set; }

    public DateTime? DateOfIncorporation { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [StringLength(100)]
    public string? BusinessSegment { get; set; }

    [StringLength(500)]
    public string? PrimaryBusinessActivity { get; set; }

    [StringLength(100)]
    public string? Occupation { get; set; }

    // Financial Information
    public decimal? AnnualTurnover { get; set; }

    public decimal? AnnualIncome { get; set; }

    [StringLength(50)]
    public string? EmployeeCountRange { get; set; }

    // Contact Information
    [StringLength(500)]
    public string? RegisteredAddress { get; set; }

    [StringLength(6)]
    public string? RegisteredPinCode { get; set; }

    [StringLength(500)]
    public string? OfficeAddress { get; set; }

    [StringLength(6)]
    public string? OfficePinCode { get; set; }

    [StringLength(500)]
    public string? CorrespondenceAddress { get; set; }

    [StringLength(6)]
    public string? CorrespondencePinCode { get; set; }

    [Phone]
    [StringLength(15)]
    public string? OfficePhone { get; set; }

    [Phone]
    [StringLength(15)]
    public string? MobileNumber { get; set; }

    [Phone]
    [StringLength(15)]
    public string? AlternateNumber { get; set; }

    [EmailAddress]
    [StringLength(255)]
    public string? PrimaryEmail { get; set; }

    [EmailAddress]
    [StringLength(255)]
    public string? SecondaryEmail { get; set; }

    [Url]
    [StringLength(255)]
    public string? Website { get; set; }

    [StringLength(255)]
    public string? LinkedInProfile { get; set; }

    [StringLength(100)]
    public string? TwitterHandle { get; set; }

    // Banking Information
    [StringLength(255)]
    public string? PrimaryBankName { get; set; }

    public int? BankingSinceYear { get; set; }

    public bool IsExistingCustomer { get; set; } = false;

    [StringLength(100)]
    public string? ExistingProductType { get; set; }

    public decimal? OutstandingAmount { get; set; }

    [StringLength(500)]
    public string? OtherLenderRelationships { get; set; }

    // Status
    [Required]
    [StringLength(50)]
    public string CustomerStatus { get; set; } = "Prospect";

    // Assignment
    public int? AssignedToUserId { get; set; }

    public DateTime? AssignedDate { get; set; }

    // MCA Integration
    public bool IsMCAVerified { get; set; } = false;

    public DateTime? MCAFetchDate { get; set; }

    public string? MCAData { get; set; }

    // Audit
    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }
}
