using System.ComponentModel.DataAnnotations;

namespace DTO.Inventory;

public class CreateInventoryDto
{
    
    [Required]
    public Guid ProductId { get; set; }

    [Required]
    public Guid WarehouseId { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }
    
}