using MediatR;
using Product.Domain.Requests;

namespace Product.BLL.Commands.ProductCommands.Create;

public class CreateProductCommand : IRequest<int>
{
    public ProductRequest ProductRequest { get; private set; }

    public CreateProductCommand(ProductRequest productRequest)
    {
        ProductRequest = productRequest;
    }
}