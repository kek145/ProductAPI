using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Product.DAL.Repositories.ProductRepository;

namespace Product.BLL.Commands.ProductCommands.Delete;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.DeleteProductAsync(request.ProductId);
    }
}