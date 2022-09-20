using PathFindingLab1.BLL.Services;

namespace PathfindingLab1.ConsoleApp;

public static class ConsolePrinter
{
    public static void PrintAlgorithmResults(int[] from, int start, int end, int[,] fieldMatrix)
    {
        var pathPoints = new HashSet<int>();
        var current = end;
        while (current != start)
        {
            pathPoints.Add(current);
            current = from[current];
        }

        for (var i = 0; i < fieldMatrix.GetLength(0); i++)
        {
            for (var j = 0; j < fieldMatrix.GetLength(1); j++)
            {
                var point = FieldService.GetPointNumber(j, i, fieldMatrix.GetLength(1));
                Console.Write(pathPoints.Contains(point) ? "x " : $"{fieldMatrix[i, j]} ");
            }
            Console.WriteLine();
        }
    }
}