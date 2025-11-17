using System.ComponentModel.DataAnnotations;

namespace SambandhCRM.Core.Models;

public class MasterData
{
    public int MasterDataId { get; set; }

    [Required]
    [StringLength(50)]
    public string Category { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string Value { get; set; } = string.Empty;

    public int DisplayOrder { get; set; } = 0;

    public bool IsActive { get; set; } = true;

    public bool IsSystemDefined { get; set; } = false;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }
}
