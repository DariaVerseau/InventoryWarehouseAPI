using BLL.Interfaces;
using DAL.Interfaces;
using DTO.Product;

namespace BLL.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<List<ProductDto>> GetProducts() 
        => await productRepository.GetAll();

    public async Task<ProductDto> GetProduct(Guid id) 
        => await productRepository.GetById(id);

    public async Task<ProductDto> CreateProduct(CreateProductDto productDto) 
        => await productRepository.Create(productDto);

    public async Task<ProductDto> UpdateProduct(UpdateProductDto productDto) 
        => await productRepository.Update(productDto);

    public async Task DeleteProduct(Guid id) 
        => await productRepository.Delete(id);
}