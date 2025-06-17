using System.ComponentModel.DataAnnotations;
namespace DTO.Product;

public class UpdateProductDto
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Unit { get; set; } = string.Empty;
    
    [Required]
    public int TotalQuantity { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? SupplierId { get; set; }
    public bool IsVisible { get; set; }
    
}