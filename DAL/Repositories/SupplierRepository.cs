using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Category;
using DTO.Product;
using DTO.Supplier;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class SupplierRepository(AppDbContext context) : ISupplierRepository
{
    public async Task<List<SupplierDto>> GetAll()
    {
        var suppliers = await context.Suppliers
            .Include(s => s.Products)
                .ThenInclude(p => p.Category)
            .OrderBy(s => s.Name)
            .ToListAsync();

        return suppliers.Select(MapToDto).ToList();
    }

    public async Task<SupplierDto> GetById(Guid id)
    {
        var supplier = await context.Suppliers
            .Include(s => s.Products)
                .ThenInclude(p => p.Category)
            .FirstOrDefaultAsync(s => s.Id == id);

        return supplier != null ? MapToDto(supplier) : null;
    }

    public async Task<SupplierDto> Create(CreateSupplierDto supplierDto)
    {
        var supplier = new Supplier
        {
            Name = supplierDto.Name,
            Phone = supplierDto.Phone,
            Email = supplierDto.Email,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        context.Suppliers.Add(supplier);
        await context.SaveChangesAsync();

        return MapToDto(supplier);
    }

    public async Task<SupplierDto?> Update(UpdateSupplierDto supplierDto)
    {
        var supplier = await context.Suppliers.FindAsync(supplierDto.Id);
        if (supplier == null) return null;

        supplier.Name = supplierDto.Name;
        supplier.Phone = supplierDto.Phone;
        supplier.Email = supplierDto.Email;
        supplier.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
        return await GetById(supplier.Id);
    }

    public async Task<bool> Delete(Guid id)
    {
        var supplier = await context.Suppliers.FindAsync(id);
        if (supplier == null) return false;

        context.Suppliers.Remove(supplier);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<List<SupplierDto>> SearchByName(string searchTerm)
    {
        return await context.Suppliers
            .Where(s => Microsoft.EntityFrameworkCore.EF.Functions.Like(s.Name, $"%{searchTerm}%"))
            .Select(s => MapToDto(s))
            .ToListAsync();
    }

    public async Task<bool> HasProducts(Guid supplierId)
    {
        return await context.Products
            .AnyAsync(p => p.SupplierId == supplierId);
    }

    private static SupplierDto MapToDto(Supplier supplier)
    {
        return new SupplierDto
        {
            Id = supplier.Id,
            Name = supplier.Name,
            Phone = supplier.Phone,
            Email = supplier.Email,
            Products = supplier.Products?.Select(p => new ProductShortDto
            {
                Id = p.Id,
                Name = p.Name,
                Unit = p.Unit,
                Category = p.Category != null ? new CategoryShortDto
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name,
                    Description = p.Category.Description
                } : null
            }).ToList() ?? new List<ProductShortDto>(),
            CreatedAt = supplier.CreatedAt,
            UpdatedAt = supplier.UpdatedAt
        };
    }
}