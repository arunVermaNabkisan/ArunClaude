using System.ComponentModel.DataAnnotations;

namespace SambandhCRM.Core.Models;

public class Document
{
    public int DocumentId { get; set; }

    public int? CustomerId { get; set; }

    public int? LeadId { get; set; }

    [Required]
    [StringLength(100)]
    public string DocumentType { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string DocumentName { get; set; } = string.Empty;

    [StringLength(500)]
    public string? DocumentDescription { get; set; }

    [Required]
    [StringLength(255)]
    public string FileName { get; set; } = string.Empty;

    public long? FileSize { get; set; }

    [StringLength(10)]
    public string? FileExtension { get; set; }

    [Required]
    [StringLength(500)]
    public string FilePath { get; set; } = string.Empty;

    public DateTime UploadDate { get; set; } = DateTime.Now;

    public int? UploadedBy { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public int? CreatedBy { get; set; }
}
