using System.ComponentModel.DataAnnotations;

using DTO.Product;
using DTO.Warehouse;

namespace DTO.InventoryTransaction;

public class UpdateInventoryTransactionDto
{
    public Guid Id { get; set; }
    
    [Required]
    public Guid? ProductId { get; set; }
    public ProductDto? Product { get; set; }
    [Required]
    public Guid? WarehouseId { get; set; }
    public WarehouseDto? Warehouse { get; set; }
    public int Quantity { get; set; }
    public TransactionType TransactionType { get; set; }
  
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
}