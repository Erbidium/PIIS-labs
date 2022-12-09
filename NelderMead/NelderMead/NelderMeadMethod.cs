namespace NelderMead;

public static class NelderMeadMethod
{
    private const int Alpha = 1;
    private const int GammaLowerBound = 2;
    private const int GammaUpperBound = 3;
    private const double BetaLowerBound = 0.4;
    private const double BetaUpperBound = 0.6;

    public static int ObjectiveFunction((int x1, int x2, int x3) point)
        => -5 * point.x1 * (int)Math.Pow(point.x2, 2) * point.x3
           + 2 * (int)Math.Pow(point.x1, 2) * point.x2
           - 3 * point.x1 * (int)Math.Pow(point.x2, 4)
           + point.x1 * (int)Math.Pow(point.x3, 2);

    public static void Run((int x1, int x2, int x3) startingPoint, int distanceBetweenTwoPoints, double precision, int iterationsNumber)
    {
        
        
    }
}