using DTO.Inventory;

namespace BLL.Services;

public interface IInventoryService
{
    Task<InventoryDto> GetByIdAsync(Guid id);
    Task<List<InventoryDto>> GetByProductIdAsync(Guid productId);
    Task<List<InventoryDto>> GetByWarehouseIdAsync(Guid warehouseId);
    Task<InventoryDto> UpdateStockAsync(Guid inventoryId, int quantityChange);
    Task<int> GetTotalStockAsync(Guid productId);
}