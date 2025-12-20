using DTO.PagedResponse; 

namespace DTO.Inventory;

public class InventoryFilterDto : PagedResponse<InventoryDto>
{
    public string? ProductName { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? WarehouseId { get; set; }
    public int? MinQuantity { get; set; }
    public int? MaxQuantity { get; set; }
    public string? SortBy { get; set; } = "productName"; // "productName", "quantity desc", "warehouseName"
}