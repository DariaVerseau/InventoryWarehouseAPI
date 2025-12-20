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
    Task<(List<Inventory> Items, long TotalCount)> GetFilteredAsync(
        string? productName = null,
        Guid? productId = null,
        Guid? warehouseId = null,
        int? minQuantity = null,
        int? maxQuantity = null,
        string? sortBy = null,
        int page = 1,
        int pageSize = 10);
    Task<(List<Inventory> Items, long TotalCount)> GetPagedAsync(int page, int pageSize);
}