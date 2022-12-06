namespace SimplexMethod;

public static class ConsolePrinter
{
    public static void PrintMatrix(double[,] simplexTable)
    {
        for (var j = 0; j < simplexTable.GetLength(1) - 1; j++)
        {
            Console.Write($"X{j + 1}".PadLeft(7));
        }
        Console.Write("F".PadLeft(7));
        Console.WriteLine();
        for (var i = 0; i < simplexTable.GetLength(0); i++)
        {
            for (var j = 0; j < simplexTable.GetLength(1); j++)
            {
                Console.Write($"{Math.Round(simplexTable[i, j], 3) }".PadLeft(7));
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public static void PrintAlgorithmResults(double[,] simplexTable)
    {
        Console.WriteLine($"Function value: {simplexTable[0, simplexTable.GetLength(1) - 1]}");
        var basisVariables = Enumerable.Range(1, simplexTable.GetLength(0) - 1)
            .Select(row => (variable: simplexTable.GetBasisVariableNumberByRow(row),
                value: simplexTable[row, simplexTable.GetLength(1) - 1]))
            .Where(tuple => tuple.variable >= 0)
            .ToList();
        var freeVariables = Enumerable.Range(0, simplexTable.GetLength(1) - 1)
            .Except(basisVariables.Select(tuple => tuple.variable).ToList())
            .ToList();
        foreach (var basisVariable in basisVariables)
        {
            Console.WriteLine($"X{basisVariable.variable + 1} = {basisVariable.value}");
        }
        foreach (var freeVariable in freeVariables)
        {
            Console.WriteLine($"X{freeVariable + 1} = 0");
        }
    }
}