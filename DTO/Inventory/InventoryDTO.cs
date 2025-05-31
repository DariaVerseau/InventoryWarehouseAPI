using System.ComponentModel.DataAnnotations;
using DTO.Product;

public class InventoryDto
{
    public Guid Id { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public Guid ProductId { get; set; }
    
    public ProductDto? Product { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public Guid WarehouseId { get; set; }
    
    public WarehouseDto? Warehouse { get; set; }
    
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
    public int Quantity { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}



