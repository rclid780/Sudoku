using SudokuServer.Models;

namespace SudokuServer.Repository;

public interface IPuzzleRepository
{
        Task<Puzzle> AddAsync();
        Task<List<Puzzle>> GetPuzzlesAsync();
        Task<Puzzle?> GetByIdAsync(int id);
        void Update(Puzzle puzzle);
        Task SaveAsync();
}
