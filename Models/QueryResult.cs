public class QueryResult<T>
{
    public T Result { get; }
    public bool IsSuccess { get; }
    public string ErrorMessage { get; }
    public bool IsNotFound => !IsSuccess && Result == null;

    private QueryResult(T result, bool isSuccess, string errorMessage)
    {
        Result = result;
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public static QueryResult<T> Success(T result) => new QueryResult<T>(result, true, null);
    public static QueryResult<T> Failure(string errorMessage) => new QueryResult<T>(default, false, errorMessage);
    public static QueryResult<T> NotFound() => new QueryResult<T>(default, false, "Item não encontrado.");
}
