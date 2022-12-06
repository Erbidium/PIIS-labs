using SimplexMethod;

const string path = @"C:\Users\Acer\Documents\PIIS-labs\simplexTable.txt";
var simplexTable = FileReader.ReadSimplexTable(path);

SimplexMethod.SimplexMethod.Run(simplexTable);