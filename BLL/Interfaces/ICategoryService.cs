using DTO.Category;
using DTO.PagedResponse;

namespace BLL.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategories();
    Task<CategoryDto> GetCategory(Guid id);
    Task<CategoryDto> CreateCategory(CreateCategoryDto category);
    Task<CategoryDto> UpdateCategory(UpdateCategoryDto category);
    Task DeleteCategory(Guid id);
    Task<List<CategoryDto>> SearchByName(string searchTerm);
    Task<PagedResponse<CategoryDto>> GetCategoriesPaged(int page, int pageSize);
    Task<PagedResponse<CategoryDto>> GetFilteredCategories(CategoryFilterDto filter);
}