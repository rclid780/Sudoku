using AutoMapper;
using Sudoku;
using SudokuServer.Models;
using SudokuServer.Repository;

namespace SudokuServer.Services;

public class SolutionService(
    ISolutionRepository solutionRepository,
    IMapper mapper
) : ISolutionService
{
    public async Task<SolutionDTO?> CreateAsync(PuzzleDTO puzzle)
    {
        //Check if a solution already exists and return it if it does
        Solution? existingSolution = await solutionRepository.GetByPuzzleIdAsync(puzzle.PuzzleId);
        if(existingSolution != null)
        {
            return (SolutionDTO)mapper.Map(existingSolution, typeof(Solution), typeof(SolutionDTO));
        }
        
        //There isn't already a solution so we will create one
        List<int[][]> solutions = Solver.Solve(puzzle.Data, 1);
        if(solutions.Count == 0)
        {
            return null;
        }
        int[][] solutionData = solutions[0];
        Solution solution = await solutionRepository.AddAsync(puzzle.PuzzleId);
        await solutionRepository.SaveAsync();

        List<SolutionSquare> squares = [];
        for(int row = 0; row < solutionData.Length; row++)
        {
            for(int col = 0; col < solutionData[row].Length; col ++)
            {
                squares.Add(new() { SolutionId = solution.SolutionId, Row = row, Col = col, Value = solutionData[row][col] });
            }
        }
        solution.Squares.AddRange(squares);
        await solutionRepository.SaveAsync();
        return (SolutionDTO)mapper.Map(solution, typeof(Solution), typeof(SolutionDTO));
    }

    public async Task<SolutionDTO?> GetSolutionByPuzzleId(int puzzleId)
    {
        Solution? solution = await solutionRepository.GetByPuzzleIdAsync(puzzleId);
        if(solution == null)
        {
            return null;
        }
        return (SolutionDTO)mapper.Map(solution, typeof(Solution), typeof(SolutionDTO));
    }
    
    public async Task<SolutionDTO?> GetSolutionAsync(int solutionId)
    {
        Solution? solution = await solutionRepository.GetByIdAsync(solutionId);
        if(solution == null)
        {
            return null;
        }
        return (SolutionDTO)mapper.Map(solution, typeof(Solution), typeof(SolutionDTO));
    }
}
