using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public class AppDbContext : DbContext
{  
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        // Применение конфигураций
        modelBuilder.ApplyConfiguration(new ProductMap());
        modelBuilder.ApplyConfiguration(new InventoryTransactionMap());
        modelBuilder.ApplyConfiguration(new SupplierMap());
        modelBuilder.ApplyConfiguration(new WarehouseMap());
        modelBuilder.ApplyConfiguration(new CategoryMap());
        modelBuilder.ApplyConfiguration(new InventoryMap());
        modelBuilder.Entity<Category>().HasData(
            new Category { 
                Id = Guid.Parse("a2d3b4c5-6f7e-8d9c-0b1a-2d3e4f5a6b7c"), 
                Name = "Электроника", 
                Description = "Гаджеты и устройства" 
            },
            new Category { 
                Id = Guid.Parse("b3c4d5e6-7f8e-9d0c-1a2b-3c4d5e6f7a8b"), 
                Name = "Одежда", 
                Description = "Мужская и женская одежда" 
            }
        );
    }
}