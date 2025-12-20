using DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DAL.EF;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        
        // Используем ТОТ ЖЕ connection string, что и в appsettings.json
        // Для локальной разработки — подставь свой
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=InventoryWarehousedb;Username=postgres;Password=Kattty_candyy");
        
        return new AppDbContext(optionsBuilder.Options);
    }
}