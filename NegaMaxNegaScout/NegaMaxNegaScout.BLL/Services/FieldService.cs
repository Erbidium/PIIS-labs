using PathFindingLab1.BLL.Helpers;

namespace PathFindingLab1.BLL.Services;

public static class FieldService
{
    public static int GetPointNumber(int x, int y, int width)
    {
        return y * width + x;
    }

    public static (int, int) GetPointCoordinates(int pointNumber, int width) =>
        (pointNumber % width, pointNumber / width);

    public static int[,] GetAdjacencyMatrix(int[,] fieldMatrix)
    {
        var fieldHeight = fieldMatrix.GetLength(0);
        var fieldWidth = fieldMatrix.GetLength(1);
        var adjacencyMatrix = new int[fieldHeight * fieldWidth, fieldHeight * fieldWidth];
        for (var i = 0; i < fieldHeight; i++)
        {
            for (var j = 0; j < fieldWidth; j++)
            {
                if (fieldMatrix[i, j] != 0) continue;

                if (FieldMatrixHelpers.TopNeighboringCellIsFree(i, j, fieldMatrix))
                {
                    adjacencyMatrix[GetPointNumber(j, i, fieldWidth), GetPointNumber(j, i - 1, fieldWidth)] = 1;
                }

                if (FieldMatrixHelpers.BottomNeighboringCellIsFree(i, j, fieldMatrix, fieldHeight))
                {
                    adjacencyMatrix[GetPointNumber(j, i, fieldWidth), GetPointNumber(j, i + 1, fieldWidth)] = 1;
                }

                if (FieldMatrixHelpers.LeftNeighboringCellIsFree(i, j, fieldMatrix))
                {
                    adjacencyMatrix[GetPointNumber(j, i, fieldWidth), GetPointNumber(j - 1, i, fieldWidth)] = 1;
                }

                if (FieldMatrixHelpers.RightNeighboringCellIsFree(i, j, fieldMatrix, fieldWidth))
                {
                    adjacencyMatrix[GetPointNumber(j, i, fieldWidth), GetPointNumber(j + 1, i, fieldWidth)] = 1;
                }
            }
        }

        return adjacencyMatrix;
    }

    public static int DistanceBetweenPoints(int firstPointNumber, int secondPointNumber, int width)
    {
        var (firstX, firstY) = GetPointCoordinates(firstPointNumber, width);
        var (secondX, secondY) = GetPointCoordinates(secondPointNumber, width);
        return firstX == secondX || firstY == secondY ? 10 : 14;
    }
}