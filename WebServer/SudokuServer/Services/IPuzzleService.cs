using SudokuServer.Models;

namespace SudokuServer.Services;

public interface IPuzzleService
{
    Task<List<PuzzleDTO>> GetPuzzlesAsync();
    Task<PuzzleDTO?> GetPuzzleAsync(int id);
    Task<PuzzleDTO> CreatePuzzleAsync();
    Task UpdatePuzzleAsync(int puzzleId, int[][] Data);
}
