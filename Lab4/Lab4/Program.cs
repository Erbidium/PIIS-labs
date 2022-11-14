using Lab4;

Console.WriteLine("Please, enter row of text: ");
var text = Console.ReadLine();
Console.WriteLine("Please, enter text to find: ");
var textToFind = Console.ReadLine();

var length = textToFind.Length;

for (int i = 0; i <= text.Length - length; i++)
{
    if (KarpRabin.GetHash(textToFind) == KarpRabin.GetHash(text.Substring(i, length)))
    {
        Console.WriteLine("Find!");
        break;
    }
}

