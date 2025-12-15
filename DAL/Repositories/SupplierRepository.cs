using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class SupplierRepository : Repository<Supplier>, ISupplierRepository
{
    public SupplierRepository(AppDbContext context) : base(context) { }

    public async Task<List<Supplier>> GetAllWithProductsAsync()
    {
        return await _dbSet
            .Include(s => s.Products)
            .OrderBy(s => s.Name)
            .ToListAsync();
    }

    public async Task<Supplier?> GetByIdWithProductsAsync(Guid id)
    {
        return await _dbSet
            .Include(s => s.Products)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Supplier?> GetByEmailAsync(string email)
    {
        return await _dbSet
            .FirstOrDefaultAsync(s => 
                s.Email.ToLower() == email.ToLower());
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbSet.AnyAsync(s => s.Id == id);
    }
    
    public async Task<(List<Supplier> Items, long TotalCount)> GetPagedAsync(int page, int pageSize)
    {
        var query = _dbSet
            .Include(s => s.Products)
            .AsNoTracking()
            .OrderBy(s => s.Name);

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}