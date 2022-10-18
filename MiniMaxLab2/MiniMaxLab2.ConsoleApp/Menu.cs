using PathFindingLab1.BLL.Entities;
using PathFindingLab1.BLL.Services;

namespace PathfindingLab1.ConsoleApp;

public static class Menu
{
    public static void ShowMenu()
    {
        FileReader.ReadGameFieldWithStartData(out var matrix, out var playerPosition, out var finishPosition, out var enemyPosition);
        var minimaxService = new MinimaxService(matrix, finishPosition);
        var pathFindingService = new PathFindingService();
        while (true)
        {
            var nextPlayerPosition = minimaxService.MinimaxWithAlphaBetaPruning(new Position
            {
                PlayerPosition = playerPosition,
                EnemyPosition = enemyPosition
            }, 10, int.MinValue, int.MaxValue, true);
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
            var nextEnemyPosition = pathFindingService.LeeAlgorithm(
                FieldService.GetAdjacencyMatrix(matrix), 
                matrix, 
                FieldService.GetPointNumber(enemyPosition.Item1, enemyPosition.Item2, matrix.GetLength(1)),
                FieldService.GetPointNumber(playerPosition.Item1, playerPosition.Item2, matrix.GetLength(1))
                );
            var pointNumber = Array.IndexOf(nextEnemyPosition.Item1, FieldService.GetPointNumber(enemyPosition.Item1, enemyPosition.Item2, matrix.GetLength(1)));
            enemyPosition = FieldService.GetPointCoordinates(pointNumber, matrix.GetLength(1));
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