using BLL.Interfaces;
using DAL.Interfaces;
using DTO.InventoryTransaction;

namespace BLL.Services;

public class InventoryTransactionService(IInventoryTransactionRepository transactionRepository) 
    : IInventoryTransactionService
{
    public async Task<List<InventoryTransactionDto>> GetTransactions() 
        => await transactionRepository.GetAll();

    public async Task<InventoryTransactionDto> GetTransaction(Guid id) 
        => await transactionRepository.GetById(id);

    public async Task<InventoryTransactionDto> CreateTransaction(CreateInventoryTransactionDto transactionDto) 
        => await transactionRepository.Create(transactionDto);

    public async Task<InventoryTransactionDto> UpdateTransaction(UpdateInventoryTransactionDto transactionDto) 
        => await transactionRepository.Update(transactionDto);

    public async Task DeleteTransaction(Guid id) 
        => await transactionRepository.Delete(id);
}