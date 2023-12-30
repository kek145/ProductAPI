using System.Collections.Generic;
using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Product.Domain.Helpers;
using Product.Domain.Responses;
using Product.DAL.Repositories.ProductRepository;

namespace Product.BLL.Queries.ProductQueries.GetAllProducts;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedResult<ProductResponse>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetAllProductsQueryHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }
    public async Task<PagedResult<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllProductAsync<PagedResult<ProductResponse>>(request.QueryParameters);

        var result = _mapper.Map<PagedResult<ProductResponse>>(products);

        return result;
    }
}