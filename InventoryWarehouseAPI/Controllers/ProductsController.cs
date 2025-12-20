using BLL.Interfaces;
using DAL.Entities;
using DTO.Product;
using DTO.PagedResponse;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponse<ProductDto>>> GetProducts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        if (page < 1) page = 1;
        if (pageSize is < 1 or > 50) pageSize = 10;

        var result = await _productService.GetProductsPaged(page, pageSize);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Product>> GetById(Guid id)
    {
        var inventory = await _productService.GetById(id);
        return Ok(inventory);
    }
    
    [HttpGet("filtered")]
    public async Task<ActionResult<PagedResponse<ProductDto>>> GetFiltered([FromQuery] ProductFilterDto filter)
    {
        var result = await _productService.GetFilteredProducts(filter);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto createProductDto)
    {
        var createdProduct = await _productService.Create(createProductDto);
        return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
    }

    [HttpPut]
    public async Task<ActionResult<ProductDto>> Update([FromBody] UpdateProductDto updateProductDto)
    {
        var updatedProduct = await _productService.Update(updateProductDto);
        return Ok(updatedProduct);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _productService.Delete(id);
        return NoContent();
    }
}