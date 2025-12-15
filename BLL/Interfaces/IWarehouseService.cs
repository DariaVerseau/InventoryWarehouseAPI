using DTO.PagedResponse;
using DTO.Warehouse;

namespace BLL.Interfaces;

public interface IWarehouseService
{
    Task<List<WarehouseDto>> GetAll();
    Task<WarehouseDto?> GetById(Guid id);
    Task<WarehouseDto> Create(CreateWarehouseDto dto);
    Task<WarehouseDto?> Update(UpdateWarehouseDto dto);
    Task<bool> Delete(Guid id);
    Task<List<WarehouseDto>> SearchByName(string searchTerm);
    Task<int> GetTotalCapacityUsed(Guid warehouseId);
    Task<PagedResponse<WarehouseDto>> GetWarehousesPaged(int page, int pageSize);
    
}