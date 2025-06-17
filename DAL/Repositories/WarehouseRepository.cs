using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Inventory;
using DTO.Warehouse;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class WarehouseRepository(AppDbContext context) : IWarehouseRepository
{
    public async Task<List<WarehouseDto>> GetAll()
    {
        return await context.Warehouses
            .Include(w => w.InventoryItems)
            .ThenInclude(i => i.Product)
            .OrderBy(w => w.Name)
            .Select(w => MapToDto(w))
            .ToListAsync();
    }

    public async Task<WarehouseDto?> GetById(Guid id)
    {
        var warehouse = await context.Warehouses
            .Include(w => w.InventoryItems)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(w => w.Id == id);

        return warehouse != null ? MapToDto(warehouse) : null;
    }

    public async Task<WarehouseDto> Create(CreateWarehouseDto warehouseDto)
    {
        var warehouse = new Warehouse
        {
            Name = warehouseDto.Name,
            Location = warehouseDto.Location,
            InventoryItems = new List<Inventory>(), // Важно!
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        context.Warehouses.Add(warehouse);
        await context.SaveChangesAsync();

        return MapToDto(warehouse);
    }

    public async Task<WarehouseDto?> Update(UpdateWarehouseDto warehouseDto)
    {
        var warehouse = await context.Warehouses.FindAsync(warehouseDto.Id);
        if (warehouse == null) return null;

        warehouse.Name = warehouseDto.Name;
        warehouse.Location = warehouseDto.Location;
        warehouse.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
        return await GetById(warehouse.Id);
    }

    public async Task<bool> Delete(Guid id)
    {
        var warehouse = await context.Warehouses
            .Include(w => w.InventoryItems)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (warehouse == null) return false;
        
        context.Warehouses.Remove(warehouse);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<List<WarehouseDto>> SearchByName(string searchTerm)
    {
        return await context.Warehouses
            .Where(w => Microsoft.EntityFrameworkCore.EF.Functions.Like(w.Name, $"%{searchTerm}%"))
            .Select(w => MapToDto(w))
            .ToListAsync();
    }

    public async Task<int> GetTotalCapacityUsed(Guid warehouseId)
    {
        return await context.Inventories
            .Where(i => i.WarehouseId == warehouseId)
            .SumAsync(i => i.Quantity);
    }

    private static WarehouseDto MapToDto(Warehouse warehouse)
    {
        // Полная защита от null
        if (warehouse == null)
        {
            return new WarehouseDto
            {
                InventoryItems = new List<InventoryShortDto>(),
            };
        }

        // Безопасное преобразование InventoryItems
        var inventoryItems = warehouse.InventoryItems != null
            ? warehouse.InventoryItems
                .Where(i => i != null)
                .Select(i => new InventoryShortDto
                {
                    WarehouseId = i.WarehouseId,
                    Quantity = i.Quantity,
                    WarehouseName = i.Warehouse.Name
                }).ToList()
            : new List<InventoryShortDto>();

        return new WarehouseDto
        {
            Id = warehouse.Id,
            Name = warehouse.Name ?? string.Empty,
            Location = warehouse.Location ?? string.Empty,
            InventoryItems = inventoryItems,
            CreatedAt = warehouse.CreatedAt,
            UpdatedAt = warehouse.UpdatedAt
        };
    }
    
}