using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Inventory;
using DTO.InventoryTransaction;
using DTO.Product;
using DTO.Warehouse;
using Microsoft.EntityFrameworkCore;
using TransactionType = DAL.Entities.TransactionType;

namespace DAL.Repositories;

public class InventoryTransactionRepository(AppDbContext context) : IInventoryTransactionRepository
{
    public async Task<List<InventoryTransactionDto>> GetAll()
    {
        var transactions = await context.InventoryTransactions
            .Include(t => t.Product)
            .Include(t => t.Warehouse)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();

        return transactions.Select(MapToDto).ToList();
    }

    public async Task<InventoryTransactionDto?> GetById(Guid id)
    {
        var transaction = await context.InventoryTransactions
            .Include(t => t.Product)
            .Include(t => t.Warehouse)
            .FirstOrDefaultAsync(t => t.Id == id);

        return transaction != null ? MapToDto(transaction) : null;
    }

    public async Task<InventoryTransactionDto> Create(CreateInventoryTransactionDto transactionDto)
    {
        var transaction = new InventoryTransaction
        {
            ProductId = transactionDto.ProductId,
            WarehouseId = transactionDto.WarehouseId,
            Quantity = transactionDto.Quantity,
            //TransactionType = transactionDto.TransactionType,
            TransactionDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        context.InventoryTransactions.Add(transaction);
        await context.SaveChangesAsync();

        return MapToDto(transaction);
    }

    public async Task<InventoryTransactionDto?> Update(UpdateInventoryTransactionDto transactionDto)
    {
        var transaction = await context.InventoryTransactions.FindAsync(transactionDto.Id);
        if (transaction == null) return null;

        transaction.Quantity = transactionDto.Quantity;
        //transaction.TransactionType = transactionDto.TransactionType;
        transaction.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
        return await GetById(transaction.Id);
    }

    public async Task<bool> Delete(Guid id)
    {
        var transaction = await context.InventoryTransactions.FindAsync(id);
        if (transaction == null) return false;

        context.InventoryTransactions.Remove(transaction);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<List<InventoryTransactionDto>> GetByProductId(Guid productId)
    {
        var transactions = await context.InventoryTransactions
            .Include(t => t.Warehouse)
            .Where(t => t.ProductId == productId)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();

        return transactions.Select(MapToDto).ToList();
    }

    public async Task<List<InventoryTransactionDto>> GetByWarehouseId(Guid warehouseId)
    {
        var transactions = await context.InventoryTransactions
            .Include(t => t.Product)
            .Where(t => t.WarehouseId == warehouseId)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();

        return transactions.Select(MapToDto).ToList();
    }
    
    /*public async Task<List<InventoryTransactionDto>> GetByType(TransactionType type)
    {
        return await context.InventoryTransactions
            .Include(t => t.Product)
            .Include(t => t.Warehouse)
            .Where(t => t.TransactionType == type)
            .OrderByDescending(t => t.TransactionDate)
            .Select(t => MapToDto(t))
            .ToListAsync();
    }*/

 /*   public async Task<int> GetProductQuantityHistory(Guid productId)
    {
        return await context.InventoryTransactions
            .Where(t => t.ProductId == productId)
            .SumAsync(t => t.TransactionType == TransactionType.Incoming ? t.Quantity : -t.Quantity);
    }*/

    private static InventoryTransactionDto MapToDto(InventoryTransaction transaction)
    {
        return new InventoryTransactionDto
        {
            Id = transaction.Id,
            //ProductId = transaction.ProductId,
            Product = transaction.Product != null ? new ProductShortDto
            {
                Id = transaction.Product.Id,
                Name = transaction.Product.Name,
                Unit = transaction.Product.Unit
            } : null,
           //WarehouseId = transaction.WarehouseId,
            Warehouse = transaction.Warehouse != null ? new WarehouseShortDto
            {
                Id = transaction.Warehouse.Id,
                Name = transaction.Warehouse.Name,
                Location = transaction.Warehouse.Location,
            } : null,
            Quantity = transaction.Quantity,
            //TransactionType = transaction.TransactionType,
            TransactionDate = transaction.TransactionDate,
            CreatedAt = transaction.CreatedAt,
            UpdatedAt = transaction.UpdatedAt
        };
    }
}