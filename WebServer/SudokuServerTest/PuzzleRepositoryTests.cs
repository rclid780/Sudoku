using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SudokuServer.Data;
using SudokuServer.Models;
using SudokuServer.Repository;

namespace SudokuServerTest;

[TestClass]
public class PuzzleRepositoryTests
{
    private ServiceProvider? _serviceProvider;
    private PuzzleRepository? repository;

    [TestInitialize]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddDbContext<PuzzleContext>(
            opt => opt.UseInMemoryDatabase("TestDb")
        );
        services.AddDbContext<SolutionContext>(
            opt => opt.UseInMemoryDatabase("TestDb")
        );
        services.AddScoped<IPuzzleRepository, PuzzleRepository>();
        _serviceProvider = services.BuildServiceProvider();

        repository = new(_serviceProvider?.GetService<PuzzleContext>()!);
    }

    [TestCleanup]
    public void CleanUp()
    {
        var puzzleContext = _serviceProvider?.GetService<PuzzleContext>();
        puzzleContext?.Database?.EnsureDeleted();
        repository?.Dispose();
    }

    [TestMethod]
    public async Task TestPuzzleRepository()
    {
        List<Puzzle> puzzles = await repository!.GetPuzzlesAsync();
        Assert.AreEqual(0, puzzles.Count);

        Puzzle? puzzleNull = await repository.GetByIdAsync(1);
        Assert.IsNull(puzzleNull);

        Puzzle puzzle = await repository.AddAsync();
        Assert.IsNotNull(puzzle);

        await repository.SaveAsync();

        List<PuzzleSquare> squares = [
            new PuzzleSquare(){ PuzzleId = puzzle.PuzzleId, Row = 0, Col = 0, Value = 1},
            new PuzzleSquare(){ PuzzleId = puzzle.PuzzleId, Row = 8, Col = 8, Value = 9}
        ];
        puzzle.Squares.AddRange(squares);

        repository.Update(puzzle);
        await repository.SaveAsync();

        //now we can test the gets
        puzzles = await repository.GetPuzzlesAsync();
        Assert.AreEqual(1, puzzles.Count);
        Assert.AreEqual(puzzle.PuzzleId, puzzles[0].PuzzleId);
        Assert.AreEqual(2, puzzles[0].Squares.Count);

        Puzzle? puzzleNotNull = await repository.GetByIdAsync(puzzle.PuzzleId);
        Assert.IsNotNull(puzzleNotNull);
        Assert.AreEqual(puzzle.PuzzleId, puzzleNotNull.PuzzleId);
        Assert.AreEqual(2, puzzleNotNull.Squares.Count);
        Assert.IsTrue(puzzles[0].Squares[0].PuzzleSquareId > 0);
        Assert.AreEqual(puzzle.PuzzleId, puzzles[0].Squares[0].PuzzleId);
    }
}
