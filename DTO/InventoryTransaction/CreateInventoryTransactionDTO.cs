using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DTO.Product;
using DTO.Warehouse;
using Shared.Enums;

namespace DTO.InventoryTransaction;

public class CreateInventoryTransactionDto
{
    [Required(ErrorMessage = "ProductId is required")]
    public Guid ProductId { get; set; } // Убрали nullable

    [Required]
    public Guid WarehouseId { get; set; }
 
    public int Quantity { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TransactionType TransactionType { get; set; } = TransactionType.incoming;
    
  
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
}