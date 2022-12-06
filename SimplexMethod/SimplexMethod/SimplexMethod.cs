namespace SimplexMethod;

public static class SimplexMethod
{
    public static void Run(double[,] simplexTable)
    {
        var algorithmStop = false;
        while (!algorithmStop)
        {
            var freeVariablesToEnter = GetFreeVariableToEnterOrderedByCoefficientDescending(simplexTable);
            if (freeVariablesToEnter.Count == 0)
            {
                break;
            }

            var variableToLeaveBasisWasFound = false;
            foreach (var variable in freeVariablesToEnter)
            {
                Console.WriteLine($"Entering: X{variable+1}");
                var rowNumber = GetRowOfVariableToLeaveBasis(simplexTable, variable);
                
                if (!rowNumber.HasValue) continue;

                Console.WriteLine($"Leaving X{GetBasisVariableNumberByRow(simplexTable, rowNumber.Value) + 1}");
                simplexTable.DivideRowByNumber(rowNumber.Value, simplexTable[rowNumber.Value, variable]);

                for (var i = 0; i < simplexTable.GetLength(0); i++)
                {
                    if (i == rowNumber.Value) continue;

                    var multiplier = simplexTable[i, variable];
                    for (var j = 0; j < simplexTable.GetLength(1); j++)
                    {
                        simplexTable[i, j] -= multiplier * simplexTable[rowNumber.Value, j];
                    }
                }
                Console.WriteLine("New simplex table");
                ConsolePrinter.PrintMatrix(simplexTable);
                variableToLeaveBasisWasFound = true;
                break;
            }
            if (variableToLeaveBasisWasFound == false)
            {
                algorithmStop = true;
            }
        }
        ConsolePrinter.PrintAlgorithmResults(simplexTable);
    }

    public static List<int> GetFreeVariableToEnterOrderedByCoefficientDescending(double[,] simplexTable)
    {
        return Enumerable.Range(0, simplexTable.GetLength(1) - 1)
            .Select((columnNumber, index) => (coefficient: simplexTable[0, columnNumber], variable: index))
            .Where(tuple => tuple.coefficient > 0)
            .OrderByDescending(k => k.coefficient)
            .Select(tuple => tuple.variable)
            .ToList();
    }

    public static int? GetRowOfVariableToLeaveBasis(double[,] simplexTable, int columnNumber)
    {
        var rows = Enumerable.Range(1, simplexTable.GetLength(0) - 1)
            .Select((rowNumber) =>
            {
                var coefficient = simplexTable[rowNumber, columnNumber];
                var lastColumnValue = simplexTable[rowNumber, simplexTable.GetLength(1) - 1];
                return (
                    coefficient,
                    lastColumnValue,
                    row: rowNumber,
                    ratioTest: coefficient != 0 ? lastColumnValue / coefficient : coefficient
                );
            })
            .Where(tuple => tuple.coefficient > 0)
            .OrderBy(tuple => tuple.ratioTest)
            .Select(tuple => tuple.row).ToList();
        return rows.Count > 0 ? rows[0] : null;
    }

    public static void DivideRowByNumber(this double[,] matrix, int rowNumber, double value)
    {
        for(int i = 0; i < matrix.GetLength(1); i++)
        {
            matrix[rowNumber, i] /= value;
        }
    }

    public static int GetBasisVariableNumberByRow(this double[,] matrix, int rowNumber)
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            var columnIsFound = true;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (i != rowNumber && Math.Abs(matrix[i, j]) > 0.00001)
                {
                    columnIsFound = false;
                }
            }

            if (Math.Abs(matrix[rowNumber, j] - 1) > 0.00001)
            {
                columnIsFound = false;
            }

            if (columnIsFound)
            {
                return j;
            }
        }

        return -1;
    }
}