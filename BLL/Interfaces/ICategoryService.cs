using DTO.Category;

namespace BLL.Services;

public interface ICategoryService
{
    Task<CategoryDto> GetByIdAsync(Guid id);
    Task<List<CategoryDto>> GetAllAsync();
    Task<CategoryDto> CreateAsync(CategoryDto categoryDto);
    Task<CategoryDto> UpdateAsync(CategoryDto categoryDto);
    Task<bool> DeleteAsync(Guid id);
    //Task<List<CategoryWithStatsDto>> GetCategoriesWithStatsAsync();
}