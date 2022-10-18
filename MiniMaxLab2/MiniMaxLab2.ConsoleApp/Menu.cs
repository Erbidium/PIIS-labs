using PathFindingLab1.BLL.Entities;
using PathFindingLab1.BLL.Services;
using PathfindingLab1.ConsoleApp.Exceptions;

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
            var nextPlayerPosition = minimaxService.Minimax(new Position
            {
                PlayerPosition = playerPosition,
                EnemyPosition = enemyPosition
            }, 10, int.MinValue, int.MaxValue, true);
            playerPosition = nextPlayerPosition.Item2.PlayerPosition;
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
            var pointNumber = Array.IndexOf(nextEnemyPosition.Item1, FieldService.GetPointNumber(enemyPosition.Item1, enemyPosition.Item2, matrix.GetLength(1)));
            enemyPosition = FieldService.GetPointCoordinates(pointNumber, matrix.GetLength(1));
            if (playerPosition.Item1 == enemyPosition.Item1 && playerPosition.Item2 == enemyPosition.Item2)
            {
                Console.WriteLine("Player died");
                break;
            }
            Console.Clear();
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i == playerPosition.Item2 && j == playerPosition.Item1)
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
            Thread.Sleep(3000);
        }

    }
}