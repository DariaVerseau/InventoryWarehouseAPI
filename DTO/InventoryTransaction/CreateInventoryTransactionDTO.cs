using DTO.Product;
using DTO.Warehouse;

namespace DTO.InventoryTransaction;

public class CreateInventoryTransactionDto
{
    public Guid? ProductId { get; set; }
    public ProductDto? Product { get; set; }
    public Guid? WarehouseId { get; set; }
    //public WarehouseDto? Warehouse { get; set; }
    public int Quantity { get; set; }
    public TransactionType TransactionType { get; set; }
  
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
}