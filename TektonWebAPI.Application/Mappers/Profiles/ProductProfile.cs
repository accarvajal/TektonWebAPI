using TektonWebAPI.Application.Mappers.Resolvers;

namespace TektonWebAPI.Application.Mappers.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductRequestDto>().ReverseMap();
        CreateMap<Product, ProductResponseDto>()
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom<StatusNameResolver>());
    }
}