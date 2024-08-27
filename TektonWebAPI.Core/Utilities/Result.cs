namespace TektonWebAPI.Core.Utilities;

public class Result
{
    public bool IsSuccess { get; set; }
    public string Error { get; set; } = string.Empty;
    public bool IsFailure => !IsSuccess;
    public ErrorCode ErrorCode { get; set; }

    public static Result Success() => new() { IsSuccess = true, ErrorCode = ErrorCode.None };
    public static Result Failure(string error, ErrorCode errorCode) => new() { IsSuccess = false, Error = error, ErrorCode = errorCode };
}