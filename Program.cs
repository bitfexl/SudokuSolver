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
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine(_leftSpacing + "1. Solve Sudoku");
                Console.WriteLine(_leftSpacing + "2. Generate 15 Sudokus");
                Console.WriteLine(_leftSpacing + "3. Generate (and solve) 5 Sudokus");
                Console.WriteLine(_leftSpacing + "4. Exit");
                Console.Write("\n" + _leftSpacing + "> ");

                string input = Console.ReadLine();
                Console.Clear();
                switch (input)
                {
                    case "1": solveSudokuSlow(SudokuGenerator.Replace(SudokuGenerator.Generate(), _replacementValues[1])); break;
                    case "2": printSudokus(15, false); break;
                    case "3": printSudokus(5, true); break;
                    case "4": return;
                    default: continue;
                }

                Console.Write(_leftSpacing + "Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void printSudokus(int count, bool solve)
        {
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                int replacementValue = _replacementValues[random.Next(0, 3)];

                SudokuGrid solvedGeneratedGrid = SudokuGenerator.Generate();
                SudokuGrid generatedGrid = SudokuGenerator.Replace(solvedGeneratedGrid, replacementValue);
                SudokuSolver solver = new SudokuSolver(generatedGrid);

                Console.WriteLine("\n" + _leftSpacing + "Replacement: " + replacementValue + "%");
                sudokuPrinter(generatedGrid);

                if (solve)
                {
                    solvedGeneratedGrid = solver.Solve();
                }

                Console.WriteLine("\n" + _leftSpacing + "Solved: " + SudokuValidator.Validate(solvedGeneratedGrid));
                sudokuPrinter(solvedGeneratedGrid);
                Console.WriteLine("\n\n");
            }
        }

        private static void solveSudokuSlow(SudokuGrid grid)
        {
            sudokuPrinter(grid);
            Console.Write("\n" + _leftSpacing + "Press any key to start...");
            Console.ReadKey();
            Console.Clear();

            SudokuSolver solver = new SudokuSolver(grid);
            SudokuGrid solvedGrid = solver.Solve(solveSudokuSlowPrinterCb);
            Console.WriteLine("\n" + _leftSpacing + "Solved: " + SudokuValidator.Validate(solvedGrid));
        }

        private static void solveSudokuSlowPrinterCb(SudokuGrid grid)
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            sudokuPrinter(grid);
        }

        private static void sudokuPrinter(SudokuGrid grid)
        {
            SudokuPrinter printer = new SudokuPrinter(grid);
            printer.LeftSpacing = _leftSpacing;
            printer.PrintEmptyAsZero = false;
            string print = printer.SmallPrint();
            Console.Write(print);
        }
    }
}