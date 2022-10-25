using PathFindingLab1.BLL.Entities;

namespace PathFindingLab1.BLL.Services;

public class NegamaxService
{
    private readonly int[,] _cells;
    private readonly (int, int) _finish;
    private readonly PathFindingService _pathFindingService;
    private readonly List<Position> _children = new();

    public NegamaxService(int[,] cells, (int, int) finish, PathFindingService pathFindingService)
    {
        _cells = cells;
        _finish = finish;
        _pathFindingService = pathFindingService;
    }

    public (float, Position) NegamaxWithAlphaBetaPruning(Position position, int depth, float alpha, float beta,
        int color)
    {
        var children = GenerateChildren(position, color);
        if (depth == 0 || children.Count == 0)
        {
            return (position.Evaluation * color, position);
        }

        var maxEval = float.MinValue;
        var best = children.First();
        foreach (var child in children)
        {
            var eval = NegamaxWithAlphaBetaPruning(child, depth - 1, -beta, -alpha, -color);
            eval.Item1 *= -1;
            if (eval.Item1 > maxEval)
            {
                maxEval = eval.Item1;
                best = child;
            }

            alpha = Math.Max(alpha, eval.Item1);
            if (alpha >= beta)
            {
                break;
            }
        }

        return (maxEval, best);
    }

    public (float, Position) Negamax(Position position, int depth, int color)
    {
        var children = GenerateChildren(position, color);
        if (depth == 0 || children.Count == 0)
        {
            return (position.Evaluation * color, position);
        }

        var maxEval = float.MinValue;
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
        if (position.PlayerPosition == position.EnemyPosition || position.PlayerPosition == _finish)
        {
            return new List<Position>();
        }
        _children.Clear();
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
            _children.Add(new Position
            {
                PlayerPosition = color > 0 ? newPosition : position.PlayerPosition,
                EnemyPosition = color < 0 ? newPosition : position.EnemyPosition,
                Evaluation = EvaluationFunction(position, color)
            });
        }

        return (color > 0 ? _children.OrderByDescending(child => child.Evaluation) : _children.OrderBy(child => child.Evaluation)).ToList();
    }

    private float EvaluationFunction(Position position, int color)
    {
        var distanceToEnemy = _pathFindingService.AStarAlgorithm(
            FieldService.GetPointNumber(position.PlayerPosition.Item1, position.PlayerPosition.Item2, _cells.GetLength(1)),
            FieldService.GetPointNumber(position.EnemyPosition.Item1, position.EnemyPosition.Item2, _cells.GetLength(1))
        ).Item2;;
        if (color > 0)
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

            return distanceToEnemy * 1.5f - distanceToFinish;
        }

        return distanceToEnemy;
    }
}