using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Inventory;
using DTO.Product;
using DTO.Warehouse;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class InventoryRepository(AppDbContext context) : IInventoryRepository
{
    public async Task<List<InventoryDto>> GetAll()
    {
        var inventories = await context.Inventories
            .Include(i => i.Product)
            .Include(i => i.Warehouse)
            .ToListAsync();

        return inventories.Select(MapToDto).ToList();
    }

    public async Task<InventoryDto?> GetById(Guid id)
    {
        var inventory = await context.Inventories
            .Include(i => i.Product)
            .Include(i => i.Warehouse)
            .FirstOrDefaultAsync(i => i.Id == id);

        return inventory != null ? MapToDto(inventory) : null;
    }

    public async Task<InventoryDto> Create(CreateInventoryDto inventoryDto)
    {
        var inventory = new Inventory
        {
            ProductId = inventoryDto.ProductId,
            WarehouseId = inventoryDto.WarehouseId,
            Quantity = inventoryDto.Quantity,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        context.Inventories.Add(inventory);
        await context.SaveChangesAsync();

        return MapToDto(inventory);
    }

    public async Task<InventoryDto?> Update(UpdateInventoryDto inventoryDto)
    {
        var inventory = await context.Inventories.FindAsync(inventoryDto.Id);
        if (inventory == null) return null;

        inventory.ProductId = inventoryDto.ProductId;
        inventory.WarehouseId = inventoryDto.WarehouseId;
        inventory.Quantity = inventoryDto.Quantity;
        inventory.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();

        return await GetById(inventory.Id);
    }

    public async Task<bool> Delete(Guid id)
    {
        var inventory = await context.Inventories.FindAsync(id);
        if (inventory == null) return false;

        context.Inventories.Remove(inventory);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<List<InventoryDto>> GetByProductId(Guid productId)
    {
        var inventories = await context.Inventories
            .Include(i => i.Warehouse)
            .Where(i => i.ProductId == productId)
            .ToListAsync();

        return inventories.Select(MapToDto).ToList();
    }

    public async Task<List<InventoryDto>> GetByWarehouseId(Guid warehouseId)
    {
        var inventories = await context.Inventories
            .Include(i => i.Product)
            .Where(i => i.WarehouseId == warehouseId)
            .ToListAsync();

        return inventories.Select(MapToDto).ToList();
    }

    public async Task<int> GetTotalQuantity(Guid productId)
    {
        return await context.Inventories
            .Where(i => i.ProductId == productId)
            .SumAsync(i => i.Quantity);
    }

    private static InventoryDto MapToDto(Inventory inventory)
    {
        return new InventoryDto
        {
            Id = inventory.Id,
            Quantity = inventory.Quantity,
            Product = inventory.Product != null ? new ProductDto
            {
                Id = inventory.Product.Id,
                Name = inventory.Product.Name,
                // Другие необходимые свойства продукта
            } : null,
            Warehouse = inventory.Warehouse != null ? new WarehouseDto
            {
                Id = inventory.Warehouse.Id,
                Name = inventory.Warehouse.Name,
                // Другие необходимые свойства склада
            } : null,
            CreatedAt = inventory.CreatedAt,
            UpdatedAt = inventory.UpdatedAt
        };
    }
}