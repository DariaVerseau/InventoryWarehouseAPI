using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }
    public Guid? SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public int TotalQuantity { get; set; }
    
    // Навигационное свойство к остаткам
    public List<Inventory> InventoryRecords { get; set; } = new(); //навигационное свойство с Inventory
    public IEnumerable<InventoryTransaction>? InventoryTransactions { get; set; } //навигационное свойство с InventoryTransaction
    
}

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products"); // Явное указание имени таблицы

        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("name"); // Соответствие имени столбца в БД
            
        builder.Property(p => p.Unit)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnName("unit");
        
        builder.Property(p => p.TotalQuantity)
            .IsRequired()
            .HasColumnName("TotalQuantity")
            .HasComment("Текущее количество товара");
            
        builder.Property(p => p.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
        builder.Property(p => p.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at")
            .ValueGeneratedOnAddOrUpdate();
        
        // Настройка связи с Category
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .IsRequired(false) // Если CategoryId может быть NULL
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Products_Categories");
            
        // Настройка связи с Supplier
        builder.HasOne(p => p.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SupplierId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Products_Suppliers");
        
        // Настройка связи с Inventory
        builder.HasMany(p => p.InventoryRecords)
            .WithOne(i => i.Product)
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Inventory_Products");
    }
}


