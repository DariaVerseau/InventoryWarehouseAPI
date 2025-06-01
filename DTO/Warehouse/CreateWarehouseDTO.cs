using System.ComponentModel.DataAnnotations;
using DTO.InventoryTransaction;

namespace DTO.Warehouse;

public class CreateWarehouseDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Location { get; set; } = string.Empty;
    
    public List<InventoryDto> InventoryItems { get; set; } = new(); //навигационное свойство с Inventory
    public IEnumerable<InventoryTransactionDto>? InventoryTransactions { get; set; }
}