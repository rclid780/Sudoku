namespace Sudoku;

public static class Solver
{ 
    // GridSize is the size of the 2D matrix
    static readonly int GridSize = 9; //TODO allow more sizes

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
        List<int[,]> solutions = [];
        Solve((int[,])grid.Clone(), 0, 0, maxSolutions, ref solutions);
        return solutions;
    }

    private static bool Solve(int[,] grid, int row, int col, int maxSolutions, ref List<int[,]> solutions)
    {
        /*
            if we have reached the 8th
            row and 9th column (0 indexed matrix) ,
            we are returning true to avoid further backtracking
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
}
