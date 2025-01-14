namespace SudokuServer.Models;

public class Solution
{
    public int SolutionId { get; set; }
    public int PuzzleId { get; set; }
    public List<SolutionSquare> Squares { get; set; } = [];
}
