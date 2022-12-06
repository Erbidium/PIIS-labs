namespace SimplexMethod;

public static class ConsolePrinter
{
    public static void PrintMatrix(double[,] simplexTable)
    {
        for (int j = 0; j < simplexTable.GetLength(1) - 1; j++)
        {
            Console.Write($"X{j + 1}".PadLeft(7));
        }
        Console.Write($"F".PadLeft(7));
        Console.WriteLine();
        for (int i = 0; i < simplexTable.GetLength(0); i++)
        {
            for (int j = 0; j < simplexTable.GetLength(1); j++)
            {
                Console.Write($"{Math.Round(simplexTable[i, j], 3) }".PadLeft(7));
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}