using PathfindingLab1.ConsoleApp.Exceptions;

namespace PathfindingLab1.ConsoleApp;

public static class ValuesValidator
{
    public static bool FilePathIsValid(string pathToFile) =>
        File.Exists(pathToFile);

    public static void FieldWidthIsValid(int fieldWidth)
    {
        if (fieldWidth == 0)
        {
            throw new ValidationException("Field width cannot be zero!");
        }
    }

    public static void PointIsValid((int, int) point, int [,] fieldMatrix)
    {
        if (point.Item1 < 0 || point.Item2 < 0)
        {
            throw new ValidationException("End point coordinates cannot be negative");
        }

        if (point.Item1 >= fieldMatrix.GetLength(1) || point.Item2 >= fieldMatrix.GetLength(0))
        {
            throw new ValidationException("Point is out of field!");
        }
    }
}