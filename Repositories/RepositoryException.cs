public class RepositoryException : Exception
{
    public RepositoryException(string message, Exception? innerException)
        : base(message, innerException)
    { }
}