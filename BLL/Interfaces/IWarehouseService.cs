
using DTO.Warehouse;

namespace BLL.Interfaces;

public interface IWarehouseService
{
    Task<List<WarehouseDto>> GetWarehouses();
    Task<WarehouseDto> GetWarehouse(Guid id);
    Task<WarehouseDto> CreateWarehouse(CreateWarehouseDto warehouse);
    Task<WarehouseDto> UpdateWarehouse(UpdateWarehouseDto warehouse);
    Task DeleteWarehouse(Guid id);
}