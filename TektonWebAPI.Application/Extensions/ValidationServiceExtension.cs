using FluentValidation;
using FluentValidation.AspNetCore;
using TektonWebAPI.Application.Validations;

namespace TektonWebAPI.Application.Extensions;

internal static class ValidationServiceExtension
{
    internal static IServiceCollection AddValidationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ProductValidator>();
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        return services;
    }

}
