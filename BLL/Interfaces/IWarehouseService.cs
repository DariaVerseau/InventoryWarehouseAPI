using DTO.Warehouse;

namespace BLL.Services;

public interface IWarehouseService
{
    Task<WarehouseDto> GetByIdAsync(Guid id);
    Task<List<WarehouseDto>> GetAllAsync();
    Task<WarehouseDto> CreateAsync(WarehouseDto warehouseDto);
    Task<WarehouseDto> UpdateAsync(WarehouseDto warehouseDto);
    Task<bool> DeleteAsync(Guid id);
    //Task<List<WarehouseWithStatsDto>> GetWarehousesWithStatsAsync();
    Task<bool> CheckCapacityAsync(Guid warehouseId, int requiredSpace);
}