using System.Net;
using Microsoft.AspNetCore.Mvc;
using SudokuServer.Models;
using SudokuServer.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SudokuServer.Controllers;

/// <summary>
///     The controller includes methods to generate and solve Sudoku problems
/// </summary>
/// <param name="logger"></param>
[ApiController]
[Route("api/[controller]")]
public class PuzzlesController(
    ILogger<PuzzlesController> logger,
    IPuzzleService puzzleService,
    ISolutionService solutionService
) : ControllerBase
{
    private readonly ILogger<PuzzlesController> _logger = logger;

    [HttpGet(Name = "GetAllSudokuPuzzles")]
    [ProducesResponseType(typeof(int[]), 200)]
    [SwaggerOperation(
        Summary = "Get list if the avaliable puzzle id(s)",
        Description = "Returns List of all Sudoku puzzle id(s)",
        OperationId = "GetPuzzles"
    )]
    public async Task<IActionResult> GetPuzzles()
    {
        List<PuzzleDTO> puzzles = await puzzleService.GetPuzzlesAsync();
        return Ok($"[{string.Join(", ", puzzles.Select(p => p.PuzzleId))}]");
    }

    [HttpPost(Name = "CreateNewSudokuPuzzle")]
    [ProducesResponseType(typeof(int), 201)]
    [SwaggerOperation(
        Summary = "Create a new puzzle",
        Description = "Returns the Id of the newly created Sudoku puzzle",
        OperationId = "PostPuzzle"
    )]
    public async Task<IActionResult> PostPuzzle()
    {
        PuzzleDTO puzzle = await puzzleService.CreatePuzzleAsync();
        return StatusCode(StatusCodes.Status201Created, puzzle.PuzzleId);
    }

    [HttpGet("{puzzleId}", Name = "GetSudokuPuzzleById")]
    [ProducesResponseType(typeof(PuzzleDTO), 200)]
    [ProducesResponseType(typeof(string), 404)]
    [SwaggerOperation(
        Summary = "Get puzzle data for the id specified",
        Description = "Returns the board data for the Sudoku puzzle id provided",
        OperationId = "GetPuzzle"
    )]
    public async Task<IActionResult> GetPuzzle(int puzzleId)
    {
        PuzzleDTO? puzzle = await puzzleService.GetPuzzleAsync(puzzleId);
        if(puzzle == null)
        {
            return NotFound($"Puzzle {puzzleId} was not found");
        }
        return Ok(puzzle.ToString());
    }

    [HttpPut("{puzzleId}", Name = "ModifySudokuPuzzle")]
    [ProducesResponseType(typeof(string), 200)]
    [SwaggerOperation(
        Summary = "Update puzzle data for the id specified",
        Description = "Updates the board for the Sudoku puzzle id",
        OperationId = "PutPuzzle"
    )]
    public async Task<IActionResult> PutPuzzle(int puzzleId, int[][] data)
    {
        await puzzleService.UpdatePuzzleAsync(puzzleId, data);
        return Ok("Success");
    }

    [HttpGet("{puzzleId}/solutions", Name = "GetSudokuPuzzleSolution")]
    [ProducesResponseType(typeof(SolutionDTO), 200)]
    [ProducesResponseType(typeof(string), 303)]
    [SwaggerOperation(
        Summary = "Get Solution for the puzzle id specified",
        Description = "Returns the solution to the Sudoku puzzle id provided if the solutoin has already been generated",
        OperationId = "GetSolution"
    )]
    public async Task<IActionResult> GetSolution(int puzzleId)
    {
        SolutionDTO? solution = await solutionService.GetSolutionByPuzzleId(puzzleId);
        if(solution == null)
        {
            return StatusCode(StatusCodes.Status303SeeOther, "No solution found, try Post to create a solution");
        }
        return Ok(solution.ToString());
    }

    [HttpPost("{puzzleId}/solutions", Name = "CreateSudokuPuzzleSolution")]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(typeof(string), 404)]
    [SwaggerOperation(
        Summary = "Create Solution for the puzzle id specified",
        Description = "Creates a solution for a Sudoku puzzle if one doesn't already exist",
        OperationId = "PostSolution"
    )]
    public async Task<IActionResult> PostSolution(int puzzleId)
    {
        PuzzleDTO? puzzle = await puzzleService.GetPuzzleAsync(puzzleId);
        if(puzzle == null)
        {
            return NotFound($"puzzle {puzzleId} does not exist to solve");
        }

        SolutionDTO? solution = await solutionService.CreateAsync(puzzle);
        if(solution == null)
        {
            return NotFound($"A solution to the puzzle could not be found for puzzle {puzzleId}");
        }

        return Ok(solution.SolutionId);
    }

    [HttpGet("{puzzleId}/solutions/{solutionId}", Name = "GetSudokuPuzzlesSolution")]
    [ProducesResponseType(typeof(SolutionDTO), 200)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 418)]
    [SwaggerOperation(
        Summary = "Get a the solution to a puzzle by its solution id",
        Description = "Returns a spcific soltion to a the Sudoku puzzle specified",
        OperationId = "GetPuzzleSolution"
    )]
    public async Task<IActionResult> GetPuzzleSolution(int puzzleId, int solutionId)
    {
        SolutionDTO? solution = await solutionService.GetSolutionAsync(solutionId);
        if(solution == null)
        {
            return NotFound($"Solution {solutionId} not found for puzzle {puzzleId}");
        }
        if(solution.PuzzleId != puzzleId)
        {
            return StatusCode(StatusCodes.Status418ImATeapot, "The solution requested does not solve the puzzle specified");
        }
        return Ok(solution.ToString());
    }
}
