using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class Inventory  : BaseEntity
{
    public bool IsVisible { get; set; }
    public Guid? ProductId { get; set; }
    public Product Product { get; set; }
    public Guid? WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    public int Quantity { get; set; }
 
}

public class InventoryMap : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("inventory"); // Явное указание имени таблицы
        
        builder.HasKey(i => i.Id);
        
        builder
            .Property(b => b.IsVisible)
            .HasDefaultValue(true)
            .IsRequired();
        
        builder.Property(i => i.Quantity)
            .IsRequired()
            .HasColumnName("quantity")
            .HasComment("Текущее количество товара на складе");

        // Настройка связи со складом
        builder.HasOne(i => i.Warehouse)
            .WithMany(w => w.InventoryItems)
            .HasForeignKey(i => i.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_inventory_warehouses");
        
        // Настройка связи с товаром
        builder.HasOne(i => i.Product)
            .WithMany(p => p.InventoryRecords)
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_inventory_products");
        
        builder.Property(i => i.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd()
            .HasComment("Дата создания записи");

        builder.Property(i => i.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAddOrUpdate()
            .HasComment("Дата последнего обновления");
    }
}
