using System.ComponentModel.DataAnnotations;

namespace DTO.Warehouse;

public class UpdateWarehouseDto
{
    public Guid Id { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Location { get; set; } = string.Empty;
    
    public List<Guid> InventoryItemIds { get; set; } = new(); //навигационное свойство с Inventory
    public IEnumerable<Guid>? InventoryTransactionIds { get; set; }
}