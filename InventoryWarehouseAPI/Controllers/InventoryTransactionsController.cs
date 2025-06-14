using BLL.Interfaces;
using DTO.InventoryTransaction;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryTransactionsController : ControllerBase
{
    private readonly IInventoryTransactionService _transactionService;

    public InventoryTransactionsController(IInventoryTransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    public async Task<ActionResult<List<InventoryTransactionDto>>> GetAll()
    {
        var transactions = await _transactionService.GetTransactions();
        return Ok(transactions);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<InventoryTransactionDto>> GetById(Guid id)
    {
        var transaction = await _transactionService.GetTransaction(id);
        return Ok(transaction);
    }

    [HttpPost]
    public async Task<ActionResult<InventoryTransactionDto>> Create([FromBody] CreateInventoryTransactionDto createTransactionDto)
    {
        var createdTransaction = await _transactionService.CreateTransaction(createTransactionDto);
        return CreatedAtAction(nameof(GetById), new { id = createdTransaction.Id }, createdTransaction);
    }

    [HttpPut]
    public async Task<ActionResult<InventoryTransactionDto>> Update([FromBody] UpdateInventoryTransactionDto updateTransactionDto)
    {
        var updatedTransaction = await _transactionService.UpdateTransaction(updateTransactionDto);
        return Ok(updatedTransaction);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _transactionService.DeleteTransaction(id);
        return NoContent();
    }
}