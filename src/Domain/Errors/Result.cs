namespace Domain.Errors;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None
          || !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid Error", nameof(Error));
        }
        IsSuccess = isSuccess;
        Error = error;
    }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
}
public class Result<TResult> : Result
{
    public TResult Data { get; }
    private Result(TResult data)
        : base(true, Error.None)
    {
        Data = data;
    }
    public static Result<TResult> Success(TResult data) => new(data);

}
