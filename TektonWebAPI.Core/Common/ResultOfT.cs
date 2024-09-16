using TektonWebAPI.Core.Errors;

namespace TektonWebAPI.Core.Common;

public class Result<T>(bool isSuccess, Error error, T? value) : Result(isSuccess, error)
{
    public T? Value { get; private set; } = value;

    public static Result<T> Success(T? value) => new(true, Error.None, value);
    public static new Result<T> Failure(Error error) => new(false, error, default);
}