using AutoMapper;
using SudokuServer.Models;

namespace SudokuServer.Helpers;

public class Solution2Dto : ITypeConverter<Solution, SolutionDTO>
{
    public SolutionDTO Convert(Solution source, SolutionDTO destination, ResolutionContext context)
    {
        destination = new() { SolutionId = source.SolutionId, PuzzleId = source.PuzzleId };
        foreach (SolutionSquare square in source.Squares)
        {
            destination.Data[square.Row][square.Col] = square.Value;
        }
        return destination;
    }
}
