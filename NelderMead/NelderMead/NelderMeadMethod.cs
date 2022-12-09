namespace NelderMead;

public static class NelderMeadMethod
{
    public static int ObjectiveFunction((int x1, int x2, int x3) point)
        => -5 * point.x1 * (int)Math.Pow(point.x2, 2) * point.x3
           + 2 * (int)Math.Pow(point.x1, 2) * point.x2
           - 3 * point.x1 * (int)Math.Pow(point.x2, 4)
           + point.x1 * (int)Math.Pow(point.x3, 2);
}