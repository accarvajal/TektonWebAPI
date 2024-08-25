using FluentValidation;
using FluentValidation.AspNetCore;
using TektonWebAPI.Application.Validations;

namespace TektonWebAPI.Infrastructure.Extensions;

public static class ValidationServiceExtension
{
    public static IServiceCollection AddValidationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ProductValidator>();
        services.AddFluentValidationAutoValidation();

        return services;
    }

}
