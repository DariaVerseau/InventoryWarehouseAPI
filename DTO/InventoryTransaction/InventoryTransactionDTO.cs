using System.Text.Json.Serialization;
using DTO.Product;
using DTO.Warehouse;
using Shared.Enums;

namespace DTO.InventoryTransaction;


public class InventoryTransactionDto
{
    public Guid Id { get; set; }
    public ProductShortDto? Product { get; set; }
    
    public WarehouseShortDto? Warehouse { get; set; }
    public int Quantity { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TransactionType TransactionType { get; set; }
  
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}