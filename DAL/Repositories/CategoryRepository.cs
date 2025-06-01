using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Category;
using DTO.Product;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public async Task<List<CategoryDto>> GetAll()
    {
        var categories = await context.Categories
            .Include(c => c.Products)
            .ToListAsync();

        return categories.Select(MapToDto).ToList();
    }

    public async Task<CategoryDto> GetById(Guid id)
    {
        var category = await context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);

        return category != null ? MapToDto(category) : null;
    }

    public async Task<CategoryDto> Create(CreateCategoryDto categoryDto)
    {
        var category = new Category
        {
            Name = categoryDto.Name,
            Description = categoryDto.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        context.Categories.Add(category);
        await context.SaveChangesAsync();

        return MapToDto(category);
    }

    public async Task<CategoryDto> Update(UpdateCategoryDto categoryDto)
    {
        var category = await context.Categories.FindAsync(categoryDto.Id);
        if (category == null) return null;

        category.Name = categoryDto.Name;
        category.Description = categoryDto.Description;
        category.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();

        return await GetById(category.Id);
    }

    public async Task<bool> Delete(Guid id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null) return false;

        context.Categories.Remove(category);
        await context.SaveChangesAsync();

        return true;
    }

    private static CategoryDto MapToDto(Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Products = category.Products?.Select(p => new ProductDto
            {
                Id = p.Id,
                Unit = p.Unit,
                CategoryId = p.CategoryId,
                SupplierId = p.SupplierId,
                // Добавьте другие свойства продукта по необходимости
                Name = p.Name,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList() ?? new List<ProductDto>(),
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt
        };
    }
}