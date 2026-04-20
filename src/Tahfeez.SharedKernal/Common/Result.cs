namespace Tahfeez.SharedKernal.Common;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }
    public IEnumerable<string>? ErrorsList { set; get; }

    protected Result(bool isSuccess, string error, IEnumerable<string> errorsList)
    {
        IsSuccess = isSuccess;
        Error = error;
        ErrorsList = errorsList;
    }

    public static Result Success() => new(true, string.Empty, null);
    public static Result Failure(string error, IEnumerable<string> errorsList = null) => new(false, error, errorsList);

    public static Result<T> Success<T>(T value) => new(value, true, string.Empty, null);
    public static Result<T> Failure<T>(string error, IEnumerable<string> errorsList = null) => new(default!, false, error, errorsList);
}

public class Result<T> : Result
{
    public T Value { get; }

    protected internal Result(T value, bool isSuccess, string error, IEnumerable<string> errorsList)
        : base(isSuccess, error, errorsList)
    {
        Value = value;
    }
}
