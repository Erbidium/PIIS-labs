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
        var matrixD = Matrix<double>.Build.Dense(N + 1, N);
        matrixD.SetRow(0, startingPoint);
        matrixD.MapIndexedInplace(
            (i, j, value) => matrixD[0, j] + 
                             (j == i - 1 
                                 ? D1(distanceBetweenTwoPoints) 
                                 : D2(distanceBetweenTwoPoints)), 
            Zeros.Include);

        for (var i = 0; i < iterationsNumber; i++)
        {
            var functionValues =
                matrixD.EnumerateRows().Select(ObjectiveFunction)
                    .ToList();

            var maxFunctionsValue = functionValues.Max();
            var minFunctionValue = functionValues.Min();
            var indexOfMax = functionValues.IndexOf(maxFunctionsValue);
            var indexOfMin = functionValues.IndexOf(minFunctionValue);
            var center = (matrixD.ReduceRows((row1, row2) => row1 + row2) - matrixD.Row(indexOfMax)) / N;

            if (Math.Sqrt(functionValues.Sum() / (N + 1) - ObjectiveFunction(center)) <=
                precision)
            {
                break;
            }

            var mappedPoint = center + Alpha * (center - matrixD.Row(indexOfMax));
            if (ObjectiveFunction(mappedPoint) <= minFunctionValue)
            {
                var stretchResult = center + (GammaUpperBound - GammaLowerBound) / 2.0 * (mappedPoint - center);
                matrixD.SetRow(indexOfMax,
                    ObjectiveFunction(stretchResult) <= minFunctionValue ? stretchResult : mappedPoint);
                continue;
            }

            if (ObjectiveFunction(mappedPoint) <= maxFunctionsValue)
            {
                var compressionResult =
                    center + (BetaUpperBound - BetaLowerBound) / 2 * (matrixD.Row(indexOfMax) - center);
                matrixD.SetRow(indexOfMax, compressionResult);
                continue;
            }

            var minRow = matrixD.Row(indexOfMin);
            matrixD.SetSubMatrix(0, 0,Matrix<double>.Build.DenseOfRows(matrixD.EnumerateRows().Select(row => minRow + 0.5 * (row - minRow))));
        }
    }

    private static double D1(double distanceBetweenTwoPoints)
        => distanceBetweenTwoPoints / (N * Math.Sqrt(2)) * (Math.Sqrt(N + 1) + N - 1);

    private static double D2(double distanceBetweenTwoPoints)
        => distanceBetweenTwoPoints / (N * Math.Sqrt(2)) * (Math.Sqrt(N + 1) - 1);
}