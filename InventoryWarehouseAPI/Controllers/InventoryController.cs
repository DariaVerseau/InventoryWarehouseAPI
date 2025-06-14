using BLL.Interfaces;
using DTO.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoriesController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoriesController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    // Базовые CRUD-операции
    [HttpGet]
    public async Task<ActionResult<List<InventoryDto>>> GetAll()
    {
        var inventories = await _inventoryService.GetInventories();
        return Ok(inventories);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<InventoryDto>> GetById(Guid id)
    {
        var inventory = await _inventoryService.GetInventory(id);
        return Ok(inventory);
    }

    [HttpPost]
    public async Task<ActionResult<InventoryDto>> Create([FromBody] CreateInventoryDto createInventoryDto)
    {
        var createdInventory = await _inventoryService.CreateInventory(createInventoryDto);
        return CreatedAtAction(nameof(GetById), new { id = createdInventory.Id }, createdInventory);
    }

    [HttpPut]
    public async Task<ActionResult<InventoryDto>> Update([FromBody] UpdateInventoryDto updateInventoryDto)
    {
        var updatedInventory = await _inventoryService.UpdateInventory(updateInventoryDto);
        return Ok(updatedInventory);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _inventoryService.DeleteInventory(id);
        return NoContent();
    }

    // Специфичные методы для инвентаря
    [HttpGet("product/{productId:guid}")]
    public async Task<ActionResult<List<InventoryDto>>> GetByProduct(Guid productId)
    {
        var inventories = await _inventoryService.GetByProductId(productId);
        return Ok(inventories);
    }

    [HttpGet("product/{productId:guid}/quantity")]
    public async Task<ActionResult<int>> GetTotalQuantity(Guid productId)
    {
        var quantity = await _inventoryService.GetTotalQuantity(productId);
        return Ok(quantity);
    }

    [HttpGet("warehouse/{warehouseId:guid}")]
    public async Task<ActionResult<List<InventoryDto>>> GetByWarehouse(Guid warehouseId)
    {
        var inventories = await _inventoryService.GetByWarehouseId(warehouseId);
        return Ok(inventories);
    }
}