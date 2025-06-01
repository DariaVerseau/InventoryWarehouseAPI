using System.ComponentModel.DataAnnotations;

namespace DTO.Product;

public class ProductShortDto
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Unit { get; set; } = string.Empty;
    
}