using System.Dynamic;
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
            Description = dto.Description,
            IsVisible = dto.IsVisible
        };

        var created = await _categoryRepository.Create(category);
        return MapToDto(created);
    }

    public async Task<CategoryDto> UpdateCategory(UpdateCategoryDto dto)
    {
        var existing = await _categoryRepository.GetById(dto.Id)
            ?? throw new KeyNotFoundException($"Category with ID {dto.Id} not found.");

        if (dto.Name != null) 
            existing.Name = dto.Name;

        if (dto.Description != null) 
            existing.Description = dto.Description;

        if (dto.IsVisible.HasValue) 
            existing.IsVisible = dto.IsVisible.Value;

        existing.UpdatedAt = DateTime.UtcNow;

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
    
    public async Task<List<CategoryDto>> SearchByName(string searchTerm)
    {
        var warehouses = await _categoryRepository.SearchByNameAsync(searchTerm);
        return warehouses.Select(MapToDto).ToList();
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
    
    public async Task<PagedResponse<CategoryDto>> GetFilteredCategories(CategoryFilterDto filter)
    {
        if (filter.Page < 1) filter.Page = 1;
        if (filter.PageSize < 1 || filter.PageSize > 50) filter.PageSize = 5;

        var (categories, totalCount) = await _categoryRepository.GetFilteredAsync(
            search: filter.Search,
            isVisible: filter.IsVisible,
            sortBy: filter.SortBy,
            page: filter.Page,
            pageSize: filter.PageSize
        );

        return new PagedResponse<CategoryDto>
        {
            Items = categories.Select(MapToDto).ToList(),
            TotalCount = totalCount,
            Page = filter.Page,
            PageSize = filter.PageSize
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