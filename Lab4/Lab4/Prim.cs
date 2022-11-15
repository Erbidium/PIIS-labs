namespace Lab4;

public static class Prim
{
    public static int[] Run(int[,] adjacencyMatrix)
    {
        var verticesNumber = adjacencyMatrix.GetLength(0);
        var parent = Enumerable.Repeat(-1, verticesNumber).ToArray();
        var distances = new PriorityQueue<int, int>();
        distances.Enqueue(0, 0);
        var nodes = new HashSet<int>(Enumerable.Range(0, verticesNumber).ToList());

        while (distances.Count > 0)
        {
            var current = distances.Dequeue();
            nodes.Remove(current);
            for (var i = 0; i < verticesNumber; i++)
            {
                if (adjacencyMatrix[current, i] != 0 && nodes.Contains(i))
                {
                    distances.Enqueue(i, adjacencyMatrix[current, i]);
                    parent[i] = current;
                }
            }
        }

        return parent;
    }
}