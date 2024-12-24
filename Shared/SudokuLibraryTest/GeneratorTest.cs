using Sudoku;

namespace SudokuLibraryTest;

[TestClass]
public class GeneratorTest
{
    [TestMethod]
    public void GenorateTest()
    {
        int[][] puzzle = Generator.Generate();
        Assert.AreEqual(puzzle.Length, 9);
        int emptyCount = 0;
        foreach(int[] row in puzzle)
        {
            Assert.AreEqual(row.Length, 9);
            foreach(int value in row)
            {
                if(value == 0)
                {
                    emptyCount++;
                }
            }
        }
        Assert.IsTrue(emptyCount >= 50);
    }
}