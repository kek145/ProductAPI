using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Product.DAL.Repositories.ProductRepository;

namespace Product.BLL.Commands.ProductCommands.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(request.ProductId);

        product.Name = request.ProductRequest.Name;
        product.Description = request.ProductRequest.Name;
        product.Price = request.ProductRequest.Price;
        
        await _productRepository.UpdateProductAsync(product);
    }
}