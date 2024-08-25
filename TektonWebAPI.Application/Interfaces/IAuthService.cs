namespace TektonWebAPI.Application.Interfaces;

public interface IAuthService
{
    Task<Result<string>> LoginAsync(string username, string password);
}
