
public class CommandResult
{
    public bool IsSuccess { get; private set; }
    public bool IsNotFound { get; private set; }
    public string ErrorMessage { get; private set; }

    private CommandResult(bool isSuccess, bool isNotFound, string errorMessage)
    {
        IsSuccess = isSuccess;
        IsNotFound = isNotFound;
        ErrorMessage = errorMessage;
    }

    public static CommandResult Success() => new CommandResult(true, false, string.Empty);
    public static CommandResult NotFound() => new CommandResult(false, true, string.Empty);
    public static CommandResult Failure(string errorMessage) => new CommandResult(false, false, errorMessage);
}
