using AutoMapper;
using Product.Domain.DTOs;

namespace Product.Domain.MappingProfiles;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<DbSet.Product, ProductDto>();
        CreateMap<ProductDto, DbSet.Product>();
    }
}