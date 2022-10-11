namespace PathfindingLab1.ConsoleApp.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message)
    {}
}