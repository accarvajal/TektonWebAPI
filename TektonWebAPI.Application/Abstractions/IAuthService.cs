namespace TektonWebAPI.Application.Abstractions;

public interface IAuthService
{
    Task<Result<string>> LoginAsync(string username, string password);
}
