using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Supplier;

namespace BLL.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepo;
    private readonly IMapper _mapper;

    public SupplierService(ISupplierRepository supplierRepo, IMapper mapper)
    {
        _supplierRepo = supplierRepo;
        _mapper = mapper;
    }

    public async Task<SupplierDto> GetByIdAsync(Guid id)
    {
        var supplier = await _supplierRepo.GetById(id);
        return _mapper.Map<SupplierDto>(supplier);
    }

    public async Task<List<SupplierDto>> GetAllAsync()
    {
        var suppliers = await _supplierRepo.GetAll();
        return _mapper.Map<List<SupplierDto>>(suppliers);
    }

    public async Task<SupplierDto> CreateAsync(SupplierDto supplierDto)
    {
        var supplier = _mapper.Map<Supplier>(supplierDto);
        var created = await _supplierRepo.Create(supplier);
        return _mapper.Map<SupplierDto>(created);
    }

    public async Task<SupplierDto> UpdateAsync(SupplierDto supplierDto)
    {
        var supplier = _mapper.Map<Supplier>(supplierDto);
        var updated = await _supplierRepo.Update(supplier);
        return _mapper.Map<SupplierDto>(updated);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        if (await _supplierRepo.HasProducts(id))
            throw new InvalidOperationException("Cannot delete supplier with associated products");

        return await _supplierRepo.Delete(id);
    }

    public async Task<List<SupplierWithProductsDto>> GetSuppliersWithProductsAsync()
    {
        var suppliers = await _supplierRepo.GetAll();
        return suppliers.Select(s => new SupplierWithProductsDto
        {
            Supplier = _mapper.Map<SupplierDto>(s),
            ProductCount = s.Products?.Count ?? 0
        }).ToList();
    }
}