using PathFindingLab1.BLL.Services;

namespace PathfindingLab1.ConsoleApp;

public static class ConsolePrinter
{
    public static void RenderGameFrame(int[,] matrix, (int, int) playerPosition, (int, int) enemyPosition,
        (int, int) finishPosition)
    {
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                if (i == playerPosition.Item2 && j == playerPosition.Item1 &&
                    i == enemyPosition.Item2 && j == enemyPosition.Item1)
                {
                    Console.Write("X");
                }
                else if (i == playerPosition.Item2 && j == playerPosition.Item1)
                {
                    Console.Write("P");
                }
                else if (i == enemyPosition.Item2 && j == enemyPosition.Item1)
                {
                    Console.Write("E");
                }
                else if (i == finishPosition.Item2 && j == finishPosition.Item1)
                {
                    Console.Write("F");
                }
                else
                {
                    Console.Write($"{matrix[i, j]}");
                }
            }

            Console.WriteLine();
        }
    }
}