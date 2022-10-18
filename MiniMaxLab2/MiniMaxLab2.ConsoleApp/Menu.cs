using PathFindingLab1.BLL.Entities;
using PathFindingLab1.BLL.Services;

namespace PathfindingLab1.ConsoleApp;

public static class Menu
{
    public static void ShowMenu()
    {
        Console.WriteLine("Please, choose algorithm: ");
        Console.WriteLine("Minimax(0)");
        Console.WriteLine("Minimax with alpha beta pruning(1)");
        var algorithmChoice = int.Parse(Console.ReadLine() ?? "0");
        FileReader.ReadGameFieldWithStartData(out var matrix, out var playerPosition, out var finishPosition,
            out var enemyPosition);
        var minimaxService = new MinimaxService(matrix, finishPosition);
        var pathFindingService = new PathFindingService();
        while (true)
        {
            var nextPlayerPosition = algorithmChoice == 0
                ? minimaxService.Minimax(new Position
                {
                    PlayerPosition = playerPosition,
                    EnemyPosition = enemyPosition
                }, 15, true)
                : minimaxService.MinimaxWithAlphaBetaPruning(new Position
                {
                    PlayerPosition = playerPosition,
                    EnemyPosition = enemyPosition
                }, 15, int.MinValue, int.MaxValue, true);
            playerPosition = nextPlayerPosition.Item2.PlayerPosition;
            Console.Clear();
            ConsolePrinter.RenderGameFrame(matrix, playerPosition, enemyPosition, finishPosition);
            if (playerPosition.Item1 == finishPosition.Item1 && playerPosition.Item2 == finishPosition.Item2)
            {
                Console.WriteLine("Player win");
                break;
            }

            if (playerPosition.Item1 == enemyPosition.Item1 && playerPosition.Item2 == enemyPosition.Item2)
            {
                Console.WriteLine("Player died");
                break;
            }

            var nextEnemyPosition = pathFindingService.AStarAlgorithm(
                FieldService.GetAdjacencyMatrix(matrix),
                matrix,
                FieldService.GetPointNumber(enemyPosition.Item1, enemyPosition.Item2, matrix.GetLength(1)),
                FieldService.GetPointNumber(playerPosition.Item1, playerPosition.Item2, matrix.GetLength(1))
            );
            var current = FieldService.GetPointNumber(playerPosition.Item1, playerPosition.Item2, matrix.GetLength(1));
            var start = FieldService.GetPointNumber(enemyPosition.Item1, enemyPosition.Item2, matrix.GetLength(1));
            while (current != start)
            {
                if (start == nextEnemyPosition.Item1[current])
                {
                    enemyPosition = FieldService.GetPointCoordinates(current, matrix.GetLength(1));
                }

                current = nextEnemyPosition.Item1[current];
            }

            Console.Clear();
            ConsolePrinter.RenderGameFrame(matrix, playerPosition, enemyPosition, finishPosition);
            if (playerPosition.Item1 == enemyPosition.Item1 && playerPosition.Item2 == enemyPosition.Item2)
            {
                Console.WriteLine("Player died");
                break;
            }

            Thread.Sleep(3000);
        }
    }
}