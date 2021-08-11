namespace Sudoku
{
    class SudokuValidator
    {
        //private static int _sudokusum = 45;

        public static bool Validate(SudokuNumberBox[,] box, bool ignoreEmptyFields = false) // only 3x3
        {
            SudokuNumberBox[] onedimensionalbox = new SudokuNumberBox[9];
            for (int i = 0; i < box.GetLength(0); i++)
            {
                for (int j = 0; j < box.GetLength(1); j++)
                {
                    onedimensionalbox[i * 3 + j] = box[i, j];
                }
            }
            return Validate(onedimensionalbox, ignoreEmptyFields);
        }

        public static bool Validate(SudokuNumberBox[] box, bool ignoreEmptyFields = false)
        {
            foreach (SudokuNumber num in SudokuNumberBox.RealSudokuNumbers)
            {
                int count = 0;
                foreach (SudokuNumberBox usednum in box)
                {
                    if (!ignoreEmptyFields && usednum.Number == SudokuNumber.EMPTY)
                    {
                        return false;
                    }
                    if (usednum.Number == num)
                    {
                        count++;
                    }
                    if (count > 1)
                    {
                        return false;
                    }
                }
            }
            return true;

            /*int sum = _sudokusum;
            foreach (SudokuNumberBox nmbbox in box)
            {
                if (nmbbox.Number == SudokuNumber.EMPTY)
                {
                    return false;
                }
                sum -= (int)nmbbox.Number;
            }
            return sum == 0;*/
        }

        public static bool Validate(SudokuGrid sudoku, bool ignoreEmptyFields = false)
        {
            for (int i = 0; i < 9; i++)
            {
                bool sqvalid = Validate(sudoku.GetSquare(i), ignoreEmptyFields);
                bool rvalid = Validate(sudoku.GetRow(i), ignoreEmptyFields);
                bool cvalid = Validate(sudoku.GetCol(i), ignoreEmptyFields);
                if (!(sqvalid && rvalid && cvalid))
                {
                    return false;
                }
            }
            return true;
        }

        SudokuGrid _grid;

        public SudokuValidator(SudokuGrid grid)
        {
            _grid = grid;
        }

        public bool ValidateRow(int row, bool ignoreEmptyFields = false)
        {
            return Validate(_grid.GetRow(row), ignoreEmptyFields);
        }

        public bool ValidateCol(int col, bool ignoreEmptyFields = false)
        {
            return Validate(_grid.GetCol(col), ignoreEmptyFields);
        }

        public bool ValidateSquare(int sq, bool ignoreEmptyFields = false)
        {
            return Validate(_grid.GetSquare(sq), ignoreEmptyFields);
        }

        public bool Validate(bool ignoreEmptyFields = false)
        {
            return Validate(_grid, ignoreEmptyFields);
        }
    }
}