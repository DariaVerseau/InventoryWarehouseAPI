using DTO.PagedResponse;
using DTO.Supplier;

namespace BLL.Interfaces;

public interface ISupplierService
{
    Task<List<SupplierDto>> GetAll();
    Task<SupplierDto?> GetById(Guid id);
    Task<SupplierDto> Create(CreateSupplierDto dto);
    Task<SupplierDto?> Update(UpdateSupplierDto dto);
    Task<bool> Delete(Guid id);
    Task<SupplierDto?> GetByEmail(string email);
    Task<PagedResponse<SupplierDto>> GetSuppliersPaged(int page, int pageSize);
}