using DAL.Entities;
using DTO.Product;

namespace DTO.InventoryTransaction;

public class InventoryTransactionDto
{
    public Guid Id { get; set; }
    public Guid? ProductId { get; set; }
    public ProductDto Product { get; set; }
    public Guid? WarehouseId { get; set; }
    public WarehouseDto Warehouse { get; set; }
    public int Quantity { get; set; }
    public TransactionType TransactionType { get; set; }
  
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}