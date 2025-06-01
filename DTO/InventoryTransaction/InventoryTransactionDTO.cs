using DTO.Product;
using DTO.Warehouse;

namespace DTO.InventoryTransaction;

public enum TransactionType
{
    Incoming,
    Outgoing,
    Transfer
}
public class InventoryTransactionDto
{
    public Guid Id { get; set; }
    //public Guid? ProductId { get; set; }
    public ProductShortDto? Product { get; set; }
    //public Guid? WarehouseId { get; set; }
    public WarehouseShortDto? Warehouse { get; set; }
    public int Quantity { get; set; }
    public TransactionType TransactionType { get; set; }
  
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}