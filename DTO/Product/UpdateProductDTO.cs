using System.ComponentModel.DataAnnotations;
using DTO.Category;
using DTO.Supplier;

namespace DTO.Product;

public class UpdateProductDto
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Unit { get; set; } = string.Empty;
    public Guid? CategoryId { get; set; }
    public CategoryDto? Category { get; set; }
    public Guid? SupplierId { get; set; }
    public SupplierDto? Supplier { get; set; }
    
    public List<Guid> InventoryIds { get; set; } = new();
    public IEnumerable<Guid>? InventoryTransactionIds { get; set; } 
}