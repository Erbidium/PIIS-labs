using PathFindingLab1.BLL;
using PathfindingLab1.ConsoleApp.Exceptions;
using PathfindingLab1.ConsoleApp.Helpers;

namespace PathfindingLab1.ConsoleApp;

public static class Menu
{
    public static int GetPointNumber(int x, int y, int width)
    {
        return y * width + x;
    }
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

        var adjacencyMatrix = new int[fieldHeight * fieldWidth, fieldHeight * fieldWidth];
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
            Console.WriteLine();
        }

        for(var i = 0; i < fieldHeight; i++)
        {
            for (var j = 0; j < fieldWidth; j++)
            {
                Console.Write($"{fieldMatrix[i, j]} ");
                if (fieldMatrix[i, j] != 0) continue;

                if (FieldMatrixHelpers.TopNeighboringCellIsFree(i, j, fieldMatrix))
                {
                    adjacencyMatrix[GetPointNumber(j, i, fieldWidth), GetPointNumber(j, i - 1, fieldWidth)] = 1;
                }
                if (FieldMatrixHelpers.BottomNeighboringCellIsFree(i, j, fieldMatrix, fieldHeight))
                {
                    adjacencyMatrix[GetPointNumber(j, i, fieldWidth), GetPointNumber(j, i + 1, fieldWidth)] = 1;
                }
                if (FieldMatrixHelpers.LeftNeighboringCellIsFree(i ,j, fieldMatrix))
                {
                    adjacencyMatrix[GetPointNumber(j, i, fieldWidth), GetPointNumber(j - 1, i, fieldWidth)] = 1;
                }
                if (FieldMatrixHelpers.RightNeighboringCellIsFree(i, j, fieldMatrix, fieldWidth))
                {
                    adjacencyMatrix[GetPointNumber(j, i, fieldWidth), GetPointNumber(j + 1, i, fieldWidth)] = 1;
                }
                if (FieldMatrixHelpers.TopNeighboringCellIsFree(i, j, fieldMatrix) &&
                    FieldMatrixHelpers.RightNeighboringCellIsFree(i, j, fieldMatrix, fieldWidth) &&
                    fieldMatrix[i - 1, j + 1] == 0)
                {
                    adjacencyMatrix[GetPointNumber(j, i, fieldWidth), GetPointNumber(j + 1, i - 1, fieldWidth)] = 1;
                }
                if (FieldMatrixHelpers.TopNeighboringCellIsFree(i, j, fieldMatrix) &&
                    FieldMatrixHelpers.LeftNeighboringCellIsFree(i, j, fieldMatrix) &&
                    fieldMatrix[i - 1, j - 1] == 0)
                {
                    adjacencyMatrix[GetPointNumber(j, i, fieldWidth), GetPointNumber(j - 1, i - 1, fieldWidth)] = 1;
                }
                if (FieldMatrixHelpers.BottomNeighboringCellIsFree(i, j, fieldMatrix, fieldHeight) &&
                    FieldMatrixHelpers.RightNeighboringCellIsFree(i, j, fieldMatrix, fieldWidth) &&
                    fieldMatrix[i + 1, j + 1] == 0)
                {
                    adjacencyMatrix[GetPointNumber(j, i, fieldWidth), GetPointNumber(j + 1, i + 1, fieldWidth)] = 1;
                }
                if (FieldMatrixHelpers.BottomNeighboringCellIsFree(i, j, fieldMatrix, fieldHeight) &&
                    FieldMatrixHelpers.LeftNeighboringCellIsFree(i, j, fieldMatrix) &&
                    fieldMatrix[i + 1, j - 1] == 0)
                {
                    adjacencyMatrix[GetPointNumber(j, i, fieldWidth), GetPointNumber(j - 1, i + 1, fieldWidth)] = 1;
                }
                
            }
            Console.WriteLine();
        }

        for (var i = 0; i < adjacencyMatrix.GetLength(0); i++)
        {
            for (var j = 0; j < adjacencyMatrix.GetLength(1); j++)
            {
                Console.Write($"{adjacencyMatrix[i, j]} ");
            }
            Console.WriteLine();
        }

        var AStarAlgorithm = new AStarAlgorithm();

        foreach (var point in AStarAlgorithm.GetPath(adjacencyMatrix, fieldMatrix, GetPointNumber(startPoint.Value.Item1, startPoint.Value.Item2, fieldWidth), GetPointNumber(endPoint.Value.Item1, endPoint.Value.Item2, fieldWidth)))
        {
            Console.WriteLine(point);
        }
        
        var LeeAlgo = new LeeAlgorithm();

        Console.WriteLine();
        foreach (var point in LeeAlgo.GetPath(adjacencyMatrix, fieldMatrix, GetPointNumber(startPoint.Value.Item1, startPoint.Value.Item2, fieldWidth), GetPointNumber(endPoint.Value.Item1, endPoint.Value.Item2, fieldWidth)))
        {
            Console.WriteLine(point);
        }
        
        //cout<<"Fail! Way is not found!"<<endl;
    }
}