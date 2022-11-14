namespace Lab4;

public static class KarpRabin
{
    public static int GetHash(string str)
        => Convert.ToInt32(str) % 23;
}