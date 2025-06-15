using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    
    public bool IsVisible { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
    
}

public class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories"); // Явное указание имени таблицы

        // Первичный ключ
        builder.HasKey(c => c.Id)
              .HasName("pk_categories"); // Имя первичного ключа в БД

        // Настройка свойств
        builder.Property(c => c.Name)
              .IsRequired()
              .HasMaxLength(50)
              .HasColumnName("name")
              .HasComment("Наименование категории товаров");

        builder.Property(c => c.Description)
              .IsRequired()
              .HasColumnName("description")
              .HasMaxLength(500) // Рекомендуется ограничение длины
              .HasComment("Подробное описание категории");

        // Автоматические временные метки
        builder.Property(c => c.CreatedAt)
              .IsRequired()
              .HasColumnName("created_at")
              .HasDefaultValueSql("CURRENT_TIMESTAMP")
              .ValueGeneratedOnAdd()
              .HasComment("Дата создания записи");

        builder.Property(c => c.UpdatedAt)
              .IsRequired()
              .HasColumnName("updated_at")
              .HasDefaultValueSql("CURRENT_TIMESTAMP")
              .ValueGeneratedOnAddOrUpdate()
              .HasComment("Дата последнего обновления");
        
        builder
              .Property(an => an.IsVisible)
              .HasDefaultValue(true)
              .IsRequired();
        

        // Настройка связи с продуктами (если есть навигационное свойство)
        builder.HasMany(c => c.Products)
              .WithOne(p => p.Category)
              .HasForeignKey(p => p.CategoryId)
              .OnDelete(DeleteBehavior.Restrict)
              .HasConstraintName("fk_products_category");
    }
}

