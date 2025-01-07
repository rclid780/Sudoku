namespace Sudoku;

public static class Solver
{ 
    // GridSize is the size of the 2D matrix
    static readonly int GridSize = 9;

    /*
        Takes a partially filled-in grid and attempts
        to assign values to all unassigned locations in
        such a way to meet the requirements for
        Sudoku solution (non-duplication across rows,
        columns, and boxes)
    */
    public static List<int[][]> Solve(int[][] puzzle, int maxSolutions)
    {
        int[,] grid = ArrayManipulation.CreateSquareArray(puzzle);
        List<int[,]> solutionsTemp = Solve(grid, maxSolutions);
        List<int[][]> solutions = [];
        foreach(int[,] solution in solutionsTemp)
        {
            solutions.Add(ArrayManipulation.CreateJaggedArray(solution));
        }
        return solutions;
    }

    internal static List<int[,]> Solve(int[,] grid, int maxSolutions)
    {
        if(!ValidateBoard(grid))
        {
            return [];
        }
        List<int[,]> solutions = [];
        Solve((int[,])grid.Clone(), 0, 0, maxSolutions, ref solutions);
        return solutions;
    }

    private static bool Solve(int[,] grid, int row, int col, int maxSolutions, ref List<int[,]> solutions)
    {
        /*
            if we have reached the 8th
            row and 9th column (0 indexed matrix) ,
            we found a solution 
            return true to avoid further backtracking
            return false to backtrack and find another solution
        */
        if (row == GridSize - 1 && col == GridSize)
        {
            solutions.Add((int[,])grid.Clone());

            if(solutions.Count < maxSolutions)
            {
                return false;
            }
            else
            {
                //stop searching for solutions
                return true;
            }
        }

        // Check if column value  becomes 9 ,
        // we move to next row and column start from 0
        if (col == GridSize)
        {
            row++;
            col = 0;
        }

        // Check if the current position of the grid already
        // contains value >0, we iterate for next column
        if (grid[row, col] != 0)
        {
            return Solve(grid, row, col + 1, maxSolutions, ref solutions);
        }

        for (int num = 1; num < 10; num++)
        {

            // Check if it is safe to place the num (1-9)  in the
            // given row ,col ->we move to next column
            if (IsSafe(grid, row, col, num))
            {

                /*  
                    assigning the num in the current
                    (row,col)  position of the grid and
                    assuming our assigned num in the position
                    is correct
                */
                grid[row, col] = num;

                // Checking for next possibility with next column
                if (Solve(grid, row, col + 1, maxSolutions, ref solutions))
                {
                    return true;
                }
            }
            /*
                removing the assigned num , since our
                assumption was wrong , and we go for next
                assumption with diff num value
            */
            grid[row, col] = 0;
        }
        return false;
    }
    
    public static bool IsSafe(int[][] puzzle, int row, int col, int num)
    {
        int[,] grid = ArrayManipulation.CreateSquareArray(puzzle);
        return IsSafe(grid, row, col, num);
    }

    // Check whether it will be legal to assign num to the given row, col
    internal static bool IsSafe(int[,] grid, int row, int col, int num)
    {

        // Check if we find the same num in the similar row , we return false
        for (int x = 0; x <= 8; x++)
        {
            if (grid[row, x] == num)
            {
                return false;
            }
        }

        // Check if we find the same num in the similar column , we return false
        for (int x = 0; x <= 8; x++)
        {
            if (grid[x, col] == num)
            {
                return false;
            }
        }

        // Check if we find the same num in the particular 3*3 matrix, we return false
        int startRow = row - row % 3, startCol = col - col % 3;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (grid[i + startRow, j + startCol] == num)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static bool ValidateBoard(int[][] puzzle)
    {
        int[,] grid = ArrayManipulation.CreateSquareArray(puzzle);
        return ValidateBoard(grid);
    }

    internal static bool ValidateBoard(int[,] grid)
    {
        //we can only reasonable nest loops like this because we know the grid is small
        //a better solution would need to be used if the board was very large
        // since O^3 scales very poorly ... 

        //Check all rows are valid
        for(int i = 0; i < GridSize; i++) //for each row, col, cage
        {
            for(int j = 0; j < grid.GetLength(1) - 1; j++)
            {
                for(int k = j + 1; k < grid.GetLength(1); k++)
                {
                    //we can ignore zero is a blank square not a "value"
                    if(grid[i,j] != 0 && grid[i,j] == grid[i,k])
                    {
                        //a number appears twice in row[i] this is unsolveable
                        return false;
                    }
                    if(grid[j,i] != 0 && grid[j,i] == grid[k,i])
                    {
                        //a number appears twice in col[i] this is unsolvable
                        return false;
                    }
                    int RowOffset = i / 3 * 3;
                    int Coloffset = i % 3 * 3;
                    int square1row = j / 3;
                    int square1col = j % 3;
                    int square2row = k / 3;
                    int square2col = k % 3;
                    if(grid[square1row + RowOffset, square1col + Coloffset] != 0 
                        && grid[square1row + RowOffset, square1col + Coloffset]
                        == grid[square2row + RowOffset, square2col + Coloffset]
                    )
                    {
                        //a number appears twice in a sub grid (cage) this is unsolvable
                        return false;
                    }
                }
            }
        }
        //no duplication detected probably solvable ...
        return true;
    }
}
