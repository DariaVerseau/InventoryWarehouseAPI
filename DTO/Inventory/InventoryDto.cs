using System.ComponentModel.DataAnnotations;
using DTO.Product;
using DTO.Warehouse;

namespace DTO.Inventory;

public class InventoryDto
{
    public Guid Id { get; set; }
    public ProductShortDto? Product { get; set; }
    public WarehouseShortDto? Warehouse { get; set; }
    
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
    public int Quantity { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}