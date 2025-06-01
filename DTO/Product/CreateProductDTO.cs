using System.ComponentModel.DataAnnotations;
using DTO.Category;
using DTO.Supplier;

namespace DTO.Product;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Unit { get; set; } = string.Empty;
    [Required]
    public Guid? CategoryId { get; set; }
    
    [Required]
    public Guid? SupplierId { get; set; }
    
    
    //public List<Guid> InventoryIds { get; set; } = new();
    //public IEnumerable<Guid>? InventoryTransactionIds { get; set; } 
}