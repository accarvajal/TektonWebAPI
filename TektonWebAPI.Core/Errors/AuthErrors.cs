namespace TektonWebAPI.Core.Errors;

public static class AuthErrors
{
    public const string AuthInvalidCredentials = "Auth.InvalidCredentials";

    public static Error InvalidCredentials() => new(
        AuthInvalidCredentials, "Invalid username or password");

}
