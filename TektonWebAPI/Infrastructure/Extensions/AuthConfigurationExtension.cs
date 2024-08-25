using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TektonWebAPI.Application.Auth;

namespace TektonWebAPI.Infrastructure.Extensions;

public static class AuthConfigurationExtension
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
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

        return services;
    }
}
