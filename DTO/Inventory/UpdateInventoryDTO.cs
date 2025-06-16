using System.ComponentModel.DataAnnotations;

namespace DTO.Inventory;

public class UpdateInventoryDto
{
    public Guid Id { get; set; }
    
    
    [Required]
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }
    
    [Required]
    public Guid ProductId { get; set; }

    [Required]
    public Guid WarehouseId { get; set; }
    
}