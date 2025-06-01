using AutoMapper;
using BLL.DTOs;
using DAL.Interfaces;
using DAL.Entities;
using DTO.Category;

namespace BLL.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepo;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepo, IMapper mapper)
    {
        _categoryRepo = categoryRepo;
        _mapper = mapper;
    }

    public async Task<CategoryDto> GetByIdAsync(Guid id)
    {
        var category = await _categoryRepo.GetById(id);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var categories = await _categoryRepo.GetAll();
        return _mapper.Map<List<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> CreateAsync(CategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        var created = await _categoryRepo.Create(category);
        return _mapper.Map<CategoryDto>(created);
    }

    public async Task<CategoryDto> UpdateAsync(CategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        var updated = await _categoryRepo.Update(category);
        return _mapper.Map<CategoryDto>(updated);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _categoryRepo.Delete(id);
    }

    public async Task<List<CategoryWithStatsDto>> GetCategoriesWithStatsAsync()
    {
        var categories = await _categoryRepo.GetAll();
        return categories.Select(c => new CategoryWithStatsDto
        {
            Category = _mapper.Map<CategoryDto>(c),
            ProductCount = c.Products?.Count ?? 0
        }).ToList();
    }
}