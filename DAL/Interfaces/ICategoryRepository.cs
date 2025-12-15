using DAL.Entities;

namespace DAL.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetByNameAsync(string name);
    Task<List<Category>> GetVisibleCategoriesAsync();
    Task<bool> ExistsAsync(Guid id);
    Task<(List<Category> Items, long TotalCount)> GetPagedAsync(int page, int pageSize);
}