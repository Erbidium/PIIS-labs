using MathNet.Numerics.LinearAlgebra;

namespace NelderMead;

public static class ConsolePrinter
{
    public static void PrintAlgorithmResult(Matrix<double> simplex, int iterationNumber)
    {
        Console.Write(string.Join("", Enumerable.Repeat("-", Console.WindowWidth)));
        Console.WriteLine($"Iterateion number: {iterationNumber}");
        Console.WriteLine("Simplex");
        foreach (var point in simplex.EnumerateRows())
        {
            Console.Write("(");
            foreach (var coordinate in point)
            {
                Console.Write($"{coordinate} ");
            }
            Console.WriteLine(")");
        }
        Console.WriteLine();
        
        var functionValues =
            simplex.EnumerateRows().Select(NelderMeadMethod.ObjectiveFunction)
                .ToList();

        var maxFunctionsValue = functionValues.Max();
        var minFunctionValue = functionValues.Min();
        var indexOfMax = functionValues.IndexOf(maxFunctionsValue);
        var indexOfMin = functionValues.IndexOf(minFunctionValue);
        Console.WriteLine($"Fmax = {maxFunctionsValue}");
        Console.WriteLine($"Point index: {indexOfMax}");
        Console.WriteLine();
        Console.WriteLine($"Fmin = {minFunctionValue}");
        Console.WriteLine($"Point index: {indexOfMin}");
        Console.WriteLine();
        Console.WriteLine();
    }
}