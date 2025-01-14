using SudokuServer.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace SudokuServer.Models;

[SwaggerSchema(Required = ["Puzzle Id", "Data"])]
public class PuzzleDTO
{
    [SwaggerSchema("The id of the Sudoku Puzzle", ReadOnly = true)]
    public int PuzzleId { get; set; }

    [SwaggerSchema("9x9 grid representing the Sudoku board initial state")]
    public int[][] Data { get; set; } = [
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
        return $"{{PuzzleId:{PuzzleId},Data:{JsonSerialize.Serialize(Data)}}}";
    }
}
