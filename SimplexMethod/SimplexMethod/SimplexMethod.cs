﻿namespace SimplexMethod;

public static class SimplexMethod
{
    public static /*(List<double>, double)*/void Run(double[,] simplexTable)
    {
        var algorithmStop = false;
        while (!algorithmStop)
        {
            var freeVariablesToEnter = GetFreeVariableToEnterOrderedByCoefficientDescending(simplexTable);
            if (freeVariablesToEnter.Count == 0)
            {
                algorithmStop = true;
                break;
            }

            var variableToLeaveBasisWasFound = false;
            foreach (var variable in freeVariablesToEnter)
            {
                var rowNumber = GetRowOfVariablesToLeaveBasis(simplexTable, variable);
                if (rowNumber.HasValue)
                {
                    simplexTable.DivideRowByNumber(rowNumber.Value, simplexTable[rowNumber.Value, variable]);
                    for (int i = 0; i < simplexTable.GetLength(0); i++)
                    {
                        for (int j = 0; j < simplexTable.GetLength(1); j++)
                        {
                            if (j != rowNumber.Value)
                            {
                                simplexTable[i, j] -= simplexTable[rowNumber.Value, j] * simplexTable[i, variable];
                            }
                        }
                    }
                    variableToLeaveBasisWasFound = true;
                    break;
                }
            }
            Console.WriteLine();
            for (int i = 0; i < simplexTable.GetLength(0); i++)
            {
                for (int j = 0; j < simplexTable.GetLength(1); j++)
                {
                    Console.Write($"{simplexTable[i, j] }".PadLeft(7));
                }
                Console.WriteLine();
            }
            if (variableToLeaveBasisWasFound == false)
            {
                algorithmStop = true;
            }
        }
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

    public static int? GetRowOfVariablesToLeaveBasis(double[,] simplexTable, int columnNumber)
    {
        return Enumerable.Range(0, simplexTable.GetLength(0))
            .Select((rowNumber, index) =>
            {
                var coefficient = simplexTable[rowNumber, columnNumber];
                var lastColumnValue = simplexTable[rowNumber, simplexTable.GetLength(1) - 1];
                return (
                    coefficient,
                    lastColumnValue,
                    variable: index,
                    ratioTest: lastColumnValue / coefficient
                );
            })
            .Where(tuple => tuple.coefficient > 0)
            .OrderBy(tuple => tuple.ratioTest)
            .Select(tuple => tuple.variable)
            .FirstOrDefault();
    }

    public static void DivideRowByNumber(this double[,] matrix, int rowNumber, double value)
    {
        for(int i = 0; i < matrix.GetLength(1); i++)
        {
            matrix[rowNumber, i] /= value;
        }
    }
}