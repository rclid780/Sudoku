using SudokuServer.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace SudokuServer.Models;

[SwaggerSchema(Required = ["SolutionId","PuzzleId","Data"])]
public class SolutionDTO
{
    [SwaggerSchema("The id of this Sudoku solution", ReadOnly = true)]
    public int SolutionId { get; set; }

    [SwaggerSchema("The id of this Sudoku puzzle this is a solution solves", ReadOnly = true)]
    public int PuzzleId { get; set; }

    [SwaggerSchema("9x9 grid representing the completed Sudoku board")]
    public int[][] Data  { get; set; } = [
        [0, 0, 0, 0, 0, 0, 0, 0, 0 ],
        [0, 0, 0, 0, 0, 0, 0, 0, 0 ],
        [0, 0, 0, 0, 0, 0, 0, 0, 0 ],
        [0, 0, 0, 0, 0, 0, 0, 0, 0 ],
        [0, 0, 0, 0, 0, 0, 0, 0, 0 ],
        [0, 0, 0, 0, 0, 0, 0, 0, 0 ],
        [0, 0, 0, 0, 0, 0, 0, 0, 0 ],
        [0, 0, 0, 0, 0, 0, 0, 0, 0 ],
        [0, 0, 0, 0, 0, 0, 0, 0, 0 ]
    ];

    public override string ToString()
    {
        return $"{{SolutionId:{SolutionId},PuzzleId:{PuzzleId},Data:{JsonSerialize.Serialize(Data)}}}";
    }

}
