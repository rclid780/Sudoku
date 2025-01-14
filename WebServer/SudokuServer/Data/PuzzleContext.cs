using Microsoft.EntityFrameworkCore;
using SudokuServer.Models;

namespace SudokuServer.Data;

public class PuzzleContext(DbContextOptions<PuzzleContext> options) : DbContext(options)
{
    public DbSet<Puzzle> Puzzle => Set<Puzzle>();
}
