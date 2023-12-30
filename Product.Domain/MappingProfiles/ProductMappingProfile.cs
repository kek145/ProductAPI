using AutoMapper;
using Product.Domain.DTOs;
using Product.Domain.Helpers;
using Product.Domain.Requests;
using Product.Domain.Responses;

namespace Product.Domain.MappingProfiles;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<DbSet.Product, ProductDto>();
        CreateMap<ProductDto, DbSet.Product>();

        CreateMap<ProductRequest, ProductDto>()
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description,
                opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Price,
                opt => opt.MapFrom(src => src.Price));
        
        CreateMap<ProductDto, ProductResponse>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description,
                opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Price,
                opt => opt.MapFrom(src => src.Price));
        
        CreateMap<PagedResult<ProductDto>, PagedResult<ProductResponse>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items)); // Укажите свойство Items, если оно отличается
    }
}