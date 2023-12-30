using System.Collections.Generic;
using MediatR;
using Product.Domain.Helpers;
using Product.Domain.Responses;

namespace Product.BLL.Queries.ProductQueries.GetAllProducts;

public class GetAllProductsQuery : IRequest<PagedResult<ProductResponse>>
{
    public QueryParameters QueryParameters { get; private set; }

    public GetAllProductsQuery(QueryParameters queryParameters)
    {
        QueryParameters = queryParameters;
    }
}