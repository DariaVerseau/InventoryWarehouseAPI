using DAL.EF;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.SeedData;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Применяем миграции
        await context.Database.MigrateAsync();

        // Добавляем категории, если их нет
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category
                {
                    Id = Guid.Parse("a2d3b4c5-6f7e-8d9c-0b1a-2d3e4f5a6b7c"),
                    Name = "Электроника",
                    Description = "Гаджеты и устройства",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsVisible = true
                },
                new Category
                {
                    Id = Guid.Parse("b3c4d5e6-7f8e-9d0c-1a2b-3c4d5e6f7a8b"),
                    Name = "Одежда",
                    Description = "Мужская и женская одежда",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsVisible = true
                }
            );

            await context.SaveChangesAsync();
        }
    }
}