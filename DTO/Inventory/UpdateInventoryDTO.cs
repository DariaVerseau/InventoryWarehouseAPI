using System.ComponentModel.DataAnnotations;

namespace DTO.Inventory;

public class UpdateInventoryDto
{
    [Required]
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }
    
}