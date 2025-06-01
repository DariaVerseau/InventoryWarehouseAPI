using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class Supplier : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = new List<Product>();
}

public class SupplierMap : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("suppliers"); // Явное указание имени таблицы

        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("name"); // Соответствие имени столбца в БД
            
        builder.Property(s => s.Phone)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("phone")
            .HasComment("Контактный телефон поставщика"); // Добавление комментария
            
        builder.Property(s => s.Email)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("email")
            .HasAnnotation("RegularExpression", 
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"); // Проверка формата email
            
        builder.Property(s => s.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();
            
        builder.Property(s => s.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at")
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
     /*   builder.HasMany(s => s.Products)
            .WithOne(p => p.Supplier)
            .HasForeignKey(p => p.SupplierId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Products_Suppliers");*/
        
            //навигация с product 
    }
}

