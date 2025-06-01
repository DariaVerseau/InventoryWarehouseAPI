using System.ComponentModel.DataAnnotations;
using DTO.Inventory;
using DTO.InventoryTransaction;

namespace DTO.Warehouse;

public class WarehouseDto
{
    public Guid Id { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Location { get; set; } = string.Empty;
    
    public List<InventoryDto> InventoryItems { get; set; } = new(); //навигационное свойство с Inventory
    public IEnumerable<InventoryTransactionDto>? InventoryTransactions { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}