using System.ComponentModel.DataAnnotations;

namespace SambandhCRM.Core.Models;

public class Individual
{
    public int IndividualId { get; set; }

    [Required]
    [StringLength(255)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [Phone]
    [StringLength(15)]
    public string MobileNumber { get; set; } = string.Empty;

    [EmailAddress]
    [StringLength(255)]
    public string? Email { get; set; }

    [StringLength(10)]
    public string? PANNumber { get; set; }

    [StringLength(8)]
    public string? DINNumber { get; set; }

    [StringLength(255)]
    public string? LinkedInProfile { get; set; }

    [Phone]
    [StringLength(15)]
    public string? AlternatePhone { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }
}
