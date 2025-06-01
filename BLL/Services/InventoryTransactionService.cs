using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Interfaces;
using DTO.InventoryTransaction;
using TransactionType = DTO.InventoryTransaction.TransactionType;

namespace BLL.Services;

public class InventoryTransactionService : IInventoryTransactionService
{
    private readonly IInventoryTransactionRepository _transactionRepo;
    private readonly IInventoryRepository _inventoryRepo;
    private readonly IMapper _mapper;

    public InventoryTransactionService(
        IInventoryTransactionRepository transactionRepo,
        IInventoryRepository inventoryRepo,
        IMapper mapper)
    {
        _transactionRepo = transactionRepo;
        _inventoryRepo = inventoryRepo;
        _mapper = mapper;
    }

    public async Task<InventoryTransactionDto> ProcessTransactionAsync(InventoryTransactionDto transactionDto)
    {
        // Валидация
        if (transactionDto.Quantity <= 0)
            throw new ArgumentException("Quantity must be positive");

        // Бизнес-логика обработки транзакции
        var transaction = _mapper.Map<InventoryTransaction>(transactionDto);
        var created = await _transactionRepo.Create(transaction);

        // Обновление остатков
        if (transactionDto.TransactionType == TransactionType.Incoming)
        {
            await _inventoryRepo.AddToStock(
                transactionDto.Product,
                transactionDto.Warehouse,
                transactionDto.Quantity);
        }
        else
        {
            await _inventoryRepo.RemoveFromStock(
                transactionDto.Product,
                transactionDto.Warehouse,
                transactionDto.Quantity);
        }

        return _mapper.Map<InventoryTransactionDto>(created);
    }

    public async Task<List<InventoryTransactionDto>> GetByProductAsync(Guid productId)
    {
        var transactions = await _transactionRepo.GetByProductId(productId);
        return _mapper.Map<List<InventoryTransactionDto>>(transactions);
    }

    public async Task<List<InventoryTransactionDto>> GetByWarehouseAsync(Guid warehouseId)
    {
        var transactions = await _transactionRepo.GetByWarehouseId(warehouseId);
        return _mapper.Map<List<InventoryTransactionDto>>(transactions);
    }

  /*  public async Task<List<InventoryTransactionDto>> GetByDateRangeAsync(DateTime start, DateTime end)
    {
        var transactions = await _transactionRepo.GetByDateRange(start, end);
        return _mapper.Map<List<InventoryTransactionDto>>(transactions);
    }*/
}