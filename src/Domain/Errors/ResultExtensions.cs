namespace Domain.Errors;

public static class ResultExtensions
{
    public static TResultType GetData<TResultType>(this Result result)
    {
        return ((Result<TResultType>)result).Data;
    }
}
