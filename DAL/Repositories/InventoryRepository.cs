using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class InventoryRepository : Repository<Inventory>, IInventoryRepository
{
    public InventoryRepository(AppDbContext context) : base(context) { }


    /// Получает все записи Inventory с загруженными связями Product и Warehouse.
    /// Используется, когда нужны данные для маппинга в InventoryDto.

    public async Task<List<Inventory>> GetAllWithDetails()
    {
        return await _dbSet
            .Include(i => i.Product)
            .Include(i => i.Warehouse)
            .ToListAsync();
    }
    
    public async Task<List<Inventory>> GetByProductId(Guid productId)
    {
        return await _dbSet
            .Include(i => i.Product)
            .Include(i => i.Warehouse)
            .Where(i => i.ProductId == productId)
            .ToListAsync();
    }
    public async Task<int> GetTotalQuantity(Guid productId)
    {
        return await _dbSet
            .Where(i => i.ProductId == productId)
            .SumAsync(i => i.Quantity);
    }

    /// Получает все записи Inventory для указанного склада с полными данными.

    public async Task<List<Inventory>> GetByWarehouseId(Guid warehouseId)
    {
        return await _dbSet
            .Include(i => i.Product)
            .Include(i => i.Warehouse)
            .Where(i => i.WarehouseId == warehouseId)
            .ToListAsync();
    }


    /// Получает запись Inventory по комбинации ProductId и WarehouseId.
  
    public async Task<Inventory?> GetByProductAndWarehouse(Guid productId, Guid warehouseId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(i => i.ProductId == productId && i.WarehouseId == warehouseId);
    }

    
    /// Удаляет все записи Inventory для указанного товара (например, при удалении Product).
    
    public async Task DeleteByProductId(Guid productId)
    {
        var inventories = await _dbSet.Where(i => i.ProductId == productId).ToListAsync();
        _dbSet.RemoveRange(inventories);
        await _context.SaveChangesAsync();
    }


    /// Удаляет все записи Inventory для указанного склада (например, при удалении Warehouse).
    
    public async Task DeleteByWarehouseId(Guid warehouseId)
    {
        var inventories = await _dbSet.Where(i => i.WarehouseId == warehouseId).ToListAsync();
        _dbSet.RemoveRange(inventories);
        await _context.SaveChangesAsync();
    }
    
    public async Task<(List<Inventory> Items, long TotalCount)> GetPagedAsync(int page, int pageSize)
    {
        var query = _dbSet
            .Include(i => i.Product)
            .Include(i => i.Warehouse)
            .AsNoTracking()
            .OrderBy(i => i.Product!.Name); // сортировка по имени товара

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
    
    public async Task<(List<Inventory> Items, long TotalCount)> GetFilteredAsync(
        string? productName,
        Guid? productId,
        Guid? warehouseId,
        int? minQuantity,
        int? maxQuantity,
        string? sortBy,
        int page,
        int pageSize)
    {
        var query = _dbSet
            .Include(i => i.Product)
            .Include(i => i.Warehouse)
            .AsNoTracking();

        //Фильтрация
        if (!string.IsNullOrWhiteSpace(productName))
        {
            productName = productName.Trim().ToLower();
            query = query.Where(i => i.Product.Name.ToLower().Contains(productName));
        }

        if (productId.HasValue)
            query = query.Where(i => i.ProductId == productId.Value);

        if (warehouseId.HasValue)
            query = query.Where(i => i.WarehouseId == warehouseId.Value);

        if (minQuantity.HasValue)
            query = query.Where(i => i.Quantity >= minQuantity.Value);

        if (maxQuantity.HasValue)
            query = query.Where(i => i.Quantity <= maxQuantity.Value);

        //Общее количество
        var totalCount = await query.CountAsync();

        //Сортировка
        query = sortBy?.ToLower() switch
        {
            "productName desc" => query.OrderByDescending(i => i.Product.Name),
            "warehouseName" => query.OrderBy(i => i.Warehouse.Name),
            "warehouseName desc" => query.OrderByDescending(i => i.Warehouse.Name),
            "quantity" => query.OrderBy(i => i.Quantity),
            "quantity desc" => query.OrderByDescending(i => i.Quantity),
            _ => query.OrderBy(i => i.Product.Name)
        };

        //Пагинация
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}