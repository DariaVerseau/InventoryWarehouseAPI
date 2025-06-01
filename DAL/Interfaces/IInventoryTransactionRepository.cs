using DAL.Entities;
using DTO.InventoryTransaction;

namespace DAL.Interfaces;

public interface IInventoryTransactionRepository : IRepository<InventoryTransactionDto, CreateInventoryTransactionDto, UpdateInventoryTransactionDto>
{
    Task<List<InventoryTransactionDto>> GetByProductId(Guid productId);
    Task<List<InventoryTransactionDto>> GetByWarehouseId(Guid warehouseId);
    //Task<List<InventoryTransactionDto>> GetByType(TransactionType type);
    //Task<int> GetProductQuantityHistory(Guid productId);

}