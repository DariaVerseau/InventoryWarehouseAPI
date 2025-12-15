using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Product;
using DAL.Entities;
using DTO.Category;
using DTO.Inventory;
using DTO.PagedResponse;
using DTO.Supplier;

namespace BLL.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly ISupplierRepository _supplierRepo;

    public ProductService(
        IProductRepository productRepo,
        ICategoryRepository categoryRepo,
        ISupplierRepository supplierRepo)
    {
        _productRepo = productRepo;
        _categoryRepo = categoryRepo;
        _supplierRepo = supplierRepo;
    }

    public async Task<PagedResponse<ProductDto>> GetProductsPaged(int page, int pageSize)
    {
        // Получаем данные из репозитория (только сущности)
        var (products, totalCount) = await _productRepo.GetPagedAsync(page, pageSize);

        // Маппинг в DTO
        var dtos = products.Select(MapToDto).ToList();

        // Формируем ответ
        return new PagedResponse<ProductDto>
        {
            Items = dtos,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }
    
    public async Task<List<ProductDto>> GetAll()
    {
        var products = await _productRepo.GetAllWithDetailsAsync();
        return products.Select(MapToDto).ToList();
    }

    public async Task<ProductDto?> GetById(Guid id)
    {
        var product = await _productRepo.GetByIdWithDetailsAsync(id);
        return product == null ? null : MapToDto(product);
    }

    public async Task<ProductDto> Create(CreateProductDto dto)
    {
        // Валидация внешних ключей (опционально, но рекомендуется)
        if (!await _categoryRepo.ExistsAsync(dto.CategoryId))
            throw new ArgumentException("Category not found.");
        if (!await _supplierRepo.ExistsAsync(dto.SupplierId))
            throw new ArgumentException("Supplier not found.");

        var product = new Product
        {
            Name = dto.Name,
            Unit = dto.Unit,
            TotalQuantity = dto.TotalQuantity,
            CategoryId = dto.CategoryId,
            SupplierId = dto.SupplierId,
            IsVisible = true // по умолчанию
        };

        var created = await _productRepo.Create(product);
        return MapToDto(created);
    }

    public async Task<ProductDto> Update(UpdateProductDto dto)
    {
        var existing = await _productRepo.GetById(dto.Id)
            ?? throw new KeyNotFoundException($"Product with ID {dto.Id} not found.");

        existing.Name = dto.Name;
        existing.Unit = dto.Unit;
        existing.TotalQuantity = dto.TotalQuantity;
        existing.CategoryId = dto.CategoryId;
        existing.SupplierId = dto.SupplierId;
        existing.IsVisible = dto.IsVisible;

        var updated = await _productRepo.Update(existing);
        return MapToDto(updated);
    }

    public async Task<bool> Delete(Guid id)
    {
        return await _productRepo.Delete(id);
    }

    public async Task<List<ProductDto>> GetByCategoryId(Guid categoryId)
    {
        var products = await _productRepo.GetByCategoryIdAsync(categoryId);
        return products.Select(MapToDto).ToList();
    }

    public async Task<List<ProductDto>> GetBySupplierId(Guid supplierId)
    {
        var products = await _productRepo.GetBySupplierIdAsync(supplierId);
        return products.Select(MapToDto).ToList();
    }

    public async Task<int> GetTotalStockQuantity(Guid productId)
    {
        return await _productRepo.GetTotalStockQuantityAsync(productId);
    }

    public async Task<(List<ProductDto> Items, long TotalCount)> GetPaged(int page, int pageSize)
    {
        var (products, totalCount) = await _productRepo.GetPagedAsync(page, pageSize);
        var dtos = products.Select(MapToDto).ToList();
        return (dtos, totalCount);
    }

    private static ProductDto MapToDto(Product entity)
    {
        return new ProductDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Unit = entity.Unit,
            TotalQuantity = entity.TotalQuantity,
            IsVisible = entity.IsVisible,
            Category = entity.Category != null ? new CategoryShortDto
            {
                Id = entity.Category.Id,
                Name = entity.Category.Name
            } : null,
            Supplier = entity.Supplier != null ? new SupplierShortDto
            {
                Id = entity.Supplier.Id,
                Name = entity.Supplier.Name
            } : null,
            InventoryRecords = entity.InventoryRecords?
                .Select(i => new InventoryShortDto
                {
                    WarehouseId = i.WarehouseId,
                    WarehouseName = i.Warehouse?.Name,
                    Quantity = i.Quantity
                }).ToList() ?? new List<InventoryShortDto>(),
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}