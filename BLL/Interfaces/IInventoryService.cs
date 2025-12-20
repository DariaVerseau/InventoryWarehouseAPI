using DTO.Inventory;
using DTO.PagedResponse;

namespace BLL.Interfaces;

public interface IInventoryService
{
    Task<List<InventoryDto>> GetInventories();
    Task<InventoryDto> GetInventory(Guid id);
    Task<InventoryDto> CreateInventory(CreateInventoryDto dto);
    Task<InventoryDto> UpdateInventory(UpdateInventoryDto dto);
    Task DeleteInventory(Guid id);

    Task<List<InventoryDto>> GetByProductId(Guid productId);
    Task<int> GetTotalQuantity(Guid productId); 

    Task<List<InventoryDto>> GetByWarehouseId(Guid warehouseId);
    Task<PagedResponse<InventoryDto>> GetInventoriesPaged(int page, int pageSize);
    Task<PagedResponse<InventoryDto>> GetFilteredInventory(InventoryFilterDto filter);
}