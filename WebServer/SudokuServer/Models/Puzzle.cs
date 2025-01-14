namespace SudokuServer.Models;

public class Puzzle
{
    public int PuzzleId { get; set; }
    public List<PuzzleSquare> Squares { get; set; } = [];
}
