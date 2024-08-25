namespace TektonWebAPI.Application.Features.Auth.Commands;

public class LoginCommand : IRequest<Result<string>>
{
    public string Username { get; set; }
    public string Password { get; set; }

    public LoginCommand(string username, string password)
    {
        Username = username;
        Password = password;
    }
}