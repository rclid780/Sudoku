using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SudokuServer.Data;
using SudokuServer.Models;
using SudokuServer.Repository;

namespace SudokuServerTest;

[TestClass]
public class SolutionRepositoryTests
{
    private ServiceProvider? _serviceProvider;
    private SolutionRepository? repository;

    [TestInitialize]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddDbContext<SolutionContext>(
            opt => opt.UseInMemoryDatabase("TestDb")
        );
        services.AddDbContext<SolutionContext>(
            opt => opt.UseInMemoryDatabase("TestDb")
        );
        services.AddScoped<ISolutionRepository, SolutionRepository>();
        _serviceProvider = services.BuildServiceProvider();

        repository = new(_serviceProvider?.GetService<SolutionContext>()!);
    }

    [TestCleanup]
    public void CleanUp()
    {
        var solutionContext = _serviceProvider?.GetService<SolutionContext>();
        solutionContext?.Database?.EnsureDeleted();
        repository?.Dispose();
    }

    [TestMethod]
    public async Task TestSolutionRepository()
    {
        int puzzleId = 1;

        Solution? solutionNull = await repository!.GetByIdAsync(1);
        Assert.IsNull(solutionNull);

        solutionNull = await repository!.GetByPuzzleIdAsync(puzzleId);
        Assert.IsNull(solutionNull);

        Solution solution = await repository.AddAsync(puzzleId);
        Assert.IsNotNull(solution);
        await repository.SaveAsync();

        List<SolutionSquare> squares = [
            new SolutionSquare(){ SolutionId = solution.SolutionId, Row = 0, Col = 0, Value = 1},
            new SolutionSquare(){ SolutionId = solution.SolutionId, Row = 8, Col = 8, Value = 9}
        ];
        solution.Squares.AddRange(squares);
        
        repository.Update(solution);
        await repository.SaveAsync();

        Solution? solutionGet = await repository.GetByIdAsync(solution.SolutionId);
        Assert.IsNotNull(solutionGet);
        Assert.AreEqual(solution.SolutionId, solutionGet.SolutionId);
        Assert.AreEqual(2, solutionGet.Squares.Count);

        Solution? solutionByPuzzle = await repository.GetByPuzzleIdAsync(puzzleId);
        Assert.IsNotNull(solutionByPuzzle);
        Assert.AreEqual(solution.SolutionId, solutionByPuzzle.SolutionId);
        Assert.AreEqual(2, solutionByPuzzle.Squares.Count);
        Assert.IsTrue(solutionByPuzzle.Squares[0].SolutionSquareId > 0);
        Assert.AreEqual(solution.SolutionId, solutionByPuzzle.Squares[0].SolutionId);
    }
}
