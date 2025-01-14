namespace SudokuServer.Models;

public class SolutionSquare
{
    public int SolutionSquareId { get; set; }
    public int SolutionId { get; set; }
    public int Row { get; set; }
    public int Col { get; set; }
    public int Value { get; set; }
}

