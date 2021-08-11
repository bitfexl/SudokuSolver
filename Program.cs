/*
    Sudoku generator
*/

using System;
using Sudoku;

namespace test
{
    class Program
    {
        private const string _leftSpacing = "  ";
        private static int[] _replacementValues = { 60, 70, 80 };

        static void Main(string[] args)
        {
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                int replacementValue = _replacementValues[random.Next(0, 3)];

                SudokuGrid solvedGeneratedGrid = SudokuGenerator.Generate();
                SudokuGrid generatedGrid = SudokuGenerator.Replace(solvedGeneratedGrid, replacementValue);

                SudokuPrinter printer = new SudokuPrinter(generatedGrid);
                printer.LeftSpacing = _leftSpacing;
                printer.PrintEmptyAsZero = false;

                SudokuPrinter solvedPrinter = new SudokuPrinter(solvedGeneratedGrid);
                solvedPrinter.LeftSpacing = _leftSpacing;

                Console.WriteLine("\n" + _leftSpacing + "Replacement: " + replacementValue + "%");
                Console.WriteLine(printer.SmallPrint());
                Console.WriteLine(_leftSpacing + "Solved: " + SudokuValidator.Validate(solvedGeneratedGrid));
                Console.WriteLine(solvedPrinter.SmallPrint() + "\n\n");
            }

            /*SudokuGrid grid = new SudokuGrid();
            SudokuPrinter printer = new SudokuPrinter(grid);
            printer.LeftSpacing += _leftSpacing;
            SudokuSolver solver = new SudokuSolver(grid);

            getSudoku(grid);

            string print = printer.SmallPrint();
            Console.WriteLine(print);

            bool isValid = false;
            try
            {
                SudokuGrid solvedGrid = solver.Solve(true);
                isValid = SudokuValidator.Validate(solvedGrid);
            }
            catch (SudokuNotSolvableException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine(_leftSpacing + "Sudoku is solvable: " + isValid + "\n");
            print = printer.SmallPrint();
            Console.Write(print);
            //print = solvedGridPrinter.SmallPrint();
            //Console.WriteLine(print);*/
        }

        private static void getSudoku(SudokuGrid grid)
        {
            SudokuNumberBox[] row4 = grid.GetRow(4);
            SudokuNumberBox[,] box2 = grid.GetSquare(2);
            row4[3].Number = SudokuNumber.THREE;
            row4[6].Number = SudokuNumber.FIVE;
            box2[1, 1].Number = SudokuNumber.SEVEN;
            box2[2, 1].Number = SudokuNumber.FOUR;
        }
    }
}