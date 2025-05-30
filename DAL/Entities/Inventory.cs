using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class Inventory  : BaseEntity
{
    public int? ProductId { get; set; }
    public Product Product { get; set; }
    public int? WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    public int Quantity { get; set; }
 
}

public class InventoryMap : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("inventory"); // Явное указание имени таблицы

        // Составной первичный ключ (ProductId + WarehouseId)
        builder.HasKey(i => new { i.ProductId, i.WarehouseId });
        
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
    }
}
