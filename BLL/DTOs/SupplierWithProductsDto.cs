using DTO.Supplier;

namespace BLL.DTOs;

public class SupplierWithProductsDto
{
    public SupplierDto Supplier { get; set; }
    public int ProductCount { get; set; }
}