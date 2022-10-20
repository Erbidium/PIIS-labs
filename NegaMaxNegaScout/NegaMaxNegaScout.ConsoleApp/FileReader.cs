namespace PathfindingLab1.ConsoleApp;

public static class FileReader
{
    public static void ReadGameFieldWithStartData(out int[,] fieldMatrix, out (int, int) startPlayerPoint,
        out (int, int) finishPoint, out (int, int) enemyPoint)
    {
        var filePath = @"";
        var fileLines = File.ReadLines(filePath).ToList();

        startPlayerPoint = (int.Parse(fileLines[0][0].ToString()), int.Parse(fileLines[0][1].ToString()));
        enemyPoint = (int.Parse(fileLines[1][0].ToString()), int.Parse(fileLines[1][1].ToString()));
        finishPoint = (int.Parse(fileLines[2][0].ToString()), int.Parse(fileLines[2][1].ToString()));

        var fieldHeight = fileLines.Count - 3;
        var fieldWidth = fileLines[3].Length;
        fieldMatrix = new int[fieldHeight, fieldWidth];

        for (var i = 3; i < fileLines.Count; i++)
        {
            for (var j = 0; j < fileLines[i].Length; j++)
            {
                if (!int.TryParse(fileLines[i][j].ToString(), out var pointValue) || pointValue > 1)
                {
                    Console.WriteLine("Wrong point value");
                    return;
                }

                fieldMatrix[i - 3, j] = pointValue;
            }
        }
    }
}