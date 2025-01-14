namespace SudokuServer.Models;

public class PuzzleSquare
{
    public int PuzzleSquareId{ get; set; }
    public int PuzzleId { get; set; }
    public int Row { get; set; }
    public int Col { get; set; }
    public int Value { get; set; }
}
