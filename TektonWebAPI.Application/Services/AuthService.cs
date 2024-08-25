using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TektonWebAPI.Application.Auth;

namespace TektonWebAPI.Application.Services;

public class AuthService(IOptions<JwtSettings> jwtSettings) : IAuthService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    private const string USERNAME_MOCK = "admin";
    private const string PASSWORD_MOCK = "password";

    public async Task<Result<string>> LoginAsync(string username, string password)
    {
        if (username == USERNAME_MOCK && password == PASSWORD_MOCK)
        {
            return Result<string>.Success(await Task.FromResult(GenerateToken(username)));
        }

        return Result<string>.Failure("Invalid username or password.");
    }

    private string GenerateToken(string username)
    {
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, username)
            }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}