

using BLL.DTOs;
using DTO.Supplier;

namespace BLL.Services;

public interface ISupplierService
{
    Task<SupplierDto> GetByIdAsync(Guid id);
    Task<List<SupplierDto>> GetAllAsync();
    Task<SupplierDto> CreateAsync(SupplierDto supplierDto);
    Task<SupplierDto> UpdateAsync(SupplierDto supplierDto);
    Task<bool> DeleteAsync(Guid id);
    Task<List<SupplierWithProductsDto>> GetSuppliersWithProductsAsync();
}