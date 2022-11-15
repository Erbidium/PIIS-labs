namespace Lab4;

public static class Prim
{
    public static int[] Run(int[,] adjacencyMatrix)
    {
        var numberOfPoints = adjacencyMatrix.GetLength(0);
        var parent = Enumerable.Repeat(-1, numberOfPoints).ToArray();
        var distances = Enumerable.Repeat(int.MaxValue, numberOfPoints).ToArray();
        distances[0] = 0;
        var nodes = Enumerable.Range(0, numberOfPoints).ToList();

        while (nodes.Count > 0)
        {
            var current = nodes.MinBy(i => distances[i]);
            nodes.Remove(current);
            for (var i = 0; i < numberOfPoints; i++)
            {
                if (adjacencyMatrix[current, i] != 0 && nodes.Contains(i) &&
                   adjacencyMatrix[current, i] < distances[i])
                {
                    distances[i] = adjacencyMatrix[current, i];
                    parent[i] = current;
                }
            }
        }

        return parent;
    }
}