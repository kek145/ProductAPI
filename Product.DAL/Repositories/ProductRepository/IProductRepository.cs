using System.Linq;
using Product.Domain.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;
using Product.Domain.Helpers;

namespace Product.DAL.Repositories.ProductRepository;

public interface IProductRepository
{
    Task DeleteProductAsync(int productId);
    IQueryable<ProductDto> GetAllProducts();
    Task AddProductAsync(ProductDto product);
    Task UpdateProductAsync(ProductDto product);
    Task<ProductDto> GetProductByIdAsync(int productId);
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<PagedResult<ProductDto>> GetAllProductAsync<TResult>(QueryParameters queryParameters);
}