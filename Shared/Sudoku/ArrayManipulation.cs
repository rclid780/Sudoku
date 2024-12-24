namespace Sudoku;

/// <summary>
///     Since most code deals with int[][] easier than int[,] but int[,] simplifies
///     the back tracking algo we are moving this methods internal to the dll both so
///     that we can provide a more useful dll interface but also so that applications 
///     that use this dll don't have to repeat this code themselves
///     
///     NOTE:This code does not guard against Array Index Errors if is it's passed a 9x9 array
/// </summary>
public static class ArrayManipulation
{
    private static readonly int GridSize = 9;

    public static int[,] CreateSquareArray(int[][] input)
    {
        int[,] ret = new int[GridSize, GridSize];
        for(int i = 0; i < GridSize; i++)
        {
            int[] array = input[i];
            for (int j = 0; j < GridSize; j++)
            {
                ret[i, j] = array[j];
            }
        }
        return ret;
    }

    public static int[][] CreateJaggedArray(int[,] input)
    {
        int[][] ret = new int[GridSize][];
        for(int i = 0; i < GridSize; i++)
        {
            ret[i] = new int[GridSize];
            
            for(int j = 0; j < GridSize; j++)
            {
                ret[i][j] = input[i, j];
            }
        }
        return ret;
    }
}
