using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Category;

namespace BLL.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<List<CategoryDto>> GetCategories() 
        => await categoryRepository.GetAll();

    public async Task<CategoryDto> GetCategory(Guid id) 
        => await categoryRepository.GetById(id);

    public async Task<CategoryDto> CreateCategory(CreateCategoryDto categoryDto) 
        => await categoryRepository.Create(categoryDto);

    public async Task<CategoryDto> UpdateCategory(UpdateCategoryDto categoryDto) 
        => await categoryRepository.Update(categoryDto);

    public async Task DeleteCategory(Guid id) 
        => await categoryRepository.Delete(id);
}