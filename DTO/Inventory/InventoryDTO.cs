using System.ComponentModel.DataAnnotations;
using DTO.Product;
using DTO.Warehouse;

public class InventoryDto
{
    public Guid Id { get; set; }
    
    [Required]
    public Guid ProductId { get; set; }
    
    public ProductDto? Product { get; set; }
    
    [Required]
    public Guid WarehouseId { get; set; }
    
    public WarehouseDto? Warehouse { get; set; }
    
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
    public int Quantity { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}



