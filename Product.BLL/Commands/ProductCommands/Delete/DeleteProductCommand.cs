using MediatR;

namespace Product.BLL.Commands.ProductCommands.Delete;

public class DeleteProductCommand : IRequest
{
    public int ProductId { get; private set; }

    public DeleteProductCommand(int productId)
    {
        ProductId = productId;
    }
}