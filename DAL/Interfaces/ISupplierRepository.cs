using DAL.Entities;

namespace DAL.Interfaces;

public interface ISupplierRepository : IRepository<Supplier>
{
    Task<List<Supplier>> GetAllWithProductsAsync();
    Task<Supplier?> GetByIdWithProductsAsync(Guid id);
    Task<Supplier?> GetByEmailAsync(string email);
    Task<bool> ExistsAsync(Guid id);
    Task<(List<Supplier> Items, long TotalCount)> GetPagedAsync(int page, int pageSize);
}