using DAL.EF;
using DAL.Entities;
using DAL.Helpers;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    private IQueryable<Product> GetBaseQuery()
    {
        return _dbSet
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Include(p => p.InventoryRecords)
            .ThenInclude(i => i.Warehouse);
    }

    public async Task<List<Product>> GetAllWithDetailsAsync()
    {
        return await GetBaseQuery().OrderBy(p => p.Name).ToListAsync();
    }

    public async Task<Product?> GetByIdWithDetailsAsync(Guid id)
    {
        return await GetBaseQuery().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Product>> GetByCategoryIdAsync(Guid categoryId)
    {
        return await GetBaseQuery()
            .Where(p => p.CategoryId == categoryId)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<List<Product>> GetBySupplierIdAsync(Guid supplierId)
    {
        return await GetBaseQuery()
            .Where(p => p.SupplierId == supplierId)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<int> GetTotalStockQuantityAsync(Guid productId)
    {
        return await _context.Set<Inventory>()
            .AsNoTracking()
            .Where(i => i.ProductId == productId)
            .SumAsync(i => i.Quantity);
    }
    
    public async Task<(List<Product> Items, long TotalCount)> GetFilteredAsync(
        string? search,
        Guid? categoryId,
        Guid? supplierId,
        bool? isVisible,
        string? sortBy,
        int page,
        int pageSize)
    {
        var query = _dbSet
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .AsNoTracking();

        //Фильтрация
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
    
        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId.Value);
    
        if (supplierId.HasValue)
            query = query.Where(p => p.SupplierId == supplierId.Value);
    
        if (isVisible.HasValue)
            query = query.Where(p => p.IsVisible == isVisible.Value);

        //Общее количество ДО пагинации
        var totalCount = await query.CountAsync();

        //Сортировка
        query = sortBy?.ToLower() switch
        {
            "name desc" => query.OrderByDescending(p => p.Name),
            "createdAt" => query.OrderBy(p => p.CreatedAt),
            "createdAt desc" => query.OrderByDescending(p => p.CreatedAt),
            _ => query.OrderBy(p => p.Name) // по умолчанию
        };

        // Пагинация
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
    
    public async Task<(List<Product> Items, long TotalCount)> GetPagedAsync(int page, int pageSize)
    {
        var query = _dbSet
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .AsNoTracking()
            .OrderBy(p => p.Name);

        return await PaginationHelper.PaginateAsync(query, page, pageSize);
    }
}