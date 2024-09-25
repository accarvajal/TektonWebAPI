using Microsoft.IdentityModel.Tokens;
using System.Text;
using TektonWebAPI.Application.Abstractions;
using TektonWebAPI.Application.Auth;
using TektonWebAPI.Application.Services;

namespace TektonWebAPI.Infrastructure.Extensions;

internal static class AuthServiceExtension
{
    internal static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

        var jwtSettings = new JwtSettings();
        configuration.Bind(nameof(JwtSettings), jwtSettings);

        var secret = Encoding.UTF8.GetBytes(jwtSettings.Secret);

        if (secret.Length < 32)
        {
            throw new InvalidOperationException("The JWT Secret key must be at least 32 characters long.");
        }

        services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(secret)
            };
        });

        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
