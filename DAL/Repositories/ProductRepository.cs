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
    public async Task<List<ProductDto>> GetAll()
    {
        var products = await context.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Include(p => p.InventoryRecords)
                .ThenInclude(i => i.Warehouse)
            .OrderBy(p => p.Name)
            .ToListAsync();

        return products.Select(MapToDto).ToList();
    }

    public async Task<ProductDto?> GetById(Guid id)
    {
        var product = await context.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Include(p => p.InventoryRecords)
                .ThenInclude(i => i.Warehouse)
            .Include(p => p.InventoryTransactions)
            .FirstOrDefaultAsync(p => p.Id == id);

        return product != null ? MapToDto(product) : null;
    }

    public async Task<ProductDto> Create(CreateProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Unit = productDto.Unit,
            CategoryId = productDto.CategoryId,
            SupplierId = productDto.SupplierId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        context.Products.Add(product);
        await context.SaveChangesAsync();

        return MapToDto(product);
    }

    public async Task<ProductDto?> Update(UpdateProductDto productDto)
    {
        var product = await context.Products.FindAsync(productDto.Id);
        if (product == null) return null;

        product.Name = productDto.Name;
        product.Unit = productDto.Unit;
        product.CategoryId = productDto.CategoryId;
        product.SupplierId = productDto.SupplierId;
        product.UpdatedAt = DateTime.UtcNow;
        product.IsVisible = productDto.IsVisible;

        await context.SaveChangesAsync();
        return await GetById(product.Id);
    }

    public async Task<bool> Delete(Guid id)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null) return false;

        context.Products.Remove(product);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<List<ProductDto>> GetByCategoryId(Guid categoryId)
    {
        return await context.Products
            .Where(p => p.CategoryId == categoryId)
            .Include(p => p.Category)
            .Select(p => MapToDto(p))
            .ToListAsync();
    }

    public async Task<List<ProductDto>> GetBySupplierId(Guid supplierId)
    {
        return await context.Products
            .Where(p => p.SupplierId == supplierId)
            .Include(p => p.Supplier)
            .Select(p => MapToDto(p))
            .ToListAsync();
    }

    public async Task<int> GetTotalStockQuantity(Guid productId)
    {
        return await context.Inventories
            .Where(i => i.ProductId == productId)
            .SumAsync(i => i.Quantity);
    }

    public async Task<List<ProductWithStockDto>> GetProductsWithStockInfo()
    {
        return await context.Products
            .Select(p => new ProductWithStockDto
            {
                Product = MapToDto(p),
                TotalQuantity = p.InventoryRecords.Sum(i => i.Quantity),
                LastTransactionDate = p.InventoryTransactions
                    .OrderByDescending(t => t.TransactionDate)
                    .FirstOrDefault().TransactionDate
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
            InventoryRecords = product.InventoryRecords?.Select(i => new InventoryShortDto
            {
                WarehouseId = i.WarehouseId,
                WarehouseName = i.Warehouse?.Name,
                Quantity = i.Quantity
            }).ToList() ?? new List<InventoryShortDto>(),
            /*TransactionHistory = product.InventoryTransactions?.Select(t => new InventoryTransactionShortDto
            {
                Id = t.Id,
                TransactionType = t.TransactionType,
                Quantity = t.Quantity,
                TransactionDate = t.TransactionDate
            }).OrderByDescending(t => t.TransactionDate).ToList() 
                ?? new List<InventoryTransactionShortDto>(),*/
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            IsVisible = product.IsVisible
        };
    }
}