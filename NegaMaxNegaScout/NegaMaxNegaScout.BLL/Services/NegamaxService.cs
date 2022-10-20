using PathFindingLab1.BLL.Entities;

namespace PathFindingLab1.BLL.Services;

public class NegamaxService
{
    private readonly int[,] _cells;
    private readonly (int, int) _finish;
    private readonly PathFindingService _pathFindingService;

    public NegamaxService(int[,] cells, (int, int) finish, PathFindingService pathFindingService)
    {
        _cells = cells;
        _finish = finish;
        _pathFindingService = pathFindingService;
    }

    public (int, Position) NegamaxWithAlphaBetaPruning(Position position, int depth, int alpha, int beta,
        int color)
    {
        var children = GenerateChildren(position, color);
        if (depth == 0 || children.Count == 0)
        {
            return (position.Evaluation * color, position);
        }

        var maxEval = int.MinValue;
        var best = children.First();
        foreach (var child in children)
        {
            var eval = NegamaxWithAlphaBetaPruning(child, depth - 1, alpha, beta, -color);
            eval.Item1 *= -1;
            if (eval.Item1 > maxEval)
            {
                maxEval = eval.Item1;
                best = child;
            }

            alpha = Math.Max(alpha, eval.Item1);
            if (beta <= alpha)
            {
                break;
            }
        }

        return (maxEval, best);
    }

    public (int, Position) Negamax(Position position, int depth, int color)
    {
        var children = GenerateChildren(position, color);
        if (depth == 0 || children.Count == 0)
        {
            return (position.Evaluation * color, position);
        }

        var maxEval = int.MinValue;
        var best = children.First();
        foreach (var child in children)
        {
            var eval = Negamax(child, depth - 1, -color);
            eval.Item1 *= -1;
            if (eval.Item1 > maxEval)
            {
                maxEval = eval.Item1;
                best = child;
            }
        }

        return (maxEval, best);
    }

    private List<Position> GenerateChildren(Position position, int color)
    {
        var children = new List<Position>();
        var playerPosition = color > 0 ? position.PlayerPosition : position.EnemyPosition;
        var directions = new[]
        {
            (1, 0),
            (-1, 0),
            (0, 1),
            (0, -1)
        };
        foreach (var direction in directions)
        {
            if (playerPosition.Item1 + direction.Item1 >= _cells.GetLength(1) ||
                playerPosition.Item2 + direction.Item2 >= _cells.GetLength(0) ||
                playerPosition.Item1 + direction.Item1 < 0 ||
                playerPosition.Item2 + direction.Item2 < 0 ||
                _cells[playerPosition.Item2 + direction.Item2, playerPosition.Item1 + direction.Item1] != 0) continue;

            var newPosition = (playerPosition.Item1 + direction.Item1, playerPosition.Item2 + direction.Item2);
            children.Add(new Position
            {
                PlayerPosition = color > 0 ? newPosition : position.PlayerPosition,
                EnemyPosition = color < 0 ? newPosition : position.EnemyPosition,
                Evaluation = EvaluationFunction(position, color > 0)
            });
        }

        return children;
    }

    private int EvaluationFunction(Position position, bool isMaximizingPlayer)
    {
        var distanceToEnemy = Math.Abs(position.PlayerPosition.Item1 - position.EnemyPosition.Item1) +
                              Math.Abs(position.PlayerPosition.Item2 - position.EnemyPosition.Item2);
        if (isMaximizingPlayer)
        {
            var distanceToFinish = _pathFindingService.AStarAlgorithm(
                FieldService.GetPointNumber(position.PlayerPosition.Item1, position.PlayerPosition.Item2, _cells.GetLength(1)),
                FieldService.GetPointNumber(_finish.Item1, _finish.Item2, _cells.GetLength(1))
            ).Item2;
            if (distanceToEnemy <= 1)
            {
                return int.MinValue;
            }

            if (distanceToFinish <= 1)
            {
                return int.MaxValue;
            }

            return distanceToEnemy * 2 - distanceToFinish;
        }

        return distanceToEnemy;
    }
}