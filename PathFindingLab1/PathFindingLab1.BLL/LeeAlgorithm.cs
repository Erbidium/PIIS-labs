namespace PathFindingLab1.BLL;

public class LeeAlgorithm
{
    public static int DistanceBetweenPoints(int firstPointNumber, int secondPointNumber, int width, int height)
    {
        var firstX = firstPointNumber % width;
        var firstY = firstPointNumber / width;
        var secondX = secondPointNumber % width;
        var secondY = secondPointNumber / width;
        return firstX == firstY || secondX == secondY ? 10 : 14;
    }

    public int[] GetPath(int[,] adjacencyMatrix, int[,] fieldMatrix, int start, int end)
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