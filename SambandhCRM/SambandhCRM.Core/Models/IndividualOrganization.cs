using System.ComponentModel.DataAnnotations;

namespace SambandhCRM.Core.Models;

public class IndividualOrganization
{
    public int IndividualOrganizationId { get; set; }

    [Required]
    public int IndividualId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    [StringLength(100)]
    public string RoleInOrganization { get; set; } = string.Empty;

    [StringLength(255)]
    public string? RoleSpecification { get; set; }

    public DateTime? RoleStartDate { get; set; }

    public bool IsStillActive { get; set; } = true;

    public DateTime? RoleEndDate { get; set; }

    public bool IsDecisionMaker { get; set; } = false;

    public bool IsPreferredContact { get; set; } = false;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }
}
