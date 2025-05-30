using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public int? SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    
    // Навигационное свойство к остаткам
    public List<Inventory> InventoryRecords { get; set; } = new();
}

/*
 *public class ProductMap
{
    public ProductMap(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(p => p.Unit)
            .IsRequired()
            .HasMaxLength(10);
            
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt).IsRequired();
        
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
            
        builder.HasOne(p => p.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SupplierId);
        
        builder.HasMany(p => p.InventoryRecords)
            .WithOne(i => i.Product)
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
 
 */

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

/*
 
 public class BookMap
{
    public BookMap(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Title).IsRequired();
        builder.Property(b => b.Author).IsRequired();
        builder.Property(b => b.PublishDate).IsRequired();
        builder
            .Property(b => b.IsVisible)
            .HasDefaultValue(true)
            .IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
        builder.Property(b => b.UpdatedAt).IsRequired();
        builder
            .HasMany(b => b.Genres)
            .WithMany(g => g.Books);
    }
}

 
 * protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Book>()
        .HasOne(b => b.Author)          // У книги один автор
        .WithMany(a => a.Books)         // У автора много книг
        .HasForeignKey(b => b.AuthorId) // Указываем свойство внешнего ключа
        .OnDelete(DeleteBehavior.Cascade); // Поведение при удалении
}

public class Book : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; }
    public bool IsVisible { get; set; }
    public List<Genre> Genres { get; set; } = new();
}

public class BookMap
{
    public BookMap(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Title).IsRequired();
        builder.Property(b => b.Author).IsRequired();
        builder.Property(b => b.PublishDate).IsRequired();
        builder
            .Property(b => b.IsVisible)
            .HasDefaultValue(true)
            .IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
        builder.Property(b => b.UpdatedAt).IsRequired();
        builder
            .HasMany(b => b.Genres)
            .WithMany(g => g.Books);
    }
}

 */

/*
 CREATE TABLE products (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    category_id INTEGER REFERENCES categories(id),
    supplier_id INTEGER REFERENCES suppliers(id),
    unit VARCHAR(10) NOT NULL
);


public class Warehouse : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
}

public class WarehouseMap
{
    public WarehouseMap(EntityTypeBuilder<Warehouse> builder)
    {
       builder.HasKey(x => x.Id);
       builder.Property(x => x.Name).IsRequired();
       builder.Property(x => x.Location).IsRequired();
       builder.Property(b => b.CreatedAt).IsRequired();
       builder.Property(b => b.UpdatedAt).IsRequired();

    }
}
 *
 */