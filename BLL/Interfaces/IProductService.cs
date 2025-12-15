using DTO.PagedResponse;
using DTO.Product;

namespace BLL.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetAll();
    Task<ProductDto?> GetById(Guid id);
    Task<ProductDto> Create(CreateProductDto dto);
    Task<ProductDto> Update(UpdateProductDto dto);
    Task<bool> Delete(Guid id);
    Task<List<ProductDto>> GetByCategoryId(Guid categoryId);
    Task<List<ProductDto>> GetBySupplierId(Guid supplierId);
    Task<int> GetTotalStockQuantity(Guid productId);
    Task<PagedResponse<ProductDto>> GetProductsPaged(int page, int pageSize);
}