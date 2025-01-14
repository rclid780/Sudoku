using AutoMapper;
using SudokuServer.Models;

namespace SudokuServer.Helpers;

public class Dto2Puzzle : ITypeConverter<PuzzleDTO, Puzzle>
{
    public Puzzle Convert(PuzzleDTO source, Puzzle destination, ResolutionContext context)
    {
        destination = new() { PuzzleId = source.PuzzleId };
        for (int i = 0; i < source.Data.Length; i++)
        {
            for (int j = 0; j < source.Data[i].Length; j++)
            {
                destination.Squares.Add(new PuzzleSquare() {PuzzleId = source.PuzzleId, Row = i, Col = j, Value = source.Data[i][j] });
            } 
        }
        return destination;
    }
}
