namespace DTO.Inventory;

public class InventoryShortDto
{
    public Guid? WarehouseId { get; set; }
    public string? WarehouseName { get; set; }
    public int Quantity { get; set; }
}