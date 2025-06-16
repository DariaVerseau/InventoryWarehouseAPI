using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DTO.Product;
using DTO.Warehouse;

namespace DTO.InventoryTransaction;

public class CreateInventoryTransactionDto
{
    [Required(ErrorMessage = "ProductId is required")]
    public Guid ProductId { get; set; } // Убрали nullable

    [Required]
    public Guid WarehouseId { get; set; }
 
    public int Quantity { get; set; }
    
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TransactionType TransactionType { get; set; } = TransactionType.Incoming;
    
  
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
}