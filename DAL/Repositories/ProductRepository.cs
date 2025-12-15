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