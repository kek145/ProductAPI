using MediatR;
using Product.Domain.Responses;

namespace Product.BLL.Queries.ProductQueries.GetById;

public class GetProductByIdQuery : IRequest<ProductResponse>
{
    public int ProductId { get; private set; }

    public GetProductByIdQuery(int productId)
    {
        ProductId = productId;
    }
}