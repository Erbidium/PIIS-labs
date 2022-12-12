using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace NelderMead;

public static class ConsolePrinter
{
    public static bool PrintAlgorithmResult(Matrix<double> simplex, int iterationNumber)
    {
        Console.Write(string.Join("", Enumerable.Repeat("-", Console.WindowWidth)));
        Console.WriteLine($"Iteration number: {iterationNumber}");
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
        if (!PrintValueAndIndexOrInfinity(maxFunctionsValue, indexOfMax, "Fmax"))
        {
            return false;
        }

        if (!PrintValueAndIndexOrInfinity(minFunctionValue, indexOfMin, "Fmin"))
        {
            return false;
        }
        Console.WriteLine();
        Console.WriteLine();
        return true;
    }

    private static bool PrintValueAndIndexOrInfinity(double functionValue, double indexOfFunctionValue, string text)
    {
        if (functionValue.IsFinite())
        {
            Console.WriteLine($"{text} = {functionValue}");
            Console.WriteLine($"Point index: {indexOfFunctionValue}");
            Console.WriteLine();
            return true;
        }

        Console.WriteLine($"{text} = {(double.IsPositiveInfinity(functionValue) ? "+" : "-")}INF");
        Console.WriteLine();
        return false;
    }
}