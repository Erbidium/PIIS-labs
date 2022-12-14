using PathFindingLab1.BLL.Entities;

namespace PathFindingLab1.BLL.Services;

public class MinimaxService
{
    private readonly int[,] _cells;
    private readonly (int, int) _finish;
    private readonly PathFindingService _pathFindingService;

    public MinimaxService(int[,] cells, (int, int) finish, PathFindingService pathFindingService)
    {
        _cells = cells;
        _finish = finish;
        _pathFindingService = pathFindingService;
    }

    public (int, Position) MinimaxWithAlphaBetaPruning(Position position, int depth, int alpha, int beta,
        bool maximizingPlayer)
    {
        var children = GenerateChildren(position, maximizingPlayer);
        if (depth == 0 || children.Count == 0)
        {
            return (position.Evaluation, position);
        }

        if (maximizingPlayer)
        {
            var maxEval = int.MinValue;
            var best = children.First();
            for (var i = children.Count - 1; i >= 0; i--)
            {
                var eval = MinimaxWithAlphaBetaPruning(children[i], depth - 1, alpha, beta, false);
                if (eval.Item1 > maxEval)
                {
                    maxEval = eval.Item1;
                    best = children[i];
                }

                alpha = Math.Max(alpha, eval.Item1);
                if (beta <= alpha)
                {
                    break;
                }
            }

            return (maxEval, best);
        }
        else
        {
            var minEval = int.MaxValue;
            var best = children.First();
            foreach (var child in children)
            {
                var eval = MinimaxWithAlphaBetaPruning(child, depth - 1, alpha, beta, true);
                if (eval.Item1 < minEval)
                {
                    minEval = eval.Item1;
                    best = child;
                }

                beta = Math.Min(beta, eval.Item1);
                if (beta <= alpha)
                {
                    break;
                }
            }

            return (minEval, best);
        }
    }

    public (int, Position) Minimax(Position position, int depth, bool maximizingPlayer)
    {
        var children = GenerateChildren(position, maximizingPlayer);
        if (depth == 0 || children.Count == 0)
        {
            return (position.Evaluation, position);
        }

        if (maximizingPlayer)
        {
            var maxEval = int.MinValue;
            var best = children.First();
            for (var i = children.Count - 1; i >= 0; i--)
            {
                var eval = Minimax(children[i], depth - 1, false);
                if (eval.Item1 > maxEval)
                {
                    maxEval = eval.Item1;
                    best = children[i];
                }
            }

            return (maxEval, best);
        }
        else
        {
            var minEval = int.MaxValue;
            var best = children.First();
            foreach (var child in children)
            {
                var eval = Minimax(child, depth - 1, true);
                if (eval.Item1 < minEval)
                {
                    minEval = eval.Item1;
                    best = child;
                }
            }

            return (minEval, best);
        }
    }

    private List<Position> GenerateChildren(Position position, bool isMaximizingPlayer)
    {
        var children = new List<Position>();
        var playerPosition = isMaximizingPlayer ? position.PlayerPosition : position.EnemyPosition;
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
                PlayerPosition = isMaximizingPlayer ? newPosition : position.PlayerPosition,
                EnemyPosition = !isMaximizingPlayer ? newPosition : position.EnemyPosition,
                Evaluation = EvaluationFunction(position, isMaximizingPlayer)
            });
        }

        return children.OrderBy(child => child.Evaluation).ToList();
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