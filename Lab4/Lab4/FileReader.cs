namespace Lab4;

public static class FileReader
{
    public static int[,] ReadAdjacencyMatrix(string path)
    {
        var fileLines = File.ReadLines(path).ToList();
        var matrixHeight = fileLines.Count;
        var matrixWidth = fileLines[0].Length;
        var adjacencyMatrix = new int[matrixHeight, matrixWidth];
        for(var i = 0; i < fileLines.Count; i++)
        {
            for(var j = 0; j < fileLines[i].Length; j++)
            {
                adjacencyMatrix[i, j] = int.Parse(fileLines[i][j].ToString());
            }
        }

        return adjacencyMatrix;
    }
}