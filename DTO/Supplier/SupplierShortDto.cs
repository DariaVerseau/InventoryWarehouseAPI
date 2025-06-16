using System.ComponentModel.DataAnnotations;

namespace DTO.Supplier;

public class SupplierShortDto
{
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;
    
}