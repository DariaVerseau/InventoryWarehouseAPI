using DAL.Entities;

namespace DAL.Interfaces;

public interface IWarehouseRepository : IRepository<Warehouse>
{
    Task<List<Warehouse>> GetAllWithInventoryAsync();
    Task<Warehouse?> GetByIdWithInventoryAsync(Guid id);
    Task<List<Warehouse>> SearchByNameAsync(string searchTerm);
    Task<int> GetTotalCapacityUsedAsync(Guid warehouseId);
    Task<bool> ExistsAsync(Guid id);
    Task<(List<Warehouse> Items, long TotalCount)> GetFilteredAsync(
        string? search = null,
        string? sortBy = null,
        int page = 1,
        int pageSize = 10);
    Task<(List<Warehouse> Items, long TotalCount)> GetPagedAsync(int page, int pageSize);
}