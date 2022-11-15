using Lab4;

Console.WriteLine("(0) Rabin-Karp algorithm");
Console.WriteLine("(1) Djkstra's algorithm");
Console.WriteLine("(2) Rabin-Karp algorithm");
Console.WriteLine("Please, enter your choice");

var userInput = Console.ReadLine();
if (int.TryParse(userInput, out var choice))
{
    switch (choice)
    {
        case 0: Menu.RunKarpRabinAlgorithm();
            break;
        case 1: Menu.RunDjkstraAlgorithm();
            break;
        default: Menu.RunPrimAlgorithm();
            break;
    }
}

