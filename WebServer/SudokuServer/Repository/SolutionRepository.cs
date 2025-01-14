using Microsoft.EntityFrameworkCore;
using SudokuServer.Data;
using SudokuServer.Models;

namespace SudokuServer.Repository;

public class SolutionRepository(
    SolutionContext solutionDb
) : ISolutionRepository, IDisposable
{
    private bool disposed = false;

    public async Task<Solution> AddAsync(int puzzleId)
    {
        Solution solution = new(){PuzzleId = puzzleId};
        await solutionDb.Solution.AddAsync(solution);
        return solution;        
    }

    public async Task<Solution?> GetByIdAsync(int solutionId)
    {
        return await solutionDb.Solution.Where(s => s.SolutionId == solutionId && s.Squares.Count > 0)
            .Select(s => new Solution(){
                SolutionId = s.SolutionId,
                PuzzleId = s.PuzzleId,
                Squares = s.Squares
            }).FirstOrDefaultAsync();
    }

    public async Task<Solution?> GetByPuzzleIdAsync(int puzzleId)
    {
        return await solutionDb.Solution.Where(s => s.PuzzleId == puzzleId && s.Squares.Count > 0)
            .Select(s => new Solution()
            {
                SolutionId = s.SolutionId,
                PuzzleId = s.PuzzleId,
                Squares = s.Squares
            }).FirstOrDefaultAsync();
    }

    public void Update(Solution solution)
    {
        solutionDb.Update(solution);
    }

    protected virtual void Dispose(bool disposing)
    {
        if(!this.disposed)
        {
            if(disposing)
            {
                solutionDb.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task SaveAsync()
    {
        await solutionDb.SaveChangesAsync();
    }
}