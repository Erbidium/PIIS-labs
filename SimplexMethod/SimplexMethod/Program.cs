using SimplexMethod;

const string path = @"C:\Users\Acer\Documents\PIIS-labs\simplexTable.txt";
Console.WriteLine("Parsed table");
var simplexTable = FileReader.ReadSimplexTable(path);
ConsolePrinter.PrintMatrix(simplexTable);
SimplexMethod.SimplexMethod.Run(simplexTable);