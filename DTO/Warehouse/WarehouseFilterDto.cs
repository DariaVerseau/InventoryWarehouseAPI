using DTO.PagedResponse;
namespace DTO.Warehouse;

public class WarehouseFilterDto :  PagedResponse<WarehouseDto>
{
    public string? Search { get; set; } // поиск по имени или местоположению
    public string? SortBy { get; set; } = "name"; // "name", "name desc", "location"
}