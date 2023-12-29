using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Product.DAL.Repositories.ProductRepository;
using Product.Domain.DTOs;

namespace Product.BLL.Commands.ProductCommands.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }
    
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<ProductDto>(request.ProductRequest);

        await _productRepository.AddProductAsync(product);

        return product.Id;
    }
}