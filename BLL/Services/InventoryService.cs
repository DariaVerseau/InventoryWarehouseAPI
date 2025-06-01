using AutoMapper;

using DAL.Interfaces;
using DTO.Inventory;

namespace BLL.Services;

public class InventoryService : IInventoryService
{
    private readonly IInventoryRepository _inventoryRepo;
    private readonly IMapper _mapper;

    public InventoryService(IInventoryRepository inventoryRepo, IMapper mapper)
    {
        _inventoryRepo = inventoryRepo;
        _mapper = mapper;
    }

    public async Task<InventoryDto> GetByIdAsync(Guid id)
    {
        var inventory = await _inventoryRepo.GetById(id);
        return _mapper.Map<InventoryDto>(inventory);
    }

    public async Task<List<InventoryDto>> GetByProductIdAsync(Guid productId)
    {
        var inventories = await _inventoryRepo.GetByProductId(productId);
        return _mapper.Map<List<InventoryDto>>(inventories);
    }

    public async Task<List<InventoryDto>> GetByWarehouseIdAsync(Guid warehouseId)
    {
        var inventories = await _inventoryRepo.GetByWarehouseId(warehouseId);
        return _mapper.Map<List<InventoryDto>>(inventories);
    }

    public async Task<InventoryDto> UpdateStockAsync(Guid inventoryId, int quantityChange)
    {
        // Бизнес-логика проверки
        if (quantityChange == 0)
            throw new ArgumentException("Quantity change cannot be zero");

        var inventory = await _inventoryRepo.GetById(inventoryId);
        if (inventory == null)
            throw new KeyNotFoundException("Inventory record not found");

        inventory.Quantity += quantityChange;
        var updated = await _inventoryRepo.Update(inventory);
        return _mapper.Map<InventoryDto>(updated);
    }

    public async Task<int> GetTotalStockAsync(Guid productId)
    {
        return await _inventoryRepo.GetTotalQuantity(productId);
    }
}