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

    private static double ObjectiveFunction(IList<double> point)
        => -5 * point[0] * Math.Pow(point[1], 2) * point[2]
           + 2 * Math.Pow(point[0], 2) * point[1]
           - 3 * point[0] * Math.Pow(point[1], 4)
           + point[0] * Math.Pow(point[2], 2);

    public static void Run(Vector<double> startingPoint, double distanceBetweenTwoPoints, double precision, int iterationsNumber)
    {
        var simplex = Matrix<double>.Build.Dense(N + 1, N);
        simplex.SetRow(0, startingPoint);
        simplex.MapIndexedInplace(
            (i, j, value) => simplex[0, j] + 
                             (j == i - 1 
                                 ? D1(distanceBetweenTwoPoints) 
                                 : D2(distanceBetweenTwoPoints)), 
            Zeros.Include);
        for (var i = 0; i < iterationsNumber; i++)
        {
            var functionValues =
                simplex.EnumerateRows().Select(ObjectiveFunction)
                    .ToList();

            var maxFunctionsValue = functionValues.Max();
            Console.WriteLine(maxFunctionsValue);
            var minFunctionValue = functionValues.Min();
            var indexOfMax = functionValues.IndexOf(maxFunctionsValue);
            var indexOfMin = functionValues.IndexOf(minFunctionValue);
            var centerOfGravity = (simplex.ReduceRows((row1, row2) => row1 + row2) - simplex.Row(indexOfMax)) / N;

            if (Math.Sqrt(functionValues.Sum() / (N + 1) - ObjectiveFunction(centerOfGravity)) <=
                precision)
            {
                break;
            }

            var reflectedPoint = centerOfGravity + Alpha * (centerOfGravity - simplex.Row(indexOfMax));
            if (ObjectiveFunction(reflectedPoint) <= minFunctionValue)
            {
                var expandedPoint = centerOfGravity + (GammaUpperBound - GammaLowerBound) / 2.0 * (reflectedPoint - centerOfGravity);
                simplex.SetRow(indexOfMax,
                    ObjectiveFunction(expandedPoint) <= minFunctionValue ? expandedPoint : reflectedPoint);
                continue;
            }

            if (ObjectiveFunction(reflectedPoint) <= maxFunctionsValue)
            {
                var contractedPoint =
                    centerOfGravity + (BetaUpperBound - BetaLowerBound) / 2 * (simplex.Row(indexOfMax) - centerOfGravity);
                simplex.SetRow(indexOfMax, contractedPoint);
                continue;
            }

            var minRow = simplex.Row(indexOfMin);
            simplex.SetSubMatrix(0, 0,Matrix<double>.Build.DenseOfRows(simplex.EnumerateRows().Select(row => minRow + 0.5 * (row - minRow))));
        }
    }

    private static double D1(double distanceBetweenTwoPoints)
        => distanceBetweenTwoPoints / (N * Math.Sqrt(2)) * (Math.Sqrt(N + 1) + N - 1);

    private static double D2(double distanceBetweenTwoPoints)
        => distanceBetweenTwoPoints / (N * Math.Sqrt(2)) * (Math.Sqrt(N + 1) - 1);
}