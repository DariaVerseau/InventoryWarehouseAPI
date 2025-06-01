using System.ComponentModel.DataAnnotations;
using DTO.Category;
using DTO.Inventory;
using DTO.InventoryTransaction;
using DTO.Supplier;

namespace DTO.Product;

public class ProductDto 
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Unit { get; set; } = string.Empty;
    [Required]
    public Guid? CategoryId { get; set; }
   public CategoryShortDto? Category { get; set; }
    [Required]
    public Guid? SupplierId { get; set; }
    public SupplierShortDto? Supplier { get; set; }
    
    // Навигационное свойство к остаткам
    public List<InventoryShortDto> InventoryRecords { get; set; } = new();
    public IEnumerable<InventoryTransactionDto>? InventoryTransactions { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}