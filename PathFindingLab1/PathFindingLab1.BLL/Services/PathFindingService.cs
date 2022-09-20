namespace PathFindingLab1.BLL.Services;

public class PathFindingService
{
    private static int HeuristicValue(int currentPointNumber, int endPointNumber, int width)
    {
        var (currentX, currentY) = FieldService.GetPointCoordinates(currentPointNumber, width);
        var (endX, endY) = FieldService.GetPointCoordinates(endPointNumber, width);
        return Math.Abs(currentX - endX) + Math.Abs(currentY - endY);
    }
    
    private int DistanceBetweenPoints(int firstPointNumber, int secondPointNumber, int width)
    {
        var firstX = firstPointNumber % width;
        var firstY = firstPointNumber / width;
        var secondX = secondPointNumber % width;
        var secondY = secondPointNumber / width;
        return firstX == firstY || secondX == secondY ? 10 : 14;
    }
    
    public (int[], int) AStarAlgorithm(int[,] adjacencyMatrix, int[,] fieldMatrix, int start, int end)
    {
        var numberOfPoints = adjacencyMatrix.GetLength(0);
        var open = new PriorityQueue<int, int>();
        var from = Enumerable.Repeat(-1, numberOfPoints).ToArray();
        var g = Enumerable.Repeat(int.MaxValue, numberOfPoints).ToArray();
        var f = Enumerable.Repeat(int.MaxValue, numberOfPoints).ToArray();
        var isOpen = Enumerable.Repeat(true, numberOfPoints).ToArray();
        g[start] = 0;
        f[start] = g[start] + HeuristicValue(start, end, fieldMatrix.GetLength(1));
        open.Enqueue(start, -f[start]);
        while (open.Count != 0)
        {
            var current = open.Dequeue();
            if (isOpen[current] == false)
                continue;
            isOpen[current] = false;
            if (current == end)
            {
                return (from, g[end]);
            }

            for (var i = 0; i < numberOfPoints; i++)
            {
                if (adjacencyMatrix[current, i] != 0 && isOpen[i] && g[current] + DistanceBetweenPoints(current, i, fieldMatrix.GetLength(1)) < g[i])
                {
                    from[i] = current;
                    g[i] = g[current] + DistanceBetweenPoints(current, i, fieldMatrix.GetLength(1));
                    f[i] = g[i] + HeuristicValue(i, end, fieldMatrix.GetLength(1));
                    open.Enqueue(i, -f[i]);
                }
            }
        }

        return (Array.Empty<int>(), 0);
    }
    
    public int[] LeeAlgorithm(int[,] adjacencyMatrix, int[,] fieldMatrix, int start, int end)
    {
        var numberOfPoints = adjacencyMatrix.GetLength(0);
        var queue = new Queue<int>();
        var from = Enumerable.Repeat(-1, numberOfPoints).ToArray();
        var visited = Enumerable.Repeat(false, numberOfPoints).ToArray();
        var distances = Enumerable.Repeat(int.MaxValue, numberOfPoints).ToArray();

        visited[start] = true;
        queue.Enqueue(start);
        while (queue.Count != 0)
        {
            var current = queue.Dequeue();
            if (current == end)
            {
                return from;
            }
            for (var i = 0; i < numberOfPoints; i++)
            {
                if (adjacencyMatrix[current, i] != 0 && !visited[i])
                {
                    queue.Enqueue(i);
                    from[i] = current;
                    visited[i] = true;
                }
            }
        }

        return Array.Empty<int>();
    }
}