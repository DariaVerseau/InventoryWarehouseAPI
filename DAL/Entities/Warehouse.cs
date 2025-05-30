namespace DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

