using System.Text.Json;

namespace SudokuServer.Helpers;

/// <summary>
///  This class main purpose it to cache the JsonSerializerOptions so that we aren't constantly creating a new one
///  This also servers to reduce code duplication across the web interfaces that need to serialize data return to the client
/// </summary>
public static class JsonSerialize
{
    private static JsonSerializerOptions? options;

    /// <summary>
    ///     Convert 2D array representing a Sudoku puzzle or solution to a JSON object
    /// </summary>
    /// <param name="grid">
    ///     int[9,9] representation of a Sudoku puzzle or solution
    /// </param>
    /// <param name="itemName">
    ///     The name expected as the JSON key name in the object
    /// </param>
    /// <returns></returns>
    public static string Serialize(int[][] grid)
    {
        if(options == null)
        {
            options = new();
            options.Converters.Add(new ArrayJsonConverter());
        }

        string jsonArray = JsonSerializer.Serialize(grid, options);
        return jsonArray;
    }
}