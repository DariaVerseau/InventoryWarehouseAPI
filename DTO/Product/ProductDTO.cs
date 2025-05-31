using System.ComponentModel.DataAnnotations;
using DTO.Category;

namespace DTO.Product;

public class ProductDto
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Unit { get; set; } = string.Empty;
    public int? CategoryId { get; set; }
    public CategoryDto? Category { get; set; }
    public int? SupplierId { get; set; }
    public SupplierDto? Supplier { get; set; }
    
    // Навигационное свойство к остаткам
    public List<InventoryDto> InventoryRecords { get; set; } = new();
    public IEnumerable<InventoryTransactionDto>? InventoryTransactions { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}