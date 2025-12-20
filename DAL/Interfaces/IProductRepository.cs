using DAL.Entities;

namespace DAL.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetAllWithDetailsAsync();
    Task<Product?> GetByIdWithDetailsAsync(Guid id);
    Task<List<Product>> GetByCategoryIdAsync(Guid categoryId);
    Task<List<Product>> GetBySupplierIdAsync(Guid supplierId);
    Task<int> GetTotalStockQuantityAsync(Guid productId);
    Task<(List<Product> Items, long TotalCount)> GetFilteredAsync(
        string? search = null,
        Guid? categoryId = null,
        Guid? supplierId = null,
        bool? isVisible = null,
        string? sortBy = null,
        int page = 1,
        int pageSize = 10);
    Task<(List<Product> Items, long TotalCount)> GetPagedAsync(int page, int pageSize);
}