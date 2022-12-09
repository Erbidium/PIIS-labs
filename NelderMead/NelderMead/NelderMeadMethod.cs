namespace NelderMead;

public static class NelderMeadMethod
{
    private const int Alpha = 1;
    private const int GammaLowerBound = 2;
    private const int GammaUpperBound = 3;
    private const double BetaLowerBound = 0.4;
    private const double BetaUpperBound = 0.6;
    private const int N = 3;

    public static int ObjectiveFunction((int x1, int x2, int x3) point)
        => -5 * point.x1 * (int)Math.Pow(point.x2, 2) * point.x3
           + 2 * (int)Math.Pow(point.x1, 2) * point.x2
           - 3 * point.x1 * (int)Math.Pow(point.x2, 4)
           + point.x1 * (int)Math.Pow(point.x3, 2);

    public static void Run((double x1, double x2, double x3) startingPoint, double distanceBetweenTwoPoints, double precision, int iterationsNumber)
    {
        var matrixD = new double[N + 1][];
        matrixD[0] = new [] { startingPoint.x1, startingPoint.x2, startingPoint.x3 };
        for (var i = 1; i < N + 1; i++)
        {
            matrixD[i] = new double[N];
            for (var j = 0; j < N; j++)
            {
                matrixD[i][j] = matrixD[0][j] + (j == i - 1
                    ? D1(distanceBetweenTwoPoints)
                    : D2(distanceBetweenTwoPoints));
            }
        }
    }

    private static double D1(double distanceBetweenTwoPoints)
        => distanceBetweenTwoPoints / (N * Math.Sqrt(2)) * (Math.Sqrt(N + 1) + N - 1);

    private static double D2(double distanceBetweenTwoPoints)
        => distanceBetweenTwoPoints / (N * Math.Sqrt(2)) * (Math.Sqrt(N + 1) - 1);
}