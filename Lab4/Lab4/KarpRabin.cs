namespace Lab4;

public static class KarpRabin
{
    private const int variant = 23;
    public static int GetHash(string str)
        => Convert.ToInt32(str) % variant;

    public static List<int> GetIndexesOfFoundSubstring(string text, string textToFind)
    {
        var length = textToFind.Length;
        var textToFindHash = GetHash(textToFind);
        var foundIndexes = new List<int>();
        for (var i = 0; i <= text.Length - length; i++)
        {
            var substringToCompare = text.Substring(i, length);
            if (textToFindHash == GetHash(substringToCompare) && textToFind == substringToCompare)
            {
                foundIndexes.Add(i);
            }
        }

        return foundIndexes;
    }
}