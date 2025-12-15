using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Inventory;
using DTO.PagedResponse;
using DTO.Product;
using DTO.Warehouse;
using InventoryDto = DTO.Inventory.InventoryDto;

namespace BLL.Services;

public class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _inventoryRepo;
    private readonly IProductRepository _productRepo; // если нужны данные Product
    private readonly IWarehouseRepository _warehouseRepo; // если нужны данные Warehouse

    public InventoryService(
        IInventoryRepository inventoryRepo,
        IProductRepository productRepo,
        IWarehouseRepository warehouseRepo)
    {
        _inventoryRepo = inventoryRepo;
        _productRepo = productRepo;
        _warehouseRepo = warehouseRepo;
    }

   public async Task<List<InventoryDto>> GetInventories()
{
    var inventories = await _inventoryRepo.GetAllWithDetails(); // ← вызов из репозитория
    return inventories.Select(MapToDto).ToList();
}

    public async Task<InventoryDto> GetInventory(Guid id)
    {
        var inventory = await _inventoryRepo.GetById(id)
            ?? throw new KeyNotFoundException($"Inventory with ID {id} not found.");
        return MapToDto(inventory);
    }

    public async Task<InventoryDto> CreateInventory(CreateInventoryDto dto)
    {
        // Проверка существования Product и Warehouse (опционально, но рекомендуется)
        var productExists = await _productRepo.GetById(dto.ProductId);
        var warehouseExists = await _warehouseRepo.GetById(dto.WarehouseId);
        
        if (productExists == null)
            throw new ArgumentException("Product not found.");
        if (warehouseExists == null)
            throw new ArgumentException("Warehouse not found.");

        var inventory = new DAL.Entities.Inventory
        {
            ProductId = dto.ProductId,
            WarehouseId = dto.WarehouseId,
            Quantity = dto.Quantity
        };

        var created = await _inventoryRepo.Create(inventory);
        return MapToDto(created);
    }

    public async Task<InventoryDto> UpdateInventory(UpdateInventoryDto dto)
    {
        var existing = await _inventoryRepo.GetById(dto.Id)
            ?? throw new KeyNotFoundException($"Inventory with ID {dto.Id} not found.");

        existing.Quantity = dto.Quantity;
        var updated = await _inventoryRepo.Update(existing);
        return MapToDto(updated);
    }

    public async Task DeleteInventory(Guid id)
    {
        var exists = await _inventoryRepo.GetById(id);
        if (exists == null)
            throw new KeyNotFoundException($"Inventory with ID {id} not found.");
        
        await _inventoryRepo.Delete(id);
    }

    public async Task<List<InventoryDto>> GetByProductId(Guid productId)
    {
        var inventories = await _inventoryRepo.GetByProductId(productId);
        return inventories.Select(MapToDto).ToList();
    }  

    public async Task<int> GetTotalQuantity(Guid productId)
    {
        return await _inventoryRepo.GetTotalQuantity(productId);
    }

    public async Task<List<InventoryDto>> GetByWarehouseId(Guid warehouseId)
    {
        var inventories = await _inventoryRepo.GetByWarehouseId(warehouseId);
        return inventories.Select(MapToDto).ToList();
    }
    
    public async Task<PagedResponse<InventoryDto>> GetInventoriesPaged(int page, int pageSize)
    {
        var (inventories, totalCount) = await _inventoryRepo.GetPagedAsync(page, pageSize);
        return new PagedResponse<InventoryDto>
        {
            Items = inventories.Select(MapToDto).ToList(),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    //Маппинг сущности → DTO
    private InventoryDto MapToDto(DAL.Entities.Inventory inventory)
    {
        // Здесь предполагается, что у тебя есть методы для получения ProductShortDto и WarehouseShortDto
        // Либо ты уже загрузил навигационные свойства через Include в репозитории
        
        return new InventoryDto
        {
            Id = inventory.Id,
            Product = inventory.Product != null ? new ProductShortDto
            {
                Id = inventory.Product.Id,
                Name = inventory.Product.Name
                // ... остальные поля
            } : null,
            Warehouse = inventory.Warehouse != null ? new WarehouseShortDto
            {
                Id = inventory.Warehouse.Id,
                Name = inventory.Warehouse.Name,
                Location = inventory.Warehouse.Location
            } : null,
            Quantity = inventory.Quantity,
            CreatedAt = inventory.CreatedAt,
            UpdatedAt = inventory.UpdatedAt
        };
    }
    
}