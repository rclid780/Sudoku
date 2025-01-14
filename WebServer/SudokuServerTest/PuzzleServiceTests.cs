using AutoMapper;
using Moq;
using SudokuServer.Helpers;
using SudokuServer.Models;
using SudokuServer.Repository;
using SudokuServer.Services;

namespace SudokuServerTest;

[TestClass]
public class PuzzleServiceTests
{
    readonly Mock<IPuzzleRepository> puzzleMock = new();
    private PuzzleService? testService;

    [TestInitialize]
    public void Setup()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        testService = new PuzzleService(puzzleMock.Object, mapperConfig.CreateMapper());
    }

    [TestMethod]
    public async Task CreatePuzzle()
    {
        int puzzleId = 1;
        Puzzle puzzle = new() { PuzzleId = puzzleId };
        puzzleMock.Setup(X => X.AddAsync()).Returns(Task.FromResult(puzzle));

        PuzzleDTO result = await testService!.CreatePuzzleAsync();

        Assert.IsNotNull(result);
        Assert.AreEqual(puzzleId, result.PuzzleId);
        Assert.AreEqual(9, result.Data.Length);
    }

    [TestMethod]
    public async Task GetPuzzles()
    {
        int id1 = 1;
        int id2 = 2;
        List<Puzzle> puzzles = [];
        puzzles.Add(TestData.GetPuzzleObj(id1)!);
        puzzles.Add(TestData.GetPuzzleObj(id2)!);
        int listCount = 2;
        puzzleMock.Setup(X => X.GetPuzzlesAsync()).Returns(Task.FromResult(puzzles));

        List<PuzzleDTO> result = await testService!.GetPuzzlesAsync();

        Assert.AreEqual(listCount, result.Count);
        Assert.AreEqual(id1, result[0].PuzzleId);
        Assert.AreEqual(id2, result[1].PuzzleId);        
    }

    [TestMethod]
    public async Task GetPuzzleNotFount()
    {
        int puzzleId = 1;

        PuzzleDTO? result = await testService!.GetPuzzleAsync(puzzleId);

        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task GetPuzzle()
    {
        int puzzleId = 1;
        Puzzle? puzzle = TestData.GetPuzzleObj(puzzleId);
        puzzleMock.Setup(X => X.GetByIdAsync(puzzleId)).Returns(Task.FromResult(puzzle));

        PuzzleDTO? result = await testService!.GetPuzzleAsync(puzzleId);

        Assert.IsNotNull(result);
        Assert.AreEqual(puzzleId, result.PuzzleId);
    }

    [TestMethod]
    public async Task UpdatePuzzle()
    {
        int puzzleId = 1;
        int[][] data = [[1,2,3,4,5,6,7,8,9]];

        await testService!.UpdatePuzzleAsync(puzzleId, data);
    }
}
