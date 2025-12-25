using System.ComponentModel.DataAnnotations;
using DTO.Product;

namespace DTO.Category;

public class CategoryDto
{
    public Guid Id { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = String.Empty;
    [StringLength(500)]
    public string Description { get; set; } = String.Empty;
    public List<ProductShortDto> Products { get; set; } = new(); // вместо List<ProductDto>
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public bool IsVisible { get; set; }
    
}
