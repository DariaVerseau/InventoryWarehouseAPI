using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class Inventory  : BaseEntity
{
    public int? ProductId { get; set; }
    public Product Product { get; set; }
    public int? WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    public int Quantity { get; set; }
    public DateTime last_update { get; set; }
}

public class InventoryMap
{
    public InventoryMap(EntityTypeBuilder<Inventory> builder)
    {
        builder.HasKey(i => i.ProductId);
        builder.Property(i => i.Quantity).IsRequired();
        builder.HasOne(i => i.Warehouse)
            .WithMany()
            .HasForeignKey(i => i.Id);
        
        builder.HasOne(i => i.Product)
            .WithMany(p => p.InventoryRecords)
            .HasForeignKey(i => i.Id);
    }
}

/*
 *-- 5. Таблица инвентаря
CREATE TABLE inventory (
    id SERIAL PRIMARY KEY,
    product_id INTEGER REFERENCES products(id),
    warehouse_id INTEGER REFERENCES warehouses(id),
    quantity INTEGER NOT NULL,
    last_update TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
 * 
 */