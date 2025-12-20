using DTO.PagedResponse;

namespace DTO.Product;

public class ProductFilterDto : PagedResponse<ProductDto>
{
    public Guid? CategoryId { get; set; }
    public Guid? SupplierId { get; set; }
    public bool? IsVisible { get; set; }
    public string? SortBy { get; set; } = "name"; // "name", "createdAt desc", и т.д.
}