using DTO.Warehouse;

namespace DAL.Interfaces;

public interface IWarehouseRepository : IRepository<WarehouseDto, CreateWarehouseDto, UpdateWarehouseDto>
{
    Task<List<WarehouseDto>> SearchByName(string searchTerm);
    Task<int> GetTotalCapacityUsed(Guid warehouseId);

}