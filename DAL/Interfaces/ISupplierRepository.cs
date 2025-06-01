using DAL.Entities;
using DTO.Supplier;

namespace DAL.Interfaces;

public interface ISupplierRepository : IRepository<SupplierDto, CreateSupplierDto, UpdateSupplierDto>
{
    Task<List<SupplierDto>> SearchByName(string searchTerm);
    Task<bool> HasProducts(Guid supplierId);
}