using DTO.Product;

namespace DAL.Interfaces;

public interface IProductRepository : IRepository<ProductDto, CreateProductDto, UpdateProductDto>
{
    Task<List<ProductDto>> GetByCategoryId(Guid categoryId);
    Task<List<ProductDto>> GetBySupplierId(Guid supplierId);
    Task<int> GetTotalStockQuantity(Guid productId);
    
}