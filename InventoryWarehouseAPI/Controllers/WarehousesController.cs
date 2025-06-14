using BLL.Interfaces;
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

    [HttpGet]
    public async Task<ActionResult<List<WarehouseDto>>> GetAll()
    {
        var warehouses = await _warehouseService.GetWarehouses();
        return Ok(warehouses);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<WarehouseDto>> GetById(Guid id)
    {
        var warehouse = await _warehouseService.GetWarehouse(id);
        return Ok(warehouse);
    }

    [HttpPost]
    public async Task<ActionResult<WarehouseDto>> Create([FromBody] CreateWarehouseDto createWarehouseDto)
    {
        var createdWarehouse = await _warehouseService.CreateWarehouse(createWarehouseDto);
        return CreatedAtAction(nameof(GetById), new { id = createdWarehouse.Id }, createdWarehouse);
    }

    [HttpPut]
    public async Task<ActionResult<WarehouseDto>> Update([FromBody] UpdateWarehouseDto updateWarehouseDto)
    {
        var updatedWarehouse = await _warehouseService.UpdateWarehouse(updateWarehouseDto);
        return Ok(updatedWarehouse);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _warehouseService.DeleteWarehouse(id);
        return NoContent();
    }
}