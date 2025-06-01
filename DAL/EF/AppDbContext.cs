using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public class AppDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

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
    }
}