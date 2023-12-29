using System.Collections.Generic;
using System.Threading.Tasks;
using Product.Domain.Requests;
using Product.Domain.Responses;

namespace Product.BLL.Services.ProductService;

public interface IProductService
{
    Task<int> CreateProductAsync(ProductRequest request);
    Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
    Task<ProductResponse> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(int productId, ProductRequest request);
    Task DeleteProductAsync(int productId);
}