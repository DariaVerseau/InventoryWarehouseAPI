using DTO.Category;

namespace BLL.DTOs;

public class CategoryWithStatsDto
{
    public CategoryDto Category { get; set; }
    public int ProductCount { get; set; }
}