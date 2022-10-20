namespace PathFindingLab1.BLL.Entities;

public class Position
{
    public (int, int) PlayerPosition { get; set; }
    public (int, int) EnemyPosition { get; set; }
    public int Evaluation { get; set; }
}