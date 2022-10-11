namespace PathfindingLab1.ConsoleApp.Helpers;

public static class FieldMatrixHelpers
{
    public static bool TopNeighboringCellIsFree(int i, int j, int[,] fieldMatrix) => i > 0 && fieldMatrix[i - 1, j] == 0;
    public static bool BottomNeighboringCellIsFree(int i, int j, int[,] fieldMatrix, int fieldHeight) => i < fieldHeight - 1 && fieldMatrix[i + 1, j] == 0;
    public static bool LeftNeighboringCellIsFree(int i, int j, int[,] fieldMatrix) => j > 0 && fieldMatrix[i, j - 1] == 0;
    public static bool RightNeighboringCellIsFree(int i, int j, int[,] fieldMatrix, int fieldWidth) => j < fieldWidth - 1 && fieldMatrix[i, j + 1] == 0;
}