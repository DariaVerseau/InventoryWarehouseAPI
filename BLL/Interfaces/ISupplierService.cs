using DTO.Supplier;

namespace BLL.Interfaces;

public interface ISupplierService
{
    Task<List<SupplierDto>> GetSuppliers();
    Task<SupplierDto> GetSupplier(Guid id);
    Task<SupplierDto> CreateSupplier(CreateSupplierDto supplier);
    Task<SupplierDto> UpdateSupplier(UpdateSupplierDto supplier);
    Task DeleteSupplier(Guid id);
}