using DTO.Product;

namespace BLL.Services;

public interface IProductService
{
    Task<ProductDto> GetByIdAsync(Guid id);
    Task<List<ProductDto>> GetAllAsync();
    Task<ProductDto> CreateAsync(ProductDto productDto);
    Task<ProductDto> UpdateAsync(ProductDto productDto);
    Task<bool> DeleteAsync(Guid id);
    Task<List<ProductWithStockDto>> GetProductsLowStockAsync(int threshold);
    Task<List<ProductDto>> SearchAsync(string searchTerm);
}