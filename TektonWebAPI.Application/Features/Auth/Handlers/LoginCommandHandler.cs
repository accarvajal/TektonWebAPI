using TektonWebAPI.Application.Features.Auth.Commands;

namespace TektonWebAPI.Application.Features.Auth.Handlers;

public class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, Result<string>>
{
    private readonly IAuthService _authService = authService;

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _authService.LoginAsync(request.Username, request.Password);
    }
}