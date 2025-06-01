using DAL.Entities;
using DTO.Inventory;

namespace DAL.Interfaces;

public interface IInventoryRepository : IRepository<InventoryDto, CreateInventoryDto, UpdateInventoryDto>
{
    Task<List<InventoryDto>> GetByProductId(Guid productId);
    Task<int> GetTotalQuantity(Guid productId);
    Task<List<InventoryDto>> GetByWarehouseId(Guid warehouseId);
}