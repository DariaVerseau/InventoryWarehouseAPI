//using BLL.DTOs.Inventory;
using DTO.Inventory;

namespace BLL.Interfaces;

public interface IInventoryService
{
    Task<List<InventoryDto>> GetInventories();
    Task<InventoryDto> GetInventory(Guid id);
    Task<InventoryDto> CreateInventory(CreateInventoryDto inventory);
    Task<InventoryDto> UpdateInventory(UpdateInventoryDto inventory);
    Task DeleteInventory(Guid id);

    Task<List<InventoryDto>> GetByProductId(Guid productId);
    Task<int> GetTotalQuantity(Guid productId);

    Task<List<InventoryDto>> GetByWarehouseId(Guid warehouseId);
}