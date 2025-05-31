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
    public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

/*
 * 
 */