using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Category;
using DAL.Entities;
using DTO.PagedResponse; // только внутри методов, не в сигнатурах!

namespace BLL.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<CategoryDto>> GetCategories()
    {
        var categories = await _categoryRepository.GetAll();
        return categories.Select(MapToDto).ToList(); // или через AutoMapper
    }

    public async Task<CategoryDto> GetCategory(Guid id)
    {
        var category = await _categoryRepository.GetById(id)
            ?? throw new KeyNotFoundException($"Category with ID {id} not found.");
        
        return MapToDto(category);
    }

    public async Task<CategoryDto> CreateCategory(CreateCategoryDto dto)
    {
        // Валидация (опционально — можно вынести в отдельный слой)
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Category name is required.");

        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description
        };

        var created = await _categoryRepository.Create(category);
        return MapToDto(created);
    }

    public async Task<CategoryDto> UpdateCategory(UpdateCategoryDto dto)
    {
        var existing = await _categoryRepository.GetById(dto.Id)
            ?? throw new KeyNotFoundException($"Category with ID {dto.Id} not found.");

        existing.Name = dto.Name;
        existing.IsVisible = dto.IsVisible;
        existing.Description = dto.Description;

        var updated = await _categoryRepository.Update(existing);
        return MapToDto(updated);
    }

    public async Task DeleteCategory(Guid id)
    {
        var exists = await _categoryRepository.GetById(id);
        if (exists == null) 
            throw new KeyNotFoundException($"Category with ID {id} not found.");

        await _categoryRepository.Delete(id);
    }
    
    public async Task<PagedResponse<CategoryDto>> GetCategoriesPaged(int page, int pageSize)
    {
        var (categories, totalCount) = await _categoryRepository.GetPagedAsync(page, pageSize);
        return new PagedResponse<CategoryDto>
        {
            Items = categories.Select(MapToDto).ToList(),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    //Приватный маппер (можно вынести в отдельный класс)
    private static CategoryDto MapToDto(Category category) => new()
    {
        Id = category.Id,
        Name = category.Name,
        Description = category.Description,
        IsVisible = category.IsVisible,
        CreatedAt = category.CreatedAt,
        UpdatedAt = category.UpdatedAt
    };
}