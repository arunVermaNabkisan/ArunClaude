using System.ComponentModel.DataAnnotations;

namespace SambandhCRM.Core.Models;

public class Communication
{
    public int CommunicationId { get; set; }

    public int? CustomerId { get; set; }

    public int? LeadId { get; set; }

    [Required]
    public DateTime CommunicationDate { get; set; } = DateTime.Now;

    [Required]
    [StringLength(50)]
    public string CommunicationType { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Direction { get; set; } = string.Empty;

    [StringLength(255)]
    public string? Subject { get; set; }

    public string? Summary { get; set; }

    [StringLength(500)]
    public string? NextActionRequired { get; set; }

    public DateTime? NextActionDate { get; set; }

    // For bulk communications
    public bool IsBulkCommunication { get; set; } = false;

    [StringLength(255)]
    public string? CampaignName { get; set; }

    [StringLength(100)]
    public string? TemplateUsed { get; set; }

    [StringLength(50)]
    public string? DeliveryStatus { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }
}
