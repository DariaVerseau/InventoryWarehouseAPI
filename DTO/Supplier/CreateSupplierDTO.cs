using System.ComponentModel.DataAnnotations;
using DTO.Product;

namespace DTO.Supplier;

public class CreateSupplierDto
{
    
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string Phone { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    
    public List<Guid> ProductIds { get; set; } = new ();
}