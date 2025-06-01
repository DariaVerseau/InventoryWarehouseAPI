using DTO.Product;

namespace BLL.DTOs;

public class ProductWithStockDto
{
    public ProductDto Product { get; set; }
    public int CurrentStock { get; set; }
}