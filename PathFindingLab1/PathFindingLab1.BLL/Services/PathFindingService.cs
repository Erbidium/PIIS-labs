namespace PathFindingLab1.BLL.Services;

public class PathFindingService
{
    private int h(int currentPointNumber, int endPointNumber, int width, int height)
    {
        var currentX = currentPointNumber % width;
        var currentY = currentPointNumber / width;
        var endX = currentPointNumber % width;
        var endY = endPointNumber / width;
        return Math.Abs(currentX - endX) + Math.Abs(currentY - endY);
    }
    
    private int DistanceBetweenPoints(int firstPointNumber, int secondPointNumber, int width, int height)
    {
        var firstX = firstPointNumber % width;
        var firstY = firstPointNumber / width;
        var secondX = secondPointNumber % width;
        var secondY = secondPointNumber / width;
        return firstX == firstY || secondX == secondY ? 10 : 14;
    }
    
    public int[] AStarAlgorithm(int[,] adjacencyMatrix, int[,] fieldMatrix, int start, int end)
    {
        var numberOfPoints = adjacencyMatrix.GetLength(0);
        var open = new PriorityQueue<int, int>();
        var from = Enumerable.Repeat(-1, numberOfPoints).ToArray();
        var g = Enumerable.Repeat(int.MaxValue, numberOfPoints).ToArray();
        var f = Enumerable.Repeat(int.MaxValue, numberOfPoints).ToArray();
        var isOpen = Enumerable.Repeat(true, numberOfPoints).ToArray();
        g[start] = 0;
        f[start] = g[start] + h(start, end, fieldMatrix.GetLength(1), fieldMatrix.GetLength(0));
        open.Enqueue(start, -f[start]);
        while (open.Count != 0)
        {
            var current = open.Dequeue();
            if (isOpen[current] == false)
                continue;
            isOpen[current] = false;
            if (current == end)
            {
                return from;
            }

            for (var i = 0; i < numberOfPoints; i++)
            {
                if (adjacencyMatrix[current, i] != 0 && isOpen[i] && g[current] + DistanceBetweenPoints(current, i, fieldMatrix.GetLength(1), fieldMatrix.GetLength(0)) < g[i])
                {
                    from[i] = current;
                    g[i] = g[current] + DistanceBetweenPoints(current, i, fieldMatrix.GetLength(1), fieldMatrix.GetLength(0));
                    f[i] = g[i] + h(i, end, fieldMatrix.GetLength(1), fieldMatrix.GetLength(0));
                    open.Enqueue(i, -f[i]);
                }
            }
        }

        return Array.Empty<int>();
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