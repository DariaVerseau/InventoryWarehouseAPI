
using DTO.InventoryTransaction;

namespace BLL.Interfaces;

public interface IInventoryTransactionService
{
    Task<List<InventoryTransactionDto>> GetTransactions();
    Task<InventoryTransactionDto> GetTransaction(Guid id);
    Task<InventoryTransactionDto> CreateTransaction(CreateInventoryTransactionDto transaction);
    Task<InventoryTransactionDto> UpdateTransaction(UpdateInventoryTransactionDto transaction);
    Task DeleteTransaction(Guid id);
}