using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Warehouse;
using DAL.Entities;
using DTO.Inventory;
using DTO.PagedResponse;

namespace BLL.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepo;

    public WarehouseService(IWarehouseRepository warehouseRepo)
    {
        _warehouseRepo = warehouseRepo;
    }

    public async Task<List<WarehouseDto>> GetAll()
    {
        var warehouses = await _warehouseRepo.GetAllWithInventoryAsync();
        return warehouses.Select(MapToDto).ToList();
    }

    public async Task<WarehouseDto?> GetById(Guid id)
    {
        var warehouse = await _warehouseRepo.GetByIdWithInventoryAsync(id);
        return warehouse == null ? null : MapToDto(warehouse);
    }

    public async Task<WarehouseDto> Create(CreateWarehouseDto dto)
    {
        var warehouse = new Warehouse
        {
            Name = dto.Name,
            Location = dto.Location
            // CreatedAt/UpdatedAt — EF заполнит сам (см. WarehouseMap)
        };

        var created = await _warehouseRepo.Create(warehouse);
        return MapToDto(created);
    }

    public async Task<WarehouseDto?> Update(UpdateWarehouseDto dto)
    {
        var warehouseUpdt = await _warehouseRepo.GetById(dto.Id);
        if (warehouseUpdt == null)
            return null;

        warehouseUpdt.Name = dto.Name;
        warehouseUpdt.Location = dto.Location;

        var updated = await _warehouseRepo.Update(warehouseUpdt);
        return MapToDto(updated);
    }

    public async Task<bool> Delete(Guid id)
    {
        // Проверка наличия InventoryItems — можно добавить валидацию
        return await _warehouseRepo.Delete(id);
    }

    public async Task<List<WarehouseDto>> SearchByName(string searchTerm)
    {
        var warehouses = await _warehouseRepo.SearchByNameAsync(searchTerm);
        return warehouses.Select(MapToDto).ToList();
    }

    public async Task<int> GetTotalCapacityUsed(Guid warehouseId)
    {
        return await _warehouseRepo.GetTotalCapacityUsedAsync(warehouseId);
    }
    
    public async Task<PagedResponse<WarehouseDto>> GetWarehousesPaged(int page, int pageSize)
    {
        var (warehouses, totalCount) = await _warehouseRepo.GetPagedAsync(page, pageSize);
        return new PagedResponse<WarehouseDto>
        {
            Items = warehouses.Select(MapToDto).ToList(),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    private static WarehouseDto MapToDto(Warehouse entity)
    {
        return new WarehouseDto
        {
            Id = entity.Id,
            Name = entity.Name ?? string.Empty,
            Location = entity.Location ?? string.Empty,
            InventoryItems = entity.InventoryItems?
                .Select(i => new InventoryShortDto
                {
                    WarehouseId = i.WarehouseId,
                    Quantity = i.Quantity,
                    WarehouseName = i.Warehouse?.Name ?? string.Empty
                }).ToList() ?? new List<InventoryShortDto>(),
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}