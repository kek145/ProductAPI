using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateProduct()
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        return Ok();
    }

    [HttpGet]
    [Route("{productId:int}")]
    public async Task<IActionResult> GetProductById(int productId)
    {
        return Ok();
    }

    [HttpPut]
    [Route("{productId:int}/update")]
    public async Task<IActionResult> UpdateProduct(int productId)
    {
        return NoContent();
    }

    [HttpDelete]
    [Route("{productId:int}/delete")]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        return NoContent();
    }
}