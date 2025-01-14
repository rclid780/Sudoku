using SudokuServer.Models;

namespace SudokuServerTest;

public class TestData
{
   //This is the data used for the tests so that we can properly Mock actual behavior of solvable puzzle
    public static PuzzleDTO? GetPuzzleDTO(int id)
    {
        PuzzleDTO? puzzle = new()
        {
            PuzzleId = id,
            Data = [
                [0,0,4,7,2,0,0,3,1],
                [0,5,0,1,0,4,7,6,8],
                [7,1,0,0,6,0,9,0,4],
                [5,0,0,0,0,0,0,0,0],
                [8,0,7,9,0,1,2,4,0],
                [0,0,0,0,8,3,6,0,5],
                [1,7,9,0,3,6,4,5,0],
                [4,0,0,0,0,7,8,9,6],
                [0,8,0,0,0,0,3,0,0]
            ]
        };
        return puzzle;
    }

    public static PuzzleDTO GetUnsolvablePuzzle(int id)
    {
        PuzzleDTO puzzle = GetPuzzleDTO(id)!;
        puzzle.Data[0][0] = 5;
        return puzzle;
    }

    public static string GetPuzzleAsString()
    {
        return "{PuzzleId:1,Data:[[0,0,4,7,2,0,0,3,1],[0,5,0,1,0,4,7,6,8],[7,1,0,0,6,0,9,0,4],[5,0,0,0,0,0,0,0,0],[8,0,7,9,0,1,2,4,0],[0,0,0,0,8,3,6,0,5],[1,7,9,0,3,6,4,5,0],[4,0,0,0,0,7,8,9,6],[0,8,0,0,0,0,3,0,0]]}";
    }
    
    public static Puzzle? GetPuzzleObj(int id)
    {
        return new()
        {
            PuzzleId = id,
            Squares =
            [
                new PuzzleSquare(){Row = 0, Col = 0, Value = 1},
                new PuzzleSquare(){Row = 8, Col = 8, Value = 9}
            ]
        };
    }

    public static SolutionDTO? GetSolutionDTO(int solutionId, int puzzleId)
    {
        return new()
        {
            SolutionId = solutionId,
            PuzzleId = puzzleId,
            Data = [
                [6,9,4,7,2,8,5,3,1],
                [3,5,2,1,9,4,7,6,8],
                [7,1,8,3,6,5,9,2,4],
                [5,4,3,6,7,2,1,8,9],
                [8,6,7,9,5,1,2,4,3],
                [9,2,1,4,8,3,6,7,5],
                [1,7,9,8,3,6,4,5,2],
                [4,3,5,2,1,7,8,9,6],
                [2,8,6,5,4,9,3,1,7]
            ]
        };
    }

    public static string GetSolutionAsString()
    {
        return "{SolutionId:1,PuzzleId:1,Data:[[6,9,4,7,2,8,5,3,1],[3,5,2,1,9,4,7,6,8],[7,1,8,3,6,5,9,2,4],[5,4,3,6,7,2,1,8,9],[8,6,7,9,5,1,2,4,3],[9,2,1,4,8,3,6,7,5],[1,7,9,8,3,6,4,5,2],[4,3,5,2,1,7,8,9,6],[2,8,6,5,4,9,3,1,7]]}";
    }

    public static Solution? GetSolutionObj(int solutionId, int puzzleId)
    {
        return new Solution()
        {
            SolutionId = solutionId,
            PuzzleId = puzzleId,
            Squares = [
                new SolutionSquare(){Row = 0, Col = 0, Value = 1},
                new SolutionSquare(){Row = 8, Col = 8, Value = 9}
            ]
        };
    }
}
