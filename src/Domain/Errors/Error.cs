namespace Domain.Errors;

public record Error(int StatusCode, string? Message = null)
{
    public static readonly Error None = new(200);
    public static implicit operator Result(Error error) => Result.Failure(error);
}
