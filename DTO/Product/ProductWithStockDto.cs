namespace DTO.Product;

public class ProductWithStockDto
{
    public ProductDto Product { get; set; }
    public int TotalQuantity { get; set; }
    public DateTime? LastTransactionDate { get; set; }
}
