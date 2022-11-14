namespace Lab4;

public static class Menu
{
    public static void RunKarpRabinAlgorithm()
    {
        Console.WriteLine("Please, enter row of text: ");
        var text = Console.ReadLine();
        Console.WriteLine("Please, enter text to find: ");
        var textToFind = Console.ReadLine();

        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(textToFind))
        {
            Console.WriteLine("Wrong entered input text");
            return;
        }
        KarpRabin.GetIndexesOfFoundSubstring(text, textToFind);
    }

    public static void RunDjkstraAlgorithm()
    {
        
    }

    public static void RunPrimAlgorithm()
    {
        
    }
}