using AutoMapper;
using TektonWebAPI.Application.Mappers.Resolvers;

namespace TektonWebAPI.Infrastructure.Extensions;

public static class CustomMappingsExtension
{
    public static IServiceCollection AddCustomMappings(this IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product, ProductRequestDto>().ReverseMap();
            cfg.CreateMap<Product, ProductResponseDto>()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom<StatusNameResolver>());
        });

        var mapper = config.CreateMapper();

        services.AddSingleton(mapper);

        return services;
    }
}
