namespace PathFindingLab1.BLL.Services;

public class PathFindingService
{
    private readonly int[,] _adjacencyMatrix;
    private readonly int[,] _fieldMatrix;
    private readonly List<int> _from = new();
    private readonly List<int> _distances = new();
    private readonly List<int> _f = new();
    public PathFindingService(int[,] adjacencyMatrix, int[,] fieldMatrix)
    {
        _adjacencyMatrix = adjacencyMatrix;
        _fieldMatrix = fieldMatrix;
    }
    private static int HeuristicValue(int currentPointNumber, int endPointNumber, int width)
    {
        var (currentX, currentY) = FieldService.GetPointCoordinates(currentPointNumber, width);
        var (endX, endY) = FieldService.GetPointCoordinates(endPointNumber, width);
        return Math.Abs(currentX - endX) + Math.Abs(currentY - endY);
    }

    public (int[], int) AStarAlgorithm(int start, int end)
    {
        var numberOfPoints = _adjacencyMatrix.GetLength(0);
        var open = new PriorityQueue<int, int>();
        _from.Clear();
        _distances.Clear();
        _f.Clear();
        _from.AddRange(Enumerable.Repeat(-1, numberOfPoints));
        _distances.AddRange(Enumerable.Repeat(int.MaxValue, numberOfPoints));
        _f.AddRange(Enumerable.Repeat(int.MaxValue, numberOfPoints));
        var isOpen = Enumerable.Repeat(true, numberOfPoints).ToArray();
        _distances[start] = 0;
        _f[start] = _distances[start] + HeuristicValue(start, end, _fieldMatrix.GetLength(1));
        open.Enqueue(start, _f[start]);
        while (open.Count != 0)
        {
            var current = open.Dequeue();
            if (isOpen[current] == false)
                continue;
            isOpen[current] = false;
            if (current == end)
            {
                return (_from.ToArray(), _distances[end]);
            }

            for (var i = 0; i < numberOfPoints; i++)
            {
                if (_adjacencyMatrix[current, i] != 0 && isOpen[i] && _distances[current] +
                    FieldService.DistanceBetweenPoints(current, i, _fieldMatrix.GetLength(1)) < _distances[i])
                {
                    _from[i] = current;
                    _distances[i] = _distances[current] +
                                   FieldService.DistanceBetweenPoints(current, i, _fieldMatrix.GetLength(1));
                    _f[i] = _distances[i] + HeuristicValue(i, end, _fieldMatrix.GetLength(1));
                    open.Enqueue(i, _f[i]);
                }
            }
        }

        return (Array.Empty<int>(), 0);
    }

    public (int[], int) LeeAlgorithm(int start, int end)
    {
        var numberOfPoints = _adjacencyMatrix.GetLength(0);
        var queue = new Queue<int>();
        var from = Enumerable.Repeat(-1, numberOfPoints).ToArray();
        var visited = Enumerable.Repeat(false, numberOfPoints).ToArray();
        var distances = Enumerable.Repeat(int.MaxValue, numberOfPoints).ToArray();

        visited[start] = true;
        distances[start] = 0;
        queue.Enqueue(start);
        while (queue.Count != 0)
        {
            var current = queue.Dequeue();
            if (current == end)
            {
                return (from, distances[end]);
            }

            for (var i = 0; i < numberOfPoints; i++)
            {
                if (_adjacencyMatrix[current, i] != 0 && !visited[i])
                {
                    queue.Enqueue(i);
                    from[i] = current;
                    distances[i] = distances[current] +
                                   FieldService.DistanceBetweenPoints(current, i, _fieldMatrix.GetLength(1));
                    visited[i] = true;
                }
            }
        }

        return (Array.Empty<int>(), 0);
    }
}