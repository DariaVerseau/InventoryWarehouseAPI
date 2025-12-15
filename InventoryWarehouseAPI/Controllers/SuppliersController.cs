using BLL.Interfaces;
using DTO.PagedResponse;
using DTO.Supplier;
using DTO.Warehouse;
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
    
    [HttpGet("suppliers")]
    public async Task<ActionResult<PagedResponse<WarehouseDto>>> GetSuppliers(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        if (page < 1) page = 1;
        if (pageSize is < 1 or > 50) pageSize = 10;

        var result = await _supplierService.GetSuppliersPaged(page, pageSize);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<SupplierDto>>> GetAll()
    {
        var suppliers = await _supplierService.GetAll();
        return Ok(suppliers);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SupplierDto>> GetById(Guid id)
    {
        var supplier = await _supplierService.GetById(id);
        return Ok(supplier);
    }

    [HttpPost]
    public async Task<ActionResult<SupplierDto>> Create([FromBody] CreateSupplierDto createSupplierDto)
    {
        var createdSupplier = await _supplierService.Create(createSupplierDto);
        return CreatedAtAction(nameof(GetById), new { id = createdSupplier.Id }, createdSupplier);
    }

    [HttpPut]
    public async Task<ActionResult<SupplierDto>> Update([FromBody] UpdateSupplierDto updateSupplierDto)
    {
        var updatedSupplier = await _supplierService.Update(updateSupplierDto);
        return Ok(updatedSupplier);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _supplierService.Delete(id);
        return NoContent();
    }
}