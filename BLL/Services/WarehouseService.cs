using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Warehouse;

namespace BLL.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepo;
    private readonly IMapper _mapper;

    public WarehouseService(IWarehouseRepository warehouseRepo, IMapper mapper)
    {
        _warehouseRepo = warehouseRepo;
        _mapper = mapper;
    }

    public async Task<WarehouseDto> GetByIdAsync(Guid id)
    {
        var warehouse = await _warehouseRepo.GetById(id);
        return _mapper.Map<WarehouseDto>(warehouse);
    }

    public async Task<List<WarehouseDto>> GetAllAsync()
    {
        var warehouses = await _warehouseRepo.GetAll();
        return _mapper.Map<List<WarehouseDto>>(warehouses);
    }

    public async Task<WarehouseDto> CreateAsync(WarehouseDto warehouseDto)
    {
        var warehouse = _mapper.Map<Warehouse>(warehouseDto);
        var created = await _warehouseRepo.Create(warehouse);
        return _mapper.Map<WarehouseDto>(created);
    }

    public async Task<WarehouseDto> UpdateAsync(WarehouseDto warehouseDto)
    {
        var warehouse = _mapper.Map<Warehouse>(warehouseDto);
        var updated = await _warehouseRepo.Update(warehouse);
        return _mapper.Map<WarehouseDto>(updated);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _warehouseRepo.Delete(id);
    }

    
   
     public async Task<List<WarehouseWithStatsDto>> GetWarehousesWithStatsAsync()
    {
        var warehouses = await _warehouseRepo.GetAll();
        var result = new List<WarehouseWithStatsDto>();

        foreach (var warehouse in warehouses)
        {
            var usedCapacity = await _warehouseRepo.GetTotalCapacityUsed(warehouse.Id);
            result.Add(new WarehouseWithStatsDto
            {
                Warehouse = _mapper.Map<WarehouseDto>(warehouse),
                UsedCapacity = usedCapacity,
                //UsagePercentage = (int)((double)usedCapacity / warehouse.Capacity * 100)
            });
        }

        return result;
    }
    /*
   public async Task<bool> CheckCapacityAsync(Guid warehouseId, int requiredSpace)
  {
      var warehouse = await _warehouseRepo.GetById(warehouseId);
      if (warehouse == null) return false;

      var usedCapacity = await _warehouseRepo.GetTotalCapacityUsed(warehouseId);
      return warehouse.Capacity - usedCapacity >= requiredSpace;
  }*/
}