using PathFindingLab1.BLL.Services;
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

        var fieldMatrix = new int[fieldHeight, fieldWidth];
        
        var startPoint = ConsoleReader.ReadStartPointCoordinates();
        if (startPoint is null)
        {
            throw new WrongInputException();
        }
        ValuesValidator.PointIsValid(startPoint.Value, fieldMatrix);

        var endPoint = ConsoleReader.ReadEndPointCoordinates();
        if (endPoint is null)
        {
            throw new WrongInputException();
        }
        ValuesValidator.PointIsValid(endPoint.Value, fieldMatrix);

        if (startPoint.Value.Item1 == endPoint.Value.Item1 && startPoint.Value.Item2 == endPoint.Value.Item2)
        {
            throw new ValidationException("Start cannot be same as end");
        }
        if (fieldMatrix[startPoint.Value.Item2, startPoint.Value.Item1] == 1)
        {
            throw new ValidationException("Start is not free");
        }
        if (fieldMatrix[endPoint.Value.Item2, endPoint.Value.Item1] == 1)
        {
            throw new ValidationException("End is not free");
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
        }

        var adjacencyMatrix = FieldService.GetAdjacencyMatrix(fieldMatrix);

        var pathFindingService = new PathFindingService();

        var (aStarPathFrom, aStarPathLength) = pathFindingService.AStarAlgorithm(adjacencyMatrix, fieldMatrix,
            FieldService.GetPointNumber(startPoint.Value.Item1, startPoint.Value.Item2, fieldWidth),
            FieldService.GetPointNumber(endPoint.Value.Item1, endPoint.Value.Item2, fieldWidth));
        var (leePathFrom, leePathLength) = pathFindingService.LeeAlgorithm(adjacencyMatrix, fieldMatrix,
            FieldService.GetPointNumber(startPoint.Value.Item1, startPoint.Value.Item2, fieldWidth),
            FieldService.GetPointNumber(endPoint.Value.Item1, endPoint.Value.Item2, fieldWidth));

        if (leePathFrom.Length == 0)
        {
            Console.WriteLine("Fail! Lee algorithm cannot find path!");
        }
        else
        {
            Console.WriteLine("Lee algorithm");
            Console.WriteLine($"Path length: {leePathLength}");
            ConsolePrinter.PrintAlgorithmResults(leePathFrom, FieldService.GetPointNumber(startPoint.Value.Item1, startPoint.Value.Item2, fieldWidth), FieldService.GetPointNumber(endPoint.Value.Item1, endPoint.Value.Item2, fieldWidth), fieldMatrix);
        }
        Console.WriteLine();
        if (aStarPathFrom.Length == 0)
        {
            Console.WriteLine("Fail! AStar algorithm cannot find path!");
        }
        else
        {
            Console.WriteLine("AStar algorithm");
            Console.WriteLine($"Path length: {aStarPathLength}");
            ConsolePrinter.PrintAlgorithmResults(aStarPathFrom, FieldService.GetPointNumber(startPoint.Value.Item1, startPoint.Value.Item2, fieldWidth), FieldService.GetPointNumber(endPoint.Value.Item1, endPoint.Value.Item2, fieldWidth), fieldMatrix);
        }
    }
}