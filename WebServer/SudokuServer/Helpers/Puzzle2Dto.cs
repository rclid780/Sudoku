using AutoMapper;
using SudokuServer.Models;

namespace SudokuServer.Helpers;

public class Puzzle2Dto : ITypeConverter<Puzzle, PuzzleDTO>
{
    public PuzzleDTO Convert(Puzzle source, PuzzleDTO destination, ResolutionContext context)
    {
        destination = new() { PuzzleId = source.PuzzleId };
        foreach (PuzzleSquare square in source.Squares)
        {
            destination.Data[square.Row][square.Col] = square.Value;
        }
        return destination;
    }
}