using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Product.DAL.Repositories.ProductRepository;
using Product.Domain.Responses;

namespace Product.BLL.Queries.ProductQueries.GetById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(request.ProductId);

        var result = _mapper.Map<ProductResponse>(product);
        
        return result;
    }
}