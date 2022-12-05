namespace SimplexMethod;

public static class FileReader
{
    public static double[,] ReadSimplexTable(string path)
    {
        var table = File.ReadLines(path);

        var lists = table.Select(row => row.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToList();
        var matrix = new double[lists.Count, lists[0].Length];
        for(var i = 0; i < matrix.GetLength(0); i++)
        {
            for(var j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] = double.Parse(lists[i][j]);
            }
        }

        return matrix;
    }
}