using AutoMapper;
using Sudoku;
using SudokuServer.Models;
using SudokuServer.Repository;

namespace SudokuServer.Services;

public class PuzzleService(
    IPuzzleRepository puzzleRepository,
    IMapper mapper
) : IPuzzleService
{
    public async Task<PuzzleDTO> CreatePuzzleAsync()
    {
        int[][] puzzleData = Generator.Generate();
        Puzzle puzzle = await puzzleRepository.AddAsync();
        await puzzleRepository.SaveAsync();
        List<PuzzleSquare> squares = [];
        for(int row = 0; row < puzzleData.Length; row++)
        {
            for(int col = 0; col < puzzleData[row].Length; col ++)
            {
                //we could but we don't need to store zero values this will decrease
                //the amount of data we store for a puzzle by 62% (31 rows instead of 81 rows)
                if (puzzleData[row][col] != 0)
                {
                    squares.Add(new() { PuzzleId = puzzle.PuzzleId, Row = row, Col = col, Value = puzzleData[row][col] });

                }
            }
        }
        puzzle.Squares.AddRange(squares);
        puzzleRepository.Update(puzzle);
        await puzzleRepository.SaveAsync();
        return new(){ PuzzleId = puzzle.PuzzleId, Data = puzzleData };
    }

    public async Task<List<PuzzleDTO>> GetPuzzlesAsync()
    {
        List<Puzzle> puzzles = await puzzleRepository.GetPuzzlesAsync();
        return (List<PuzzleDTO>)mapper.Map(puzzles, typeof(List<Puzzle>), typeof(List<PuzzleDTO>));
    }

    public async Task<PuzzleDTO?> GetPuzzleAsync(int id)
    {
        Puzzle? puzzle = await puzzleRepository.GetByIdAsync(id);
        if(puzzle == null)
        {
            return null;
        }
        return (PuzzleDTO)mapper.Map(puzzle, typeof(Puzzle), typeof(PuzzleDTO));
    }

    public async Task UpdatePuzzleAsync(int puzzleId, int[][] data)
    {
        PuzzleDTO newPuzzle = new() { PuzzleId = puzzleId, Data = data };
        Puzzle puzzle = (Puzzle)mapper.Map(newPuzzle, typeof(PuzzleDTO), typeof(Puzzle));
        puzzleRepository.Update(puzzle);
        await puzzleRepository.SaveAsync();
    }
}