using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Inventory;
using DTO.InventoryTransaction;
using DTO.Product;
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
            .Include(w => w.InventoryTransactions)!
                .ThenInclude(t => t.Product)
            .OrderBy(w => w.Name)
            .Select(w => MapToDto(w))
            .ToListAsync();
    }

    public async Task<WarehouseDto?> GetById(Guid id)
    {
        var warehouse = await context.Warehouses
            .Include(w => w.InventoryItems)
            .ThenInclude(i => i.Product)
            .Include(w => w.InventoryTransactions)!
                .ThenInclude(t => t.Product)
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
            InventoryTransactions = new List<InventoryTransaction>(), // Важно!
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
            .Include(w => w.InventoryTransactions)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (warehouse == null) return false;

        // Проверка на наличие связанных записей
        if (warehouse.InventoryItems.Any() || warehouse.InventoryTransactions.Any())
            return false; // Или throw new InvalidOperationException()

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
                InventoryItems = new List<InventoryDto>(),
                InventoryTransactions = Enumerable.Empty<InventoryTransactionDto>()
            };
        }

        // Безопасное преобразование InventoryItems
        var inventoryItems = warehouse.InventoryItems != null
            ? warehouse.InventoryItems
                .Where(i => i != null)
                .Select(i => new InventoryDto
                {
                    Id = i.Id,
                    Quantity = i.Quantity,
                    Product = i.Product != null ? new ProductShortDto
                    {
                        Id = i.Product.Id,
                        Name = i.Product.Name ?? string.Empty,
                        Unit = i.Product.Unit ?? string.Empty
                    } : null
                }).ToList()
            : new List<InventoryDto>();

        // Безопасное преобразование InventoryTransactions
        var inventoryTransactions = warehouse.InventoryTransactions != null
            ? warehouse.InventoryTransactions
                .Where(t => t != null)
                .Select(t => new InventoryTransactionDto
                {
                    Id = t.Id,
                    Quantity = t.Quantity,
                    TransactionDate = t.TransactionDate,
                    Product = t.Product != null ? new ProductShortDto
                    {
                        Id = t.Product.Id,
                        Name = t.Product.Name ?? string.Empty
                    } : null
                })
            : Enumerable.Empty<InventoryTransactionDto>();

        return new WarehouseDto
        {
            Id = warehouse.Id,
            Name = warehouse.Name ?? string.Empty,
            Location = warehouse.Location ?? string.Empty,
            InventoryItems = inventoryItems,
            InventoryTransactions = inventoryTransactions,
            CreatedAt = warehouse.CreatedAt,
            UpdatedAt = warehouse.UpdatedAt
        };
    }
    
}