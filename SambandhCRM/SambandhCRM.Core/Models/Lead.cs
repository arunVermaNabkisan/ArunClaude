using System.ComponentModel.DataAnnotations;

namespace SambandhCRM.Core.Models;

public class Lead
{
    public int LeadId { get; set; }

    [Required]
    [StringLength(20)]
    public string LeadCode { get; set; } = string.Empty;

    [Required]
    public int CustomerId { get; set; }

    [Required]
    [StringLength(50)]
    public string LeadSource { get; set; } = string.Empty;

    [StringLength(255)]
    public string? LeadSourceDetails { get; set; }

    [Required]
    [StringLength(100)]
    public string ProductInterest { get; set; } = string.Empty;

    public decimal? LoanAmountMin { get; set; }

    public decimal? LoanAmountMax { get; set; }

    [Required]
    [StringLength(20)]
    public string Priority { get; set; } = "Medium";

    [Required]
    [StringLength(50)]
    public string LeadStatus { get; set; } = "New";

    [StringLength(500)]
    public string? DropReason { get; set; }

    // Assignment
    public int? AssignedToUserId { get; set; }

    public DateTime? AssignedDate { get; set; }

    public int? AssignedBy { get; set; }

    // Follow-up
    public DateTime? LastContactDate { get; set; }

    public DateTime? NextFollowUpDate { get; set; }

    public string? Notes { get; set; }

    // Document Checklist
    public bool? HasKYCDocuments { get; set; }

    public bool? HasFinancialStatements { get; set; }

    public bool? HasBusinessDocuments { get; set; }

    public bool? HasOtherDocuments { get; set; }

    // Conversion
    public DateTime? ConvertedDate { get; set; }

    public int? ConvertedBy { get; set; }

    // Audit
    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }
}
