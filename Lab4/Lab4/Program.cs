using Lab4;

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

