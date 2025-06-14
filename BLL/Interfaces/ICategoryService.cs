using DTO.Category;

namespace BLL.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategories();
    Task<CategoryDto> GetCategory(Guid id);
    Task<CategoryDto> CreateCategory(CreateCategoryDto category);
    Task<CategoryDto> UpdateCategory(UpdateCategoryDto category);
    Task DeleteCategory(Guid id);
}