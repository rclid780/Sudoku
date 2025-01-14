using SudokuServer.Models;

namespace SudokuServer.Repository;

public interface ISolutionRepository
{
    Task<Solution> AddAsync(int PuzzleId);
    Task<Solution?> GetByIdAsync(int SolutionId);
    Task<Solution?> GetByPuzzleIdAsync(int puzzleId);
    void Update(Solution solution);
    Task SaveAsync();
}
