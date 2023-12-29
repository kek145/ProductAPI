using System.Collections.Generic;
using MediatR;
using Product.Domain.Responses;

namespace Product.BLL.Queries.ProductQueries.GetAllProducts;

public class GetAllProductsQuery : IRequest<IEnumerable<ProductResponse>>
{
    
}