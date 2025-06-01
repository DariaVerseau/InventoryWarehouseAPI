using DTO.InventoryTransaction;

namespace BLL.Services;

public interface IInventoryTransactionService
{
    Task<InventoryTransactionDto> ProcessTransactionAsync(InventoryTransactionDto transactionDto);
    Task<List<InventoryTransactionDto>> GetByProductAsync(Guid productId);
    Task<List<InventoryTransactionDto>> GetByWarehouseAsync(Guid warehouseId);
    Task<List<InventoryTransactionDto>> GetByDateRangeAsync(DateTime start, DateTime end);
}