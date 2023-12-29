using MediatR;
using System.Threading.Tasks;
using Product.Domain.Requests;
using Product.Domain.Responses;
using System.Collections.Generic;
using Product.BLL.Queries.ProductQueries.GetById;
using Product.BLL.Commands.ProductCommands.Create;
using Product.BLL.Commands.ProductCommands.Delete;
using Product.BLL.Commands.ProductCommands.Update;
using Product.BLL.Queries.ProductQueries.GetAllProducts;

namespace Product.BLL.Services.ProductService;

public class ProductService : IProductService
{
    private readonly IMediator _mediator;

    public ProductService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<int> CreateProductAsync(ProductRequest request)
    {
        var command = new CreateProductCommand(request);

        var createCommand = await _mediator.Send(command);

        return createCommand;
    }

    public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
    {
        var query = new GetAllProductsQuery();

        var getAllQuery = await _mediator.Send(query);

        return getAllQuery;
    }

    public async Task<ProductResponse> GetProductByIdAsync(int productId)
    {
        var query = new GetProductByIdQuery(productId);

        var getProductById = await _mediator.Send(query);

        return getProductById;
    }

    public async Task UpdateProductAsync(int productId, ProductRequest request)
    {
        var command = new UpdateProductCommand(productId, request);
        await _mediator.Send(command);
    }

    public async Task DeleteProductAsync(int productId)
    {
        var command = new DeleteProductCommand(productId);
        await _mediator.Send(command);
    }
}