using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Supplier;

namespace BLL.Services;

public class SupplierService(ISupplierRepository supplierRepository) : ISupplierService
{
    public async Task<List<SupplierDto>> GetSuppliers() 
        => await supplierRepository.GetAll();

    public async Task<SupplierDto> GetSupplier(Guid id) 
        => await supplierRepository.GetById(id);

    public async Task<SupplierDto> CreateSupplier(CreateSupplierDto supplierDto) 
        => await supplierRepository.Create(supplierDto);

    public async Task<SupplierDto> UpdateSupplier(UpdateSupplierDto supplierDto) 
        => await supplierRepository.Update(supplierDto);

    public async Task DeleteSupplier(Guid id) 
        => await supplierRepository.Delete(id);
}