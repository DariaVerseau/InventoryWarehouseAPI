using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Supplier;
using DAL.Entities;
using DTO.PagedResponse;
using DTO.Product;

namespace BLL.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepo;

    public SupplierService(ISupplierRepository supplierRepo)
    {
        _supplierRepo = supplierRepo;
    }

    public async Task<List<SupplierDto>> GetAll()
    {
        var suppliers = await _supplierRepo.GetAllWithProductsAsync();
        return suppliers.Select(MapToDto).ToList();
    }

    public async Task<SupplierDto?> GetById(Guid id)
    {
        var supplier = await _supplierRepo.GetByIdWithProductsAsync(id);
        return supplier == null ? null : MapToDto(supplier);
    }

    public async Task<SupplierDto> Create(CreateSupplierDto dto)
    {
        // Проверка уникальности email (опционально)
        if (await _supplierRepo.GetByEmailAsync(dto.Email) != null)
            throw new ArgumentException("Supplier with this email already exists.");

        var supplier = new Supplier
        {
            Name = dto.Name,
            Phone = dto.Phone,
            Email = dto.Email
        };

        var created = await _supplierRepo.Create(supplier);
        return MapToDto(created);
    }

    public async Task<SupplierDto?> Update(UpdateSupplierDto dto)
    {
        var existing = await _supplierRepo.GetById(dto.Id);
        if (existing == null) return null;

        // Проверка email на уникальность (если изменён)
        var existingByEmail = await _supplierRepo.GetByEmailAsync(dto.Email);
        if (existingByEmail != null && existingByEmail.Id != dto.Id)
            throw new ArgumentException("Another supplier with this email already exists.");

        existing.Name = dto.Name;
        existing.Phone = dto.Phone;
        existing.Email = dto.Email;

        var updated = await _supplierRepo.Update(existing);
        return MapToDto(updated);
    }

    public async Task<bool> Delete(Guid id)
    {
        return await _supplierRepo.Delete(id);
    }

    public async Task<SupplierDto?> GetByEmail(string email)
    {
        var supplier = await _supplierRepo.GetByEmailAsync(email);
        return supplier == null ? null : MapToDto(supplier);
    }
    
    public async Task<PagedResponse<SupplierDto>> GetSuppliersPaged(int page, int pageSize)
    {
        var (suppliers, totalCount) = await _supplierRepo.GetPagedAsync(page, pageSize);
        return new PagedResponse<SupplierDto>
        {
            Items = suppliers.Select(MapToDto).ToList(),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    private static SupplierDto MapToDto(Supplier entity)
    {
        return new SupplierDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Phone = entity.Phone,
            Email = entity.Email,
            Products = entity.Products?
                .Select(p => new ProductShortDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Unit = p.Unit
                }).ToList() ?? new List<ProductShortDto>(),
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}