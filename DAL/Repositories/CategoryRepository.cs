using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context) { }


    /// Проверяет существование категории по ID (без загрузки данных).

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbSet.AnyAsync(c => c.Id == id);
    }
    
    /// Находит категорию по точному совпадению имени (регистронезависимо для PostgreSQL).
  
    public async Task<Category?> GetByNameAsync(string name)
    {
        // Для PostgreSQL
        return await _dbSet
            .FirstOrDefaultAsync(c =>  c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        
    }
    
    /// Возвращает только видимые категории, упорядоченные по имени.

    public async Task<List<Category>> GetVisibleCategoriesAsync()
    {
        return await _dbSet
            .Where(c => c.IsVisible)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }
    
    public async Task<(List<Category> Items, long TotalCount)> GetPagedAsync(int page, int pageSize)
    {
        var query = _dbSet
            .AsNoTracking()
            .OrderBy(c => c.Name);

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
    
}