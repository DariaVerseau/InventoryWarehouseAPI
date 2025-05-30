using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class InventoryTransaction : BaseEntity
{
    public int? ProductId { get; set; }
    public Product Product { get; set; }
    public int? WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    public int Quantity { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
}

public class InventoryTransactionMap
{
    public InventoryTransactionMap(EntityTypeBuilder<InventoryTransaction> builder)
    {
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Quantity)
            .IsRequired();
            
        builder.Property(t => t.TransactionType)
            .IsRequired()
            .HasMaxLength(10)
            .HasConversion(
                v => v.ToLower(),
                v => v.ToLower())
            .HasAnnotation("CheckConstraint", "transaction_type IN ('incoming', 'outgoing', 'transfer')");
            
        builder.Property(t => t.TransactionDate)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
        builder.HasOne(t => t.Product)
            .WithMany()
            .HasForeignKey(t => t.ProductId);
            
        builder.HasOne(t => t.Warehouse)
            .WithMany()
            .HasForeignKey(t => t.WarehouseId);
    }
}


