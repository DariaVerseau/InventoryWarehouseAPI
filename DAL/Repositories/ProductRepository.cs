using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Product;
using DTO.Category;
using DTO.Supplier;
using DTO.Inventory;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    private IQueryable<Product> BaseQuery => context.Products
        .AsNoTracking()
        .Include(p => p.Category)
        .Include(p => p.Supplier)
        .Include(p => p.InventoryRecords)
            .ThenInclude(i => i.Warehouse)
        .Include(p => p.InventoryTransactions);

    public async Task<List<ProductDto>> GetAll()
    {
        return await BaseQuery
            .OrderBy(p => p.Name)
            .Select(p => MapToDto(p))
            .ToListAsync();
    }

    public async Task<ProductDto?> GetById(Guid id)
    {
        return await BaseQuery
            .Where(p => p.Id == id)
            .Select(p => MapToDto(p))
            .FirstOrDefaultAsync();
    }

    public async Task<ProductDto> Create(CreateProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Unit = productDto.Unit,
            TotalQuantity = productDto.TotalQuantity,
            CategoryId = productDto.CategoryId,
            SupplierId = productDto.SupplierId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsVisible = true // Добавляем значение по умолчанию
        };

        context.Products.Add(product);
        await context.SaveChangesAsync();

        return MapToDto(product);
    }

    public async Task<ProductDto?> Update(UpdateProductDto productDto)
    {
        var product = await context.Products
            .FirstOrDefaultAsync(p => p.Id == productDto.Id);
            
        if (product == null) return null;

        product.Name = productDto.Name;
        product.Unit = productDto.Unit;
        product.TotalQuantity = productDto.TotalQuantity;
        product.CategoryId = productDto.CategoryId;
        product.SupplierId = productDto.SupplierId;
        product.IsVisible = productDto.IsVisible;
        product.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
        return await GetById(product.Id);
    }

    public async Task<bool> Delete(Guid id)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null) return false;

        context.Products.Remove(product);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<List<ProductDto>> GetByCategoryId(Guid categoryId)
    {
        return await BaseQuery
            .Where(p => p.CategoryId == categoryId)
            .Select(p => MapToDto(p))
            .ToListAsync();
    }

    public async Task<List<ProductDto>> GetBySupplierId(Guid supplierId)
    {
        return await BaseQuery
            .Where(p => p.SupplierId == supplierId)
            .Select(p => MapToDto(p))
            .ToListAsync();
    }

    public async Task<int> GetTotalStockQuantity(Guid productId)
    {
        return await context.Inventories
            .AsNoTracking()
            .Where(i => i.ProductId == productId)
            .SumAsync(i => i.Quantity);
    }

    public async Task<List<ProductWithStockDto>> GetProductsWithStockInfo()
    {
        return await BaseQuery
            .Select(p => new ProductWithStockDto
            {
                Product = MapToDto(p),
                TotalQuantity = p.InventoryRecords.Sum(i => i.Quantity),
                LastTransactionDate = p.InventoryTransactions
                    .Max(t => (DateTime?)t.TransactionDate)
            })
            .ToListAsync();
    }

    private static ProductDto MapToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Unit = product.Unit,
            TotalQuantity = product.TotalQuantity,
            Category = product.Category != null ? new CategoryShortDto
            {
                Id = product.Category.Id,
                Name = product.Category.Name
            } : null,
            Supplier = product.Supplier != null ? new SupplierShortDto
            {
                Id = product.Supplier.Id,
                Name = product.Supplier.Name
            } : null,
            InventoryRecords = product.InventoryRecords?
                .Select(i => new InventoryShortDto
                {
                    WarehouseId = i.WarehouseId,
                    WarehouseName = i.Warehouse?.Name,
                    Quantity = i.Quantity
                }).ToList() ?? new List<InventoryShortDto>(),
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            IsVisible = product.IsVisible
        };
    }
}