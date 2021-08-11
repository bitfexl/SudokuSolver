using System;

namespace Sudoku
{
    public class SudokuNotSolvableException : Exception
    {
        public SudokuNotSolvableException() : base("No solution found.") { }

        public SudokuNotSolvableException(string message) : base(message) { }
    }

    class SudokuSolver
    {
        SudokuGrid _grid, _gridCopy;
        SudokuValidator _copyValidator;

        public SudokuSolver(SudokuGrid grid)
        {
            _grid = grid;
            _gridCopy = new SudokuGrid(grid);
            _copyValidator = new SudokuValidator(_gridCopy);
        }

        public SudokuGrid Solve()
        {
            if (!findSolutionFor(0, 0, false, grid => { }))
            {
                throw new SudokuNotSolvableException();
            }
            return _gridCopy;
        }

        public SudokuGrid Solve(Action<SudokuGrid> stepCallback)
        {
            if (!findSolutionFor(0, 0, true, stepCallback))
            {
                throw new SudokuNotSolvableException();
            }
            return _gridCopy;
        }

        public SudokuGrid Solve(bool copyBack)
        {
            Solve();
            copyGrid(copyBack);
            return _gridCopy;
        }

        public SudokuGrid Solve(bool copyBack, Action<SudokuGrid> stepCallback)
        {
            Solve(stepCallback);
            copyGrid(copyBack);
            return _gridCopy;
        }

        private void copyGrid(bool copy)
        {
            if (copy)
            {
                _gridCopy.CopyTo(_grid);
            }
        }

        // callback that gets called on each solution found, only if usecb == true
        private bool findSolutionFor(int row, int col, bool usecb, Action<SudokuGrid> callback)
        {
            int nextrow = row, nextcol = col;
            bool nextSquareAvailable = getNextNumberBox(ref nextrow, ref nextcol);

            foreach (SudokuNumber num in getPossibleNumbers(row, col))
            {
                _gridCopy.Grid[row, col].Number = num;

                // validate ignoring empty fields
                // if the current try is valid && (there are no more numberBoxes || the solution for the next numberBox is valid) return true
                if (_copyValidator.Validate(true))
                {
                    if (usecb)
                    {
                        callback(new SudokuGrid(_gridCopy));
                    }
                    if (!nextSquareAvailable || findSolutionFor(nextrow, nextcol, usecb, callback))
                    {
                        return true;
                    }
                } // else continue with the next possible number
            }
            // no number fits this square -> preveous number must be false or sudoku is not solvable -> return false
            _gridCopy.Grid[row, col].Number = _grid.Grid[row, col].Number;
            return false;
        }

        private SudokuNumber[] getPossibleNumbers(int row, int col)
        {
            SudokuNumber[] numbers;
            if (_grid.Grid[row, col].Number != SudokuNumber.EMPTY)
            {
                numbers = new SudokuNumber[] { _grid.Grid[row, col].Number };
            }
            else
            {
                numbers = SudokuNumberBox.RealSudokuNumbers;
            }
            return numbers;
        }

        private bool getNextNumberBox(ref int row, ref int col)
        {
            col++;
            if (col == 9)
            {
                row++;
                col = 0;
            }
            return row != 9; // return false if [8,8] has been reached
        }
    }
}