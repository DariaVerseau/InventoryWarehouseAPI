namespace DTO.InventoryTransaction;

public class InventoryTransactionShortDto
{
    public Guid Id { get; set; }
    //public TransactionType TransactionType { get; set; }
    public int Quantity { get; set; }
    public DateTime TransactionDate { get; set; }
}