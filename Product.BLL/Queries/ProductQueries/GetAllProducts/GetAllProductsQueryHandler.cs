using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Product.Domain.Responses;
using System.Collections.Generic;
using Product.DAL.Repositories.ProductRepository;

namespace Product.BLL.Queries.ProductQueries.GetAllProducts;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponse>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetAllProductsQueryHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }
    public async Task<IEnumerable<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllProductsAsync();

        var result = _mapper.Map<IEnumerable<ProductResponse>>(products);

        return result;
    }
}