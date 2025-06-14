using BLL.Interfaces;
using DTO.Supplier;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SupplierDto>>> GetAll()
    {
        var suppliers = await _supplierService.GetSuppliers();
        return Ok(suppliers);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SupplierDto>> GetById(Guid id)
    {
        var supplier = await _supplierService.GetSupplier(id);
        return Ok(supplier);
    }

    [HttpPost]
    public async Task<ActionResult<SupplierDto>> Create([FromBody] CreateSupplierDto createSupplierDto)
    {
        var createdSupplier = await _supplierService.CreateSupplier(createSupplierDto);
        return CreatedAtAction(nameof(GetById), new { id = createdSupplier.Id }, createdSupplier);
    }

    [HttpPut]
    public async Task<ActionResult<SupplierDto>> Update([FromBody] UpdateSupplierDto updateSupplierDto)
    {
        var updatedSupplier = await _supplierService.UpdateSupplier(updateSupplierDto);
        return Ok(updatedSupplier);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _supplierService.DeleteSupplier(id);
        return NoContent();
    }
}