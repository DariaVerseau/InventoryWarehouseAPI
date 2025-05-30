using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public List<Product> Products { get; set; } = new List<Product>();
    
}

public class CategoryMap
{
    public CategoryMap(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
        builder.Property(c => c.Description).IsRequired();
        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.UpdatedAt).IsRequired();
    }
}

/*
 *CREATE TABLE categories (
    id SERIAL PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    description TEXT
);


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
 * 
 */