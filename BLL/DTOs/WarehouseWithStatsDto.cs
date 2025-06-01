using DTO.Warehouse;

namespace BLL.DTOs;

public class WarehouseWithStatsDto
{
    public WarehouseDto Warehouse { get; set; }
    public int UsedCapacity { get; set; }
    public int UsagePercentage { get; set; }
}