using SimplexMethod;

const string path = @"C:\Users\Acer\Documents\PIIS-labs\simplexTable.txt";
Console.WriteLine("Parsed table");
var simplexTable = FileReader.ReadSimplexTable(path);
for (int i = 0; i < simplexTable.GetLength(0); i++)
{
    for (int j = 0; j < simplexTable.GetLength(1); j++)
    {
        Console.Write($"{simplexTable[i, j] }".PadLeft(7));
    }
    Console.WriteLine();
}
Console.WriteLine();
SimplexMethod.SimplexMethod.Run(simplexTable);