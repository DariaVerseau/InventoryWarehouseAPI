namespace DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class Warehouse : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    
    public List<Inventory> InventoryItems { get; set; } = new(); //навигационное свойство с Inventory
    public IEnumerable<InventoryTransaction>? InventoryTransactions { get; set; } //навигационное свойство с InventoryTransaction
    
}

public class WarehouseMap : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("warehouses");  // Явное указание имени таблицы в БД

        builder.HasKey(w => w.Id);
        
        builder.Property(w => w.Name)
            .IsRequired()
            .HasMaxLength(100)  // Рекомендуется указывать максимальную длину
            .HasColumnName("name")  // Соответствие имени столбца в БД
            .HasComment("Наименование склада");  // Добавление комментария
            
        builder.Property(w => w.Location)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("location")
            .HasComment("Физическое расположение склада");
            
        builder.Property(w => w.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")  // Значение по умолчанию
            .ValueGeneratedOnAdd()  // Автогенерация при создании
            .HasComment("Дата создания записи");
            
        builder.Property(w => w.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")  // Значение по умолчанию
            .ValueGeneratedOnAddOrUpdate()  // Автогенерация при обновлении
            .HasComment("Дата последнего обновления");
        
            
        // Настройка связи с Inventory (если есть навигационное свойство)
        builder.HasMany(w => w.InventoryItems)
            .WithOne(i => i.Warehouse)
            .HasForeignKey(i => i.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict)  // Запрет удаления если есть остатки
            .HasConstraintName("FK_inventory_warehouses");
    }
}

