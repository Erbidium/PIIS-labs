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

        for (var i = 0; i < fieldMatrix.GetLength(1) + 1; i++)
        {
            Console.Write("--");
        }
        Console.WriteLine();
        for (var i = 0; i < fieldMatrix.GetLength(0); i++)
        {
            Console.Write("|");
            for (var j = 0; j < fieldMatrix.GetLength(1); j++)
            {
                var point = FieldService.GetPointNumber(j, i, fieldMatrix.GetLength(1));
                if (point == start)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("S ");
                    Console.ResetColor();
                }
                else if (point == end)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("E ");
                    Console.ResetColor();
                }
                else if (pathPoints.Contains(point))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("x ");
                    Console.ResetColor();
                }
                else if (fieldMatrix[i, j] == 0)
                {
                    Console.Write("  ");
                }
                else if (fieldMatrix[i, j] == 1)
                {
                    Console.Write("█ ");
                }
            }
            Console.Write("|");
            Console.WriteLine();
        }
        for (var i = 0; i < fieldMatrix.GetLength(1) + 1; i++)
        {
            Console.Write("--");
        }
        Console.WriteLine();
    }
}