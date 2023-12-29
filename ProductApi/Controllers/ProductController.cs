using System.Threading.Tasks;
using Product.Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using Product.BLL.Services.ProductService;

namespace ProductApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateProduct([FromBody] ProductRequest request)
    {
        var response = await _productService.CreateProductAsync(request);
        return CreatedAtAction(nameof(GetProductById), new { productId = response}, new { productId = response });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var response = await _productService.GetAllProductsAsync();
        return Ok(response);
    }

    [HttpGet]
    [Route("{productId:int}")]
    public async Task<IActionResult> GetProductById(int productId)
    {
        var response = await _productService.GetProductByIdAsync(productId);
        return Ok(response);
    }

    [HttpPut]
    [Route("{productId:int}/update")]
    public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductRequest request)
    {
        await _productService.UpdateProductAsync(productId, request);
        return NoContent();
    }

    [HttpDelete]
    [Route("{productId:int}/delete")]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        await _productService.DeleteProductAsync(productId);
        return NoContent();
    }
}