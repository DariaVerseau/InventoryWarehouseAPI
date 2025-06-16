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
    public int TotalQuantity { get; set; }
   public CategoryShortDto? Category { get; set; }
    public SupplierShortDto? Supplier { get; set; }
    public bool IsVisible { get; set; }
    
    // Навигационное свойство к остаткам
    public List<InventoryShortDto> InventoryRecords { get; set; } = new();
    public List<InventoryTransactionDto>? InventoryTransactions { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}