using MediatR;
using Product.Domain.Requests;

namespace Product.BLL.Commands.ProductCommands.Update;

public class UpdateProductCommand : IRequest
{
    public int ProductId { get; private set; }
    public ProductRequest ProductRequest { get; private set; }

    public UpdateProductCommand(int productId, ProductRequest productRequest)
    {
        ProductId = productId;
        ProductRequest = productRequest;
    }
}