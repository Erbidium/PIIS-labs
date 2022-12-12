using MathNet.Numerics.LinearAlgebra;

namespace NelderMead;

public static class NelderMeadMethod
{
    private const int Alpha = 1;
    private const int GammaLowerBound = 2;
    private const int GammaUpperBound = 3;
    private const double BetaLowerBound = 0.4;
    private const double BetaUpperBound = 0.6;
    private const int N = 3;

    public static double ObjectiveFunction(IList<double> point)
        => -5 * point[0] * Math.Pow(point[1], 2) * point[2]
           + 2 * Math.Pow(point[0], 2) * point[1]
           - 3 * point[0] * Math.Pow(point[1], 4)
           + point[0] * Math.Pow(point[2], 2);

    public static void Run(Vector<double> startingPoint, double distanceBetweenTwoPoints, double precision, int iterationsNumber)
    {
        var simplex = Matrix<double>.Build.Dense(N + 1, N);
        simplex.SetRow(0, startingPoint);
        simplex.MapIndexedInplace(
            (i, j, value) => i == 0 
                ? value 
                : simplex[0, j] + (j == i - 1 
                    ? D1(distanceBetweenTwoPoints) 
                    : D2(distanceBetweenTwoPoints)), 
            Zeros.Include);
        for (var i = 0; i < iterationsNumber; i++)
        {
            if (!ConsolePrinter.PrintAlgorithmResult(simplex, i))
            {
                return;
            }
            var functionValues =
                simplex.EnumerateRows()
                    .Select(ObjectiveFunction).ToList();
                var ordererFunctionValues = functionValues.OrderBy(f => f).ToList();

            var worstFunctionValue = ordererFunctionValues[^1];
            var secondWorstFunctionValue = ordererFunctionValues[^2];
            var bestFunctionValue = functionValues[0];
            var indexOfWorst = functionValues.IndexOf(worstFunctionValue);
            var indexOfBest = functionValues.IndexOf(bestFunctionValue);

            var centerOfGravity = (simplex.ReduceRows((row1, row2) => row1 + row2) - simplex.Row(indexOfWorst)) / N;

            if (Math.Sqrt(functionValues
                    .Select(functionValue => Math.Pow(functionValue - ObjectiveFunction(centerOfGravity), 2))
                    .Sum() / (N + 1) ) <= precision) 
            {
                break;
            }

            var reflectedPoint = centerOfGravity + Alpha * (centerOfGravity - simplex.Row(indexOfWorst));
            if (ObjectiveFunction(reflectedPoint) <= secondWorstFunctionValue &&
                ObjectiveFunction(reflectedPoint) >= bestFunctionValue)
            {
                simplex.SetRow(indexOfWorst, reflectedPoint);
                continue;
            }
            if (ObjectiveFunction(reflectedPoint) <= bestFunctionValue)
            {
                var expandedPoint = centerOfGravity + (GammaUpperBound + GammaLowerBound) / 2.0 * (reflectedPoint - centerOfGravity);
                simplex.SetRow(indexOfWorst,
                    ObjectiveFunction(expandedPoint) <= bestFunctionValue ? expandedPoint : reflectedPoint);
                continue;
            }

            if (ObjectiveFunction(reflectedPoint) <= worstFunctionValue)
            {
                var contractedPoint =
                    centerOfGravity + (BetaUpperBound - BetaLowerBound) / 2 * (reflectedPoint - centerOfGravity);
                if (ObjectiveFunction(contractedPoint) <= ObjectiveFunction(reflectedPoint))
                {
                    simplex.SetRow(indexOfWorst, contractedPoint);
                    continue;
                }
            }
            else
            {
                var contractedPoint =
                    centerOfGravity + (BetaUpperBound - BetaLowerBound) / 2 * (simplex.Row(indexOfWorst) - centerOfGravity);
                if (ObjectiveFunction(contractedPoint) <= worstFunctionValue)
                {
                    simplex.SetRow(indexOfWorst, contractedPoint);
                    continue;
                }
            }
            var bestRow = simplex.Row(indexOfBest);
            for (var j = 0; j < simplex.RowCount; j++)
            {
                if (j == indexOfBest) continue;

                var currentRow = simplex.Row(j);
                simplex.SetRow(j, bestRow + 0.5 * (currentRow - bestRow));
            }
        }
    }

    private static double D1(double distanceBetweenTwoPoints)
        => distanceBetweenTwoPoints / (N * Math.Sqrt(2)) * (Math.Sqrt(N + 1) + N - 1);

    private static double D2(double distanceBetweenTwoPoints)
        => distanceBetweenTwoPoints / (N * Math.Sqrt(2)) * (Math.Sqrt(N + 1) - 1);
}