using Microsoft.EntityFrameworkCore;
using SudokuServer.Models;

namespace SudokuServer.Data;

public class SolutionContext(DbContextOptions<SolutionContext> options) : DbContext(options)
{
    public DbSet<Solution> Solution => Set<Solution>();
}
