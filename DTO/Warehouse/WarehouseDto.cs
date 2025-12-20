using System.ComponentModel.DataAnnotations;
using DTO.Inventory;


namespace DTO.Warehouse;

public class WarehouseDto
{
    public Guid Id { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Location { get; set; } = string.Empty;
    
    public List<InventoryShortDto> InventoryItems { get; set; } = new(); //навигационное свойство с Inventory
   
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}