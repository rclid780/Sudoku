using System;
using SudokuServer.Models;

namespace SudokuServer.Services;

public interface ISolutionService
{
    Task<SolutionDTO?> CreateAsync(PuzzleDTO puzzle);
    Task<SolutionDTO?> GetSolutionAsync(int SolutionId);
    Task<SolutionDTO?> GetSolutionByPuzzleId(int PuzzleId);
}
