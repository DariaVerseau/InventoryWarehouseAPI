using DTO.Product;

namespace BLL.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetProducts();
    Task<ProductDto> GetProduct(Guid id);
    Task<ProductDto> CreateProduct(CreateProductDto product);
    Task<ProductDto> UpdateProduct(UpdateProductDto product);
    Task DeleteProduct(Guid id);
}