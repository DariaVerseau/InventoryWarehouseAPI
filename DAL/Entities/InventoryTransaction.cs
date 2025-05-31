using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public enum TransactionType
{
    Incoming,
    Outgoing,
    Transfer
}

public class InventoryTransaction : BaseEntity
{
    
    public int? ProductId { get; set; }
    public Product Product { get; set; }
    public int? WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    public int Quantity { get; set; }
    public TransactionType TransactionType { get; set; }
  
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    
}

public class InventoryTransactionMap : IEntityTypeConfiguration<InventoryTransaction>
{
    public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
    {
        builder.ToTable("inventory_transactions"); // Явное указание имени таблицы

        builder.HasKey(t => t.Id);
            
        builder.Property(t => t.Quantity)
            .IsRequired()
            .HasColumnName("quantity")
            .HasComment("Количество товара в транзакции"); 
            
        builder.Property(t => t.TransactionType)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnName("transaction_type")
            .HasConversion(
                v => v.ToString().ToLower(),
                v => (TransactionType)Enum.Parse(typeof(TransactionType), v, true))
            .HasAnnotation("CheckConstraint", "transaction_type IN ('incoming', 'outgoing', 'transfer')")
            .HasComment("Тип транзакции: incoming, outgoing, transfer");
            
        builder.Property(t => t.TransactionDate)
            .IsRequired()
            .HasColumnName("transaction_date")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd()
            .HasComment("Дата и время транзакции");

        // Настройка связи с Product
        builder.HasOne(t => t.Product)
            .WithMany(p => p.InventoryTransactions)
            .HasForeignKey(t => t.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_inventory_transactions_products");
            
        // Настройка связи с Warehouse
        builder.HasOne(t => t.Warehouse)
            .WithMany(w => w.InventoryTransactions)
            .HasForeignKey(t => t.WarehouseId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_inventory_transactions_warehouses");
        
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



