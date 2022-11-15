namespace Lab4;

public static class Menu
{
    public static void RunKarpRabinAlgorithm()
    {
        Console.WriteLine("Please, enter row of text: ");
        var text = Console.ReadLine();
        Console.WriteLine("Please, enter text to find: ");
        var textToFind = Console.ReadLine();

        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(textToFind))
        {
            Console.WriteLine("Wrong entered input text");
            return;
        }
        var indexes = KarpRabin.GetIndexesOfFoundSubstring(text, textToFind);
        foreach (var index in indexes)
        {
            Console.WriteLine($"index: {index} value: {text.Substring(index, textToFind.Length)}");
        }
    }

    public static void RunDjkstraAlgorithm()
    {
        var path = @"C:\Users\Acer\Documents\PIIS-labs\lab4Djkstra.txt";
        var adjacencyMatrix = FileReader.ReadAdjacencyMatrix(path);
        var startVertex = 2;
        var shortestWays = Djkstra.GetShortestWays(adjacencyMatrix, startVertex);
        for(int i = 0; i < shortestWays.Length; i++)
        {
            Console.WriteLine($"{shortestWays[i]} from vertex {startVertex} to vertex {i}");
        }
        Console.WriteLine();
    }

    public static void RunPrimAlgorithm()
    {
        var path = @"C:\Users\Acer\Documents\PIIS-labs\lab4Prim.txt";
        var adjacencyMatrix = FileReader.ReadAdjacencyMatrix(path);
        var mst = Prim.Run(adjacencyMatrix);
        var totalWeight = 0;
        for (var i = 0; i < mst.Length; i++)
        {
            if (mst[i] >= 0)
            {
                Console.WriteLine($"Edge: {mst[i] + 1} - {i + 1}\t Weight: {adjacencyMatrix[mst[i], i]}");
                totalWeight += adjacencyMatrix[mst[i], i];
            }
        }
        Console.WriteLine($"Total MST weight: {totalWeight}");
    }
}