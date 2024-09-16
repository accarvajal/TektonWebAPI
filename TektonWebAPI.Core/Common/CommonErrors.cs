using TektonWebAPI.Core.Errors;

namespace TektonWebAPI.Core.Common;

public static class CommonErrors
{
    public const string CommonGeneralError = "Common.GeneralError";

    public static Error GeneralError(string description) => new(CommonGeneralError, description ?? "An unexpected error occurred");
}
