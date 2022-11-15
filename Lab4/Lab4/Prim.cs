namespace Lab4;

public static class Prim
{
    public static int[] Run(int[,] adjacencyMatrix)
    {
        var verticesNumber = adjacencyMatrix.GetLength(0);
        var parent = Enumerable.Repeat(-1, verticesNumber).ToArray();
        var edges = new PriorityQueue<int, int>();
        edges.Enqueue(1, 0);
        var nodes = new HashSet<int>(Enumerable.Range(0, verticesNumber).ToList());

        while (edges.Count > 0)
        {
            var current = edges.Dequeue();
            nodes.Remove(current);
            for (var i = 0; i < verticesNumber; i++)
            {
                if (adjacencyMatrix[current, i] != 0 && nodes.Contains(i))
                {
                    edges.Enqueue(i, adjacencyMatrix[current, i]);
                    parent[i] = current;
                }
            }
        }

        return parent;
    }
}