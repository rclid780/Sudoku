using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.OpenApi.Any;
using Moq;
using SudokuServer.Helpers;
using SudokuServer.Models;
using SudokuServer.Repository;
using SudokuServer.Services;

namespace SudokuServerTest;

[TestClass]
public class SolutionServiceTests
{

    readonly Mock<ISolutionRepository> solutionMock = new();
    private SolutionService? testService;

    
    [TestInitialize]
    public void Setup()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        testService = new SolutionService(solutionMock.Object, mapperConfig.CreateMapper());
    }

    [TestMethod]
    public async Task CreateAlreadyExists()
    {
        int puzzleId = 1;
        int solutionId = 1;
        PuzzleDTO? puzzle = TestData.GetPuzzleDTO(puzzleId);
        Solution? solution = TestData.GetSolutionObj(solutionId, puzzleId);
        solutionMock.Setup(X => X.GetByPuzzleIdAsync(puzzleId)).Returns(Task.FromResult(solution));

        SolutionDTO? result = await testService!.CreateAsync(puzzle!);

        Assert.IsNotNull(result);
        Assert.AreEqual(solutionId, result.SolutionId);
        Assert.AreEqual(puzzleId, result.PuzzleId);
    }

    [TestMethod]
    public async Task CreateNoSolution()
    {
        int puzzleId = 1;
        PuzzleDTO? puzzle = TestData.GetUnsolvablePuzzle(puzzleId);

        SolutionDTO? result = await testService!.CreateAsync(puzzle);

        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task CreateSolution()
    {
        int puzzleId = 1;
        int solutionId = 1;
        PuzzleDTO? puzzle = TestData.GetPuzzleDTO(puzzleId);
        Solution? solution = TestData.GetSolutionObj(solutionId, puzzleId);
        solutionMock.Setup(X => X.AddAsync(It.IsAny<int>())).Returns(Task.FromResult(solution!));

        SolutionDTO? result = await testService!.CreateAsync(puzzle!);

        Assert.IsNotNull(result);
        Assert.AreEqual(solutionId, result.SolutionId);
        Assert.AreEqual(puzzleId, result.PuzzleId);
    }

    [TestMethod]
    public async Task GetSolutionNotFound()
    {
        int solutionId = 1;

        SolutionDTO? result = await testService!.GetSolutionAsync(solutionId);

        Assert.IsNull(result);
    }
    
    [TestMethod]
    public async Task GetSolution()
    {
        int puzzleId = 1;
        int solutionId = 1;
        Solution? solution = TestData.GetSolutionObj(solutionId, puzzleId);
        solutionMock.Setup(X => X.GetByIdAsync(solutionId)).Returns(Task.FromResult(solution));

        SolutionDTO? result = await testService!.GetSolutionAsync(solutionId);

        Assert.IsNotNull(result);
        Assert.AreEqual(puzzleId, result.PuzzleId);
        Assert.AreEqual(solutionId, result.SolutionId);
    }

    [TestMethod]
    public async Task GetSolutionByPuzzleIdNotFound()
    {
        int puzzleId = 1;

        SolutionDTO? result = await testService!.GetSolutionByPuzzleId(puzzleId);

        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task GetSolutionByPuzzleId()
    {
        int puzzleId = 1;
        int solutionId = 1;
        Solution? solution = TestData.GetSolutionObj(solutionId, puzzleId);
        solutionMock.Setup(X => X.GetByPuzzleIdAsync(puzzleId)).Returns(Task.FromResult(solution));

        SolutionDTO? result = await testService!.GetSolutionByPuzzleId(puzzleId);

        Assert.IsNotNull(result);
        Assert.AreEqual(puzzleId, result.PuzzleId);
        Assert.AreEqual(solutionId, result.SolutionId);
    }
}
