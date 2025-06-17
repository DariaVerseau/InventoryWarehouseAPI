using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DTO.Product;
using DTO.Warehouse;
using Shared.Enums;

namespace DTO.InventoryTransaction;

public class UpdateInventoryTransactionDto
{
    public Guid Id { get; set; }
    
    public Guid? ProductId { get; set; }
    
    public Guid? WarehouseId { get; set; }

    public int Quantity { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TransactionType TransactionType { get; set; }
  
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
}