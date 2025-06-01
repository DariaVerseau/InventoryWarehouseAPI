using System.ComponentModel.DataAnnotations;
using DTO.Product;

namespace DTO.Supplier;

public class SupplierDto
{
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string Phone { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    
    public List<ProductDto> Products { get; set; } = new ();
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}