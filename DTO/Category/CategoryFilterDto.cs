using DTO.PagedResponse;

namespace DTO.Category;

public class CategoryFilterDto : PagedResponse<CategoryDto>
{
    public bool? IsVisible { get; set; }
    public string? SortBy { get; set; } = "name";
}