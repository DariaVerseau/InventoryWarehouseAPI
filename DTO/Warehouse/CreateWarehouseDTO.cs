using System.ComponentModel.DataAnnotations;
using DTO.Inventory;

namespace DTO.Warehouse;
public class CreateWarehouseDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Location { get; set; } = string.Empty;
    
    public List<CreateInventoryDto> InventoryItems { get; set; } = new(); // Инициализация по умолчанию
  
}