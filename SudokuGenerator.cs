using System;
using System.Linq;

namespace Sudoku
{
    static class SudokuGenerator
    {
        public static SudokuGrid Generate()
        {
            SudokuNumber[] randomNumberArray = ShuffleArray(SudokuNumberBox.RealSudokuNumbers);
            SudokuGrid grid = new SudokuGrid();
            SudokuNumberBox[] row0 = grid.GetRow(0);
            for (int i = 0; i < 9; i++)
            {
                row0[i].Number = randomNumberArray[i];
            }
            SudokuSolver solver = new SudokuSolver(grid);
            SudokuGrid solvedGrid = solver.Solve();
            return solvedGrid;
        }

        public static SudokuGrid Replace(SudokuGrid grid, int percent) // percent between 0 and 100
        {
            Random random = new Random();
            SudokuGrid newGrid = new SudokuGrid(grid);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (percent >= random.Next(0, 101))
                    {
                        newGrid.Grid[i, j].Number = SudokuNumber.EMPTY;
                    }
                }
            }
            return newGrid;
        }

        private static SudokuNumber[] ShuffleArray(SudokuNumber[] array)
        {
            Random random = new Random();
            return array.OrderBy(x => random.Next()).ToArray();
        }
    }
}