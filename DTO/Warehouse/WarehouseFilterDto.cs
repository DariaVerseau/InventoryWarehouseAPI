using DTO.PagedResponse;
namespace DTO.Warehouse;

public class WarehouseFilterDto :  PagedResponse<WarehouseDto>
{
    public string? SortBy { get; set; } = "name"; // "name", "name desc", "location"
}