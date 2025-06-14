using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Inventory;

namespace BLL.Services;

public class InventoryService(IInventoryRepository inventoryRepository) : IInventoryService
{
    // Базовые CRUD-операции
    public async Task<List<InventoryDto>> GetInventories() 
        => await inventoryRepository.GetAll();

    public async Task<InventoryDto> GetInventory(Guid id) 
        => await inventoryRepository.GetById(id);

    public async Task<InventoryDto> CreateInventory(CreateInventoryDto inventoryDto) 
        => await inventoryRepository.Create(inventoryDto);

    public async Task<InventoryDto> UpdateInventory(UpdateInventoryDto inventoryDto) 
        => await inventoryRepository.Update(inventoryDto);

    public async Task DeleteInventory(Guid id) 
        => await inventoryRepository.Delete(id);

    // Специфичные методы для инвентаря
    public async Task<List<InventoryDto>> GetByProductId(Guid productId) 
        => await inventoryRepository.GetByProductId(productId);

    public async Task<int> GetTotalQuantity(Guid productId) 
        => await inventoryRepository.GetTotalQuantity(productId);

    public async Task<List<InventoryDto>> GetByWarehouseId(Guid warehouseId) 
        => await inventoryRepository.GetByWarehouseId(warehouseId);
}