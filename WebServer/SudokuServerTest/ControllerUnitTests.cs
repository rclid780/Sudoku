using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SudokuServer.Controllers;
using SudokuServer.Services;
using SudokuServer.Models;
using Microsoft.AspNetCore.Http;

namespace SudokuServerTest;

[TestClass]
public class ControllerUnitTests
{
    readonly Mock<ILogger<PuzzlesController>> logMock = new();
    readonly Mock<IPuzzleService> puzzleMock = new();
    readonly Mock<ISolutionService> solutionMock = new();
    private PuzzlesController? testController;

    [TestInitialize]
    public void Setup()
    {
        testController = new PuzzlesController(logMock.Object, puzzleMock.Object, solutionMock.Object);
    }

    [TestMethod]
    public async Task GetAllPuzzles()
    {
        int id1 = 1;
        int id2 = 2;
        List<PuzzleDTO> puzzleList = [];
        puzzleList.Add(TestData.GetPuzzleDTO(id1)!);
        puzzleList.Add(TestData.GetPuzzleDTO(id2)!);
        puzzleMock.Setup(X => X.GetPuzzlesAsync()).Returns(Task.FromResult(puzzleList));
        Type expectedType = typeof(OkObjectResult);
        string expectedValue = $"[{id1}, {id2}]";
        IActionResult result = await testController!.GetPuzzles();

        Assert.AreEqual(expectedType, result.GetType());
        OkObjectResult ok = (result as OkObjectResult)!;
        Assert.AreEqual(expectedValue, ok.Value);
    }

    [TestMethod]
    public async Task CreatePuzzle()
    {
        int puzzleId = 1;
        PuzzleDTO puzzle = TestData.GetPuzzleDTO(puzzleId)!;
        puzzleMock.Setup(X => X.CreatePuzzleAsync()).Returns(Task.FromResult(puzzle));
        Type expectedType = typeof(ObjectResult);
        int status = StatusCodes.Status201Created;
        int expectedValue = 1;

        IActionResult result = await testController!.PostPuzzle();

        Assert.AreEqual(expectedType, result.GetType());
        ObjectResult ok = (result as ObjectResult)!;
        Assert.AreEqual(status, ok.StatusCode);
        Assert.AreEqual(expectedValue, ok.Value);
    }

    [TestMethod]
    public async Task PuzzleNotFound()
    {
        int puzzleId = 1;
        Type expectedType = typeof(NotFoundObjectResult);
        string expectedValue = $"Puzzle {puzzleId} was not found";

        IActionResult result = await testController!.GetPuzzle(puzzleId);

        Assert.AreEqual(expectedType, result.GetType());
        NotFoundObjectResult notfound = (result as NotFoundObjectResult)!;
        Assert.AreEqual(expectedValue, notfound.Value);
    }

    [TestMethod]
    public async Task GetPuzzle()
    {
        int puzzleId = 1;
        PuzzleDTO? puzzle = TestData.GetPuzzleDTO(puzzleId);
        puzzleMock.Setup(X => X.GetPuzzleAsync(puzzleId)).Returns(Task.FromResult(puzzle));
        Type expectedType = typeof(OkObjectResult);
        string expectedValue = TestData.GetPuzzleAsString();

        IActionResult result = await testController!.GetPuzzle(puzzleId);

        Assert.AreEqual(expectedType, result.GetType());
        OkObjectResult ok = (result as OkObjectResult)!;
        Assert.AreEqual(expectedValue, ok.Value);
    }

    [TestMethod]
    public async Task UpdatePuzzle()
    {
        int puzzleId = 1;
        PuzzleDTO puzzle = TestData.GetPuzzleDTO(puzzleId)!;
        puzzle.Data[4][8] = 3;
        Type expectedType = typeof(OkObjectResult);

        IActionResult result = await testController!.PutPuzzle(puzzleId, puzzle.Data);

        Assert.AreEqual(expectedType, result.GetType());
        OkObjectResult ok = (result as OkObjectResult)!;
        Assert.AreEqual("Success", ok.Value);
    }

    [TestMethod]
    public async Task SolutionNotFound()
    {
        int puzzleId = 1;
        Type expectedType = typeof(ObjectResult);
        int status = StatusCodes.Status303SeeOther;
        string expectedValue = "No solution found, try Post to create a solution";

        IActionResult result = await testController!.GetSolution(puzzleId);

        Assert.AreEqual(expectedType, result.GetType());
        ObjectResult notFound = (result as ObjectResult)!;
        Assert.AreEqual(status, notFound.StatusCode);
        Assert.AreEqual(expectedValue, notFound.Value);
    }

    [TestMethod]
    public async Task GetSolution()
    {
        int solutionId = 1;
        int puzzleId = 1;
        SolutionDTO? solution = TestData.GetSolutionDTO(solutionId, puzzleId);
        solutionMock.Setup(X => X.GetSolutionByPuzzleId(puzzleId)).Returns(Task.FromResult(solution));
        Type expectedType = typeof(OkObjectResult);
        string expectedValue = TestData.GetSolutionAsString();

        IActionResult result = await testController!.GetSolution(puzzleId);

        Assert.AreEqual(expectedType, result.GetType());
        OkObjectResult ok = (result as OkObjectResult)!;
        Assert.AreEqual(expectedValue, ok.Value);
    }

    [TestMethod]
    public async Task PostSolutionNoPuzzle()
    {
        int puzzleId = 1;
        Type expectedType = typeof(NotFoundObjectResult);
        string expectedValue = $"puzzle {puzzleId} does not exist to solve";
        IActionResult result = await testController!.PostSolution(puzzleId);

        Assert.AreEqual(expectedType, result.GetType());
        NotFoundObjectResult notFound = (result as NotFoundObjectResult)!;
        Assert.AreEqual(expectedValue, notFound.Value);
    }

    [TestMethod]
    public async Task PostSolutionNoSolution()
    {
        int puzzleId = 1;
        Type expectedType = typeof(NotFoundObjectResult);
        PuzzleDTO? puzzle = TestData.GetPuzzleDTO(puzzleId);
        puzzleMock.Setup(X => X.GetPuzzleAsync(puzzleId)).Returns(Task.FromResult(puzzle));
        string expectedValue = $"A solution to the puzzle could not be found for puzzle {puzzleId}";
        IActionResult result = await testController!.PostSolution(puzzleId);

        Assert.AreEqual(expectedType, result.GetType());
        NotFoundObjectResult notFound = (result as NotFoundObjectResult)!;
        Assert.AreEqual(expectedValue, notFound.Value);
    }

    [TestMethod]
    public async Task PostSolution()
    {
        int puzzleId = 1;
        int solutionId = 1;
        Type expectedType = typeof(OkObjectResult);
        PuzzleDTO? puzzle = TestData.GetPuzzleDTO(puzzleId);
        SolutionDTO? solution = TestData.GetSolutionDTO(solutionId, puzzleId);
        puzzleMock.Setup(X => X.GetPuzzleAsync(puzzleId)).Returns(Task.FromResult(puzzle));
        solutionMock.Setup(X => X.CreateAsync(puzzle!)).Returns(Task.FromResult(solution));

        IActionResult result = await testController!.PostSolution(puzzleId);

        Assert.AreEqual(expectedType, result.GetType());
        OkObjectResult ok = (result as OkObjectResult)!;
        Assert.AreEqual(solutionId, ok.Value);
    }

    [TestMethod]
    public async Task GetPuzzleSolutionNotFound()
    {
        int puzzleId = 1;
        int solutionId = 1;
        Type expectedType = typeof(NotFoundObjectResult);
        string expectedValue = $"Solution {solutionId} not found for puzzle {puzzleId}";

        IActionResult result = await testController!.GetPuzzleSolution(puzzleId, solutionId);

        Assert.AreEqual(expectedType, result.GetType());
        NotFoundObjectResult notFound = (result as NotFoundObjectResult)!;
        Assert.AreEqual(expectedValue, notFound.Value);
    }

   [TestMethod]
    public async Task GetPuzzleSolutionTeaPot()
    {
        int puzzleId1 = 1;
        int puzzleId2 = 2;
        int solutionId = 1;
        SolutionDTO? solution = TestData.GetSolutionDTO(solutionId, puzzleId1);
        solutionMock.Setup(X => X.GetSolutionAsync(solutionId)).Returns(Task.FromResult(solution));
        Type expectedType = typeof(ObjectResult);
        int status = StatusCodes.Status418ImATeapot;
        string expectedValue = "The solution requested does not solve the puzzle specified";

        IActionResult result = await testController!.GetPuzzleSolution(puzzleId2, solutionId);

        Assert.AreEqual(expectedType, result.GetType());
        ObjectResult teapot = (result as ObjectResult)!;
        Assert.AreEqual(status, teapot.StatusCode);
        Assert.AreEqual(expectedValue, teapot.Value);
    }

    [TestMethod]
    public async Task GetPuzzleSolution()
    {
        int puzzleId = 1;
        int solutionId = 1;
        SolutionDTO? solution = TestData.GetSolutionDTO(solutionId, puzzleId);
        solutionMock.Setup(X => X.GetSolutionAsync(solutionId)).Returns(Task.FromResult(solution));
        Type expectedType = typeof(OkObjectResult);
        string expectedValue = TestData.GetSolutionAsString();

        IActionResult result = await testController!.GetPuzzleSolution(puzzleId, solutionId);

        Assert.AreEqual(expectedType, result.GetType());
        ObjectResult ok = (result as ObjectResult)!;
        Assert.AreEqual(expectedValue, ok.Value);
    }
}
