using System;
using System.Globalization;
using Sudoku;

namespace SudokuLibraryTest;

[TestClass]
public class SolverTest
{
    [TestMethod]
    public void SolveTest()
    {
        int[][] input = [
            [0, 0, 5, 0, 2, 0, 0, 1, 9],
            [2, 0, 4, 0, 0, 3, 0, 0, 8],
            [0, 0, 0, 0, 0, 0, 0, 4, 7],
            [0, 0, 2, 5, 0, 0, 0, 3, 4],
            [0, 8, 0, 0, 0, 6, 1, 7, 2],
            [1, 0, 0, 0, 0, 0, 8, 0, 5],
            [0, 0, 8, 4, 0, 9, 0, 0, 0],
            [0, 0, 7, 0, 1, 0, 5, 0, 0],
            [0, 0, 1, 0, 7, 0, 4, 0, 0]
        ];
        int[][] expected = [
            [8, 7, 5, 6, 2, 4, 3, 1, 9],
            [2, 1, 4, 7, 9, 3, 6, 5, 8],
            [9, 3, 6, 1, 5, 8, 2, 4, 7],
            [7, 6, 2, 5, 8, 1, 9, 3, 4],
            [5, 8, 3, 9, 4, 6, 1, 7, 2],
            [1, 4, 9, 2, 3, 7, 8, 6, 5],
            [3, 5, 8, 4, 6, 9, 7, 2, 1],
            [4, 9, 7, 3, 1, 2, 5, 8, 6],
            [6, 2, 1, 8, 7, 5, 4, 9, 3]
        ];
  
        List<int[][]> solutions = Solver.Solve(input, 2);
        Assert.AreEqual(solutions.Count, 1);

        int[][] actual = solutions[0];
        Assert.AreEqual(expected.Length, actual.Length);

        for(int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i].Length, actual[i].Length);
            for(int j = 0; j < expected[i].Length; j++){
                Assert.AreEqual(expected[i][j], actual[i][j]);
            }
        }
    }

    [TestMethod]
    public void SolveInternal()
    {
        int[,] input = {
            {0, 0, 5, 0, 2, 0, 0, 1, 9},
            {2, 0, 4, 0, 0, 3, 0, 0, 8},
            {0, 0, 0, 0, 0, 0, 0, 4, 7},
            {0, 0, 2, 5, 0, 0, 0, 3, 4},
            {0, 8, 0, 0, 0, 6, 1, 7, 2},
            {1, 0, 0, 0, 0, 0, 8, 0, 5},
            {0, 0, 8, 4, 0, 9, 0, 0, 0},
            {0, 0, 7, 0, 1, 0, 5, 0, 0},
            {0, 0, 1, 0, 7, 0, 4, 0, 0}
        };
        int[,] expected = {
            {8, 7, 5, 6, 2, 4, 3, 1, 9},
            {2, 1, 4, 7, 9, 3, 6, 5, 8},
            {9, 3, 6, 1, 5, 8, 2, 4, 7},
            {7, 6, 2, 5, 8, 1, 9, 3, 4},
            {5, 8, 3, 9, 4, 6, 1, 7, 2},
            {1, 4, 9, 2, 3, 7, 8, 6, 5},
            {3, 5, 8, 4, 6, 9, 7, 2, 1},
            {4, 9, 7, 3, 1, 2, 5, 8, 6},
            {6, 2, 1, 8, 7, 5, 4, 9, 3}
        };
  
        List<int[,]> solutions = Solver.Solve(input, 2);
        Assert.AreEqual(solutions.Count, 1);

        int[,] actual = solutions[0];
        Assert.AreEqual(expected.GetLength(0), actual.GetLength(0));
        Assert.AreEqual(expected.GetLength(1), actual.GetLength(1));
        for(int i = 0; i < expected.GetLength(0); i++)
        {
            for(int j = 0; j < expected.GetLength(1); j++){
                Assert.AreEqual(expected[i,j], actual[i,j]);
            }
        }
    }
}
