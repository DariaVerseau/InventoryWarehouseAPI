using DAL.Entities;

namespace DAL.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetByNameAsync(string name);
    Task<List<Category>> GetVisibleCategoriesAsync();
    Task<bool> ExistsAsync(Guid id);
    Task<List<Category>> SearchByNameAsync(string searchTerm);
    Task<(List<Category> Items, long TotalCount)> GetPagedAsync(int page, int pageSize);
    // DAL/Interfaces/ICategoryRepository.cs
    Task<(List<Category> Items, long TotalCount)> GetFilteredAsync(
        string? search = null,
        bool? isVisible = null,
        string? sortBy = null,
        int page = 1,
        int pageSize = 5);
}