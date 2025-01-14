using Microsoft.EntityFrameworkCore;
using SudokuServer.Data;
using SudokuServer.Models;

namespace SudokuServer.Repository;

public class PuzzleRepository(
    PuzzleContext puzzleDb
) : IPuzzleRepository, IDisposable
{
    private bool disposed = false;

    public async Task<Puzzle> AddAsync()
    {
        Puzzle puzzle = new();
        await puzzleDb.AddAsync(puzzle);
        return puzzle;
    }

    public async Task<List<Puzzle>> GetPuzzlesAsync()
    {
        return await puzzleDb.Puzzle.Where(p => p.Squares.Count > 0)
            .Select(p => new Puzzle()
            {
                PuzzleId = p.PuzzleId,
                Squares = p.Squares
            }).ToListAsync();
    }

    public async Task<Puzzle?> GetByIdAsync(int id)
    {
        return await puzzleDb.Puzzle.Where(p => p.PuzzleId == id && p.Squares.Count > 0)
            .Select(p => new Puzzle(){
                PuzzleId = p.PuzzleId,
                Squares = p.Squares
            }).FirstOrDefaultAsync();
    }

    public async Task SaveAsync()
    {
        await puzzleDb.SaveChangesAsync();
    }

    public void Update(Puzzle puzzle)
    {
        puzzleDb.Puzzle.Update(puzzle);
    }

    protected virtual void Dispose(bool disposing)
    {
        if(!this.disposed)
        {
            if(disposing)
            {
                puzzleDb.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
