using System.ComponentModel.DataAnnotations;

public class CreateInventoryDto
{
    
    [Required]
    [Range(1, int.MaxValue)]
    public Guid ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public Guid WarehouseId { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }
    
}

