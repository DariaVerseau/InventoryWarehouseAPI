using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
//using EfCore = Microsoft.EntityFrameworkCore;
//using Npgsql;

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
    
    public async Task<List<Category>> SearchByNameAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new List<Category>();

        var term = searchTerm.Trim().ToLower();
        return await _dbSet
            .Where(c => c.Name.Trim().ToLower().Contains(term))
            .ToListAsync();
    }

    public async Task<(List<Category> Items, long TotalCount)> GetFilteredAsync(
        string? search,
        bool? isVisible,
        string? sortBy,
        int page,
        int pageSize)
    {
        var query = _dbSet.AsNoTracking();

        //Фильтрация
        if (!string.IsNullOrWhiteSpace(search))
        {
            var cleanSearch = search.Trim().ToLower();
            query = query.Where(c => 
                c.Name.Trim().ToLower().Contains(cleanSearch) ||
                c.Description.Trim().ToLower().Contains(cleanSearch));
        }

        if (isVisible.HasValue)
        {
            query = query.Where(c => c.IsVisible == isVisible.Value);
        }

        // Общее количество до пагинации
        var totalCount = await query.CountAsync();

        // Сортировка
        if (!string.IsNullOrEmpty(sortBy))
        {
            var sort = sortBy.ToLower();
            if (sort == "createdat desc")
                query = query.OrderByDescending(c => c.CreatedAt);
            else if (sort == "createdat")
                query = query.OrderBy(c => c.CreatedAt);
            else if (sort == "name desc")
                query = query.OrderByDescending(c => c.Name);
            else if (sort == "name")
                query = query.OrderBy(c => c.Name);
            else
                query = query.OrderBy(c => c.Name);
        }
        else
        {
            query = query.OrderBy(c => c.Name);
        }

        //Пагинация
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
    
}