namespace Sudoku;

public class Generator
{
    private static readonly int GridSize = 9;

    delegate void SwapDelegate(int[,] puzzle, int first, int second);

    public static int[][] Generate()
    {
        int [,] solution = GenerateBase();
  
        SwapDelegate rows = SwapRows;
        PermuteArrays(rows, solution);
        PermuteBlocks(rows, solution);

        SwapDelegate columns = SwapColumns;
        PermuteArrays(columns, solution);
        PermuteBlocks(columns, solution);

        int[,] puzzle = Mask(solution);

        
        return ArrayManipulation.CreateJaggedArray(puzzle);
    }

    private static int[,] GenerateBase()
    {
        int[,] blank = {
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0}
        };

        Random random = new();
        /*
            This needs to be less random on occasion the ransomness of this process can
            cause a significant delay >30 sec
            for this part we have to 2 checks one:
            1) a quick validation that the puzzle is valid (but maybe not solvable)
            2) a solve check
            The validation check can't be skipped because the backtracking algo can take a very
            log time to verify there is no solution in cases tha validity check catches quickly
        */
        int fillCount = 0;
        while(fillCount < 31)
        {
            int row = random.Next(0, GridSize - 1);
            int col = random.Next(0, GridSize - 1);
            int value = random.Next(1, 9);
            if(blank[row, col] != 0)
            {
                //we already have a value for this square
                continue;
            }
            if(Solver.IsSafe(blank, row, col, value))
            {
                blank[row, col] = value;
                if(Solver.Solve(blank, 1).Count == 0)
                {
                    blank[row, col] = 0;
                }
                else
                {
                    fillCount++;
                }
            }
        }
        //We know we have at least one solution return the first one
        return Solver.Solve(blank, 1)[0];
    }

    private static void PermuteArrays(SwapDelegate PremuteItem, int[,] puzzle)
    {
        for (int block = 0; block < 3; block++)
        {
            int blockOffset = block * 3;
            List<(int, int)> swaps = GenerateSwapList();
            foreach ((int, int) swap in swaps)
            {
                PremuteItem(puzzle, blockOffset + swap.Item1, blockOffset + swap.Item2);
            }
        }
    }

    private static void PermuteBlocks(SwapDelegate PremuteItem, int[,] puzzle)
    {
        //We've made the decision to combine rows and columns to reduce repeated code
        //but we could have instead choosen to pass more information to swapRows in the case of
        //row swaps the buffer being used can copy an entire row block to a temp array and back
        //instead we call swap row 3 times to reduce code overlap with swap column which can't use
        //buffer swapping and has to use looping

        List<(int, int)> swaps = GenerateSwapList();
        foreach ((int, int) swap in swaps)
        {
            for (int rowInBlock = 0; rowInBlock < 3; rowInBlock++)
            {
                PremuteItem(puzzle, rowInBlock + (swap.Item1 * 3), rowInBlock + (swap.Item2 * 3));
            }
        }
    }

    private static void SwapRows(int[,] puzzle, int firstRow, int secondRow)
    {
        //we could simply do swaps using tuples if we were using int[][] but most of the test of the program is
        //faster and simplier using int[,] so this section is slightly more complicated we will
        //instead use Buffer.BlockCopy to avoid moving one element at a time and gain back some of the
        //speed a touple swap would have provided us over an element by element swap

        //We need to calculate how long an row of data we are going to swap is
        //it will be dataType size * number of elements so 
        int  rowSize = sizeof(int) * GridSize;

        //We can also calculate the start of each block we are going to move
        int firstRowStart = rowSize * firstRow;
        int secondRowStart = rowSize * secondRow;

        // Temporary array for an intermediate step in the swap operation.
        var temp = new int[GridSize];

        // Copy first row into a temporary array.
        Buffer.BlockCopy(puzzle, firstRowStart, temp, 0, rowSize);
        // Copy second row into the first row.
        Buffer.BlockCopy(puzzle, secondRowStart, puzzle, firstRowStart, rowSize);
        // Copy temporary array into the second row.
        Buffer.BlockCopy(temp, 0, puzzle, secondRowStart, rowSize);
    }

    private static void SwapColumns(int[,] puzzle, int firstColumn, int secondColumn)
    {
        for (int row = 0; row < GridSize; row++)
        {
            (puzzle[row,secondColumn], puzzle[row,firstColumn]) = (puzzle[row,firstColumn], puzzle[row,secondColumn]);
        }
    }

    private static int[,] Mask(int[,] puzzle)
    {
        int maskCount = 0;

        Random random = new();
        //we can safely mask 50 squares but we have to make sure it's still solvable
        while (maskCount < 50)
        {
            int column = random.Next(0, GridSize);
            int row = random.Next(0, GridSize);
            if (puzzle[row, column] > 0)
            {
                int temp = puzzle[row, column];
                puzzle[row, column] = 0;

                List<int[,]> solutions = Solver.Solve(puzzle, 2);
                bool solvable = solutions.Count > 0;
                bool unique = solutions.Count < 2;

                if (solvable && unique)
                {
                    maskCount++;
                }
                else
                {
                    puzzle[row, column] = temp;
                }
            }
        }

        return puzzle;
    }

    private static List<(int, int)> GenerateSwapList()
    {
        /*
            there are 6 possible permutation of rows/columns/rowBlock/ColumnBlocks
            we can generate a random number to pick one of the possible permutations
            then return a list<(int, int)> respresenting the swaps to do
            1 = 123
            2 = 132
            3 = 213
            4 = 231
            5 = 312
            6 = 321
        */
        Random random = new();
        return random.Next(1, 6) switch
        {
            1 => [],
            2 => [(1, 2)],
            3 => [(0, 1)],
            4 => [(0, 1), (1, 2)],
            5 => [(1, 2), (0, 1)],
            6 => [(0, 2)],
            _ => [],//This can't happen but it makes the compiler happy
        };
    }
}
