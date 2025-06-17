using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Enums;

namespace DAL.Entities;

public class InventoryTransaction : BaseEntity
{
    
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    public int Quantity { get; set; }
   
    [Column(TypeName = "transaction_type_enum")]
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
            
        builder.Property(e => e.TransactionType)
            .HasColumnType("transaction_type_enum")
            .HasConversion(
                v => v.ToString().ToLower(), // сохраняет как 'incoming' или 'outgoing'
                v => (TransactionType)Enum.Parse(typeof(TransactionType), v, true));
            
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



