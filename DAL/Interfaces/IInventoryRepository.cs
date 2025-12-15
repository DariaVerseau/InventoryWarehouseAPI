using DAL.Entities;

namespace DAL.Interfaces;

public interface IInventoryRepository : IRepository<Inventory>
{
    Task<List<Inventory>> GetAllWithDetails();
    Task<List<Inventory>> GetByWarehouseId(Guid warehouseId);
    Task<List<Inventory>> GetByProductId(Guid productId);
    Task<Inventory?> GetByProductAndWarehouse(Guid productId, Guid warehouseId);
    Task DeleteByProductId(Guid productId);
    Task DeleteByWarehouseId(Guid warehouseId);
    Task<int> GetTotalQuantity(Guid productId);
    Task<(List<Inventory> Items, long TotalCount)> GetPagedAsync(int page, int pageSize);
}