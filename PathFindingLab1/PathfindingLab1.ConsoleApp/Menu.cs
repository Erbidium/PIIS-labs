using PathfindingLab1.ConsoleApp.Exceptions;

namespace PathfindingLab1.ConsoleApp;

public static class Menu
{
    public static void ShowMenu()
    {
        var filePath = ConsoleReader.ReadFilePath();
        if (filePath is null)
        {
            throw new FileNotFoundException("File with field doesn't exist");
        }

        var fileLines = File.ReadLines(filePath).ToList();
        var fieldHeight = fileLines.Count;
        if (fieldHeight == 0)
        {
            throw new ValidationException("Field height cannot be zero!");
        }
        var fieldWidth = fileLines[0].Length;
        ValuesValidator.FieldWidthIsValid(fieldWidth);

        var adjacencyMatrix = new int[fieldWidth, fieldHeight];
        var fieldMatrix = new int[fieldWidth, fieldHeight];
        
        var startPoint = ConsoleReader.ReadStartPointCoordinates();
        if (startPoint is null)
        {
            throw new WrongInputException();
        }
        ValuesValidator.PointIsValid(startPoint.Value, fieldMatrix);
        Console.WriteLine($"Start point x: {startPoint.Value.Item1} y: {startPoint.Value.Item2}");
        var endPoint = ConsoleReader.ReadEndPointCoordinates();
        if (endPoint is null)
        {
            throw new WrongInputException();
        }
        ValuesValidator.PointIsValid(endPoint.Value, fieldMatrix);
        Console.WriteLine($"End point x: {endPoint.Value.Item1} y: {endPoint.Value.Item2}");
        if (startPoint.Value.Item1 == endPoint.Value.Item1 && startPoint.Value.Item2 == endPoint.Value.Item2)
        {
            throw new ValidationException("Start cannot be same as end");
        }

        for(var i = 0; i < fileLines.Count; i++)
        {
            if (fileLines[i].Length < fieldWidth)
            {
                throw new ValidationException("Field should have rectangle shape!");
            }
            for(var j = 0; j < fileLines[i].Length; j++)
            {
                if (!int.TryParse(fileLines[i][j].ToString(), out var pointValue) || pointValue > 1)
                {
                    Console.WriteLine("Wrong point value");
                    return;
                }

                fieldMatrix[i, j] = pointValue;
            }
            Console.WriteLine();
        }

        for(var i = 0; i < fieldMatrix.GetLength(0); i++)
        {
            for (var j = 0; j < fieldMatrix.GetLength(1); j++)
            {
                Console.Write($"{fieldMatrix[i, j]} ");
            }
            Console.WriteLine();
        }
    }
}