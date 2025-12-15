using DAL.Entities;

namespace DAL.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetAllWithDetailsAsync();
    Task<Product?> GetByIdWithDetailsAsync(Guid id);
    Task<List<Product>> GetByCategoryIdAsync(Guid categoryId);
    Task<List<Product>> GetBySupplierIdAsync(Guid supplierId);
    Task<int> GetTotalStockQuantityAsync(Guid productId);
    Task<(List<Product> Items, long TotalCount)> GetPagedAsync(int page, int pageSize);
}