using System.Text.Json;
using System.Text.Json.Serialization;

namespace SudokuServer.Helpers;

public class ArrayJsonConverter() : JsonConverter<int[][]>
{
    public override int[][]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        //this is not used
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, int[][] value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        for (int i = 0; i < value.Length; i++)
        {
            writer.WriteStartArray();
            for (int j = 0; j < value[i].Length; j++)
            {
                writer.WriteNumberValue(value[i][j]);
            }
            writer.WriteEndArray();
        }
        writer.WriteEndArray();
    }
}