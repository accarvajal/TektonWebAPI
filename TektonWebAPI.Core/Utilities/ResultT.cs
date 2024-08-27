namespace TektonWebAPI.Core.Utilities;

public class Result<T> : Result
{
    public T? Value { get; private set; }

    public static Result<T> Success(T? value) => new() { IsSuccess = true, Value = value, ErrorCode = ErrorCode.None };
    public static Result<T> Failure(string error, ErrorCode errorCode) => new() { IsSuccess = false, Error = error, ErrorCode = errorCode };
}