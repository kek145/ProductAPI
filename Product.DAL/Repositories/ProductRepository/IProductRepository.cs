using System.Linq;
using Product.Domain.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Product.DAL.Repositories.ProductRepository;

public interface IProductRepository
{
    Task DeleteProductAsync(int productId);
    IQueryable<ProductDto> GetAllProducts();
    Task AddProductAsync(ProductDto product);
    Task UpdateProductAsync(ProductDto product);
    Task<ProductDto> GetProductByIdAsync(int productId);
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
}