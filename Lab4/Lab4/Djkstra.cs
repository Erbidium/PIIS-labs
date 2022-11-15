namespace Lab4;

public static class Djkstra
{
    public static int[] GetShortestWays(int[,] adjacencyMatrix, int start)
    {
        var numberOfPoints = adjacencyMatrix.GetLength(0);
        var open = new Queue<int>();
        var from = Enumerable.Repeat(-1, numberOfPoints).ToArray();
        var distances = Enumerable.Repeat(int.MaxValue, numberOfPoints).ToArray();
        var isOpen = Enumerable.Repeat(true, numberOfPoints).ToArray();
        distances[start] = 0;
        open.Enqueue(start);
        while (open.Count != 0)
        {
            var current = open.Dequeue();
            if (isOpen[current] == false)
                continue;
            isOpen[current] = false;

            for (var i = 0; i < numberOfPoints; i++)
            {
                if (adjacencyMatrix[current, i] != 0 && isOpen[i] && distances[current] + adjacencyMatrix[current, i] < distances[i])
                {
                    from[i] = current;
                    distances[i] = distances[current] + adjacencyMatrix[current, i];
                    open.Enqueue(i);
                }
            }
        }

        return distances;
    }
}