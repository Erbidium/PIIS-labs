namespace PathfindingLab1.ConsoleApp;

public static class ConsoleReader
{
    public static string? ReadFilePath()
    {
        Console.WriteLine("Please, enter path to file with labyrinth!: ");
        var pathToFile = Console.ReadLine() ?? "";

        return ValuesValidator.FilePathIsValid(pathToFile)
            ? pathToFile
            : null;
    }

    public static (int, int)? ReadStartPointCoordinates()
    {
        Console.WriteLine("Please, enter x coordinate of start point: ");
        if (!int.TryParse(Console.ReadLine(), out var xStartPoint))
        {
            return null;
        }
        Console.WriteLine("Please, enter y coordinate of start point: ");
        if (!int.TryParse(Console.ReadLine(), out var yStartPoint))
        {
            return null;
        }

        return (xStartPoint, yStartPoint);
    }

    public static (int, int)? ReadEndPointCoordinates()
    {
        Console.WriteLine("Please, enter x coordinate of end point: ");
        if (!int.TryParse(Console.ReadLine(), out var xEndPoint))
        {
            return null;
        }
        Console.WriteLine("Please, enter y coordinate of end point: ");
        if (!int.TryParse(Console.ReadLine(), out var yEndPoint))
        {
            return null;
        }

        return (xEndPoint, yEndPoint);
    }
}