using BLL.Interfaces;
using DTO.PagedResponse;
using DTO.Warehouse;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehousesController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;

    public WarehousesController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }
    
    [HttpGet("warehouses")]
    public async Task<ActionResult<PagedResponse<WarehouseDto>>> GetWarehouses(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        if (page < 1) page = 1;
        if (pageSize is < 1 or > 50) pageSize = 10;

        var result = await _warehouseService.GetWarehousesPaged(page, pageSize);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<WarehouseDto>>> GetAll()
    {
        var warehouses = await _warehouseService.GetAll();
        return Ok(warehouses);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<WarehouseDto>> GetById(Guid id)
    {
        var warehouse = await _warehouseService.GetById(id);
        return Ok(warehouse);
    }

    [HttpPost]
    public async Task<ActionResult<WarehouseDto>> Create([FromBody] CreateWarehouseDto createWarehouseDto)
    {
        var createdWarehouse = await _warehouseService.Create(createWarehouseDto);
        return CreatedAtAction(nameof(GetById), new { id = createdWarehouse.Id }, createdWarehouse);
    }

    [HttpPut]
    public async Task<ActionResult<WarehouseDto>> Update([FromBody] UpdateWarehouseDto updateWarehouseDto)
    {
        var updatedWarehouse = await _warehouseService.Update(updateWarehouseDto);
        return Ok(updatedWarehouse);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _warehouseService.Delete(id);
        return NoContent();
    }
}