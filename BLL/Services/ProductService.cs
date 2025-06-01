using AutoMapper;

using DAL.Entities;
using DAL.Interfaces;
using DTO.Product;

namespace BLL.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepo;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepo, IMapper mapper)
    {
        _productRepo = productRepo;
        _mapper = mapper;
    }

    public async Task<ProductDto> GetByIdAsync(Guid id)
    {
        var product = await _productRepo.GetById(id);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        var products = await _productRepo.GetAll();
        return _mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto> CreateAsync(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        var created = await _productRepo.Create(product);
        return _mapper.Map<ProductDto>(created);
    }

    public async Task<ProductDto> UpdateAsync(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        var updated = await _productRepo.Update(product);
        return _mapper.Map<ProductDto>(updated);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _productRepo.Delete(id);
    }

    public async Task<List<ProductWithStockDto>> GetProductsLowStockAsync(int threshold)
    {
        var products = await _productRepo.GetAll();
        var result = new List<ProductWithStockDto>();

        foreach (var product in products)
        {
            var stock = await _productRepo.GetTotalStockQuantity(product.Id);
            if (stock < threshold)
            {
                result.Add(new ProductWithStockDto
                {
                    Product = _mapper.Map<ProductDto>(product),
                    CurrentStock = stock
                });
            }
        }

        return result;
    }

    public async Task<List<ProductDto>> SearchAsync(string searchTerm)
    {
        var products = await _productRepo.SearchByName(searchTerm);
        return _mapper.Map<List<ProductDto>>(products);
    }
}