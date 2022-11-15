namespace Lab4;

public static class Djkstra
{
    public static int[] GetShortestWays(int[,] adjacencyMatrix, int start)
    {
        var verticesNumber = adjacencyMatrix.GetLength(0);
        var open = new PriorityQueue<int, int>();
        var from = Enumerable.Repeat(-1, verticesNumber).ToArray();
        var distances = Enumerable.Repeat(int.MaxValue, verticesNumber).ToArray();
        var isOpen = Enumerable.Repeat(true, verticesNumber).ToArray();
        distances[start] = 0;
        open.Enqueue(start, 0);
        while (open.Count != 0)
        {
            var current = open.Dequeue();
            if (isOpen[current] == false)
                continue;
            isOpen[current] = false;

            for (var i = 0; i < verticesNumber; i++)
            {
                if (adjacencyMatrix[current, i] != 0 && isOpen[i] && distances[current] + adjacencyMatrix[current, i] < distances[i])
                {
                    from[i] = current;
                    distances[i] = distances[current] + adjacencyMatrix[current, i];
                    open.Enqueue(i, distances[i]);
                }
            }
        }

        return distances;
    }
}