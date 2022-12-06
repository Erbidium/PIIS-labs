using SimplexMethod;

const string path = @"C:\Users\Acer\Documents\PIIS-labs\simplexTable.txt";
var simplexTable = FileReader.ReadSimplexTable(path);
Console.WriteLine("Initial simplex table");
ConsolePrinter.PrintMatrix(simplexTable);
SimplexMethod.SimplexMethod.Run(simplexTable);