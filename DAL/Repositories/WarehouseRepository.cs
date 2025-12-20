using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class WarehouseRepository : Repository<Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(AppDbContext context) : base(context) { }

    public async Task<List<Warehouse>> GetAllWithInventoryAsync()
    {
        return await _dbSet
            .Include(w => w.InventoryItems)
            .ThenInclude(i => i.Product)
            .OrderBy(w => w.Name)
            .ToListAsync();
    }

    public async Task<Warehouse?> GetByIdWithInventoryAsync(Guid id)
    {
        return await _dbSet
            .Include(w => w.InventoryItems)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<List<Warehouse>> SearchByNameAsync(string searchTerm)
    {
        // Для PostgreSQL
        return await _dbSet
            .Where(w => w.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToListAsync();

        // Для других СУБД замени на:
        // .Where(w => w.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
    }

    public async Task<int> GetTotalCapacityUsedAsync(Guid warehouseId)
    {
        return await _context.Set<Inventory>()
            .Where(i => i.WarehouseId == warehouseId)
            .SumAsync(i => i.Quantity);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbSet.AnyAsync(w => w.Id == id);
    }
    public async Task<(List<Warehouse> Items, long TotalCount)> GetFilteredAsync(
        string? search,
        string? sortBy,
        int page,
        int pageSize)
    {
        var query = _dbSet
            .Include(w => w.InventoryItems)
            .ThenInclude(i => i.Product)
            .AsNoTracking();

        //Фильтрация
        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim().ToLower();
            query = query.Where(w =>
                w.Name.ToLower().Contains(search) ||
                w.Location.ToLower().Contains(search));
        }

        //Общее количество
        var totalCount = await query.CountAsync();

        //Сортировка
        query = sortBy?.ToLower() switch
        {
            "name desc" => query.OrderByDescending(w => w.Name),
            "location" => query.OrderBy(w => w.Location),
            "location desc" => query.OrderByDescending(w => w.Location),
            _ => query.OrderBy(w => w.Name)
        };

        //Пагинация
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
    
    public async Task<(List<Warehouse> Items, long TotalCount)> GetPagedAsync(int page, int pageSize)
    {
        var query = _dbSet
            .Include(w => w.InventoryItems)
            .ThenInclude(i => i.Product)
            .AsNoTracking()
            .OrderBy(w => w.Name);

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}