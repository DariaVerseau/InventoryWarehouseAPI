using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Warehouse;

namespace BLL.Services;

public class WarehouseService(IWarehouseRepository warehouseRepository) : IWarehouseService
{
    public async Task<List<WarehouseDto>> GetWarehouses() 
        => await warehouseRepository.GetAll();

    public async Task<WarehouseDto> GetWarehouse(Guid id) 
        => await warehouseRepository.GetById(id);

    public async Task<WarehouseDto> CreateWarehouse(CreateWarehouseDto warehouseDto) 
        => await warehouseRepository.Create(warehouseDto);

    public async Task<WarehouseDto> UpdateWarehouse(UpdateWarehouseDto warehouseDto) 
        => await warehouseRepository.Update(warehouseDto);

    public async Task DeleteWarehouse(Guid id) 
        => await warehouseRepository.Delete(id);
}