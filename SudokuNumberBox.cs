namespace Sudoku
{
    enum SudokuNumber { EMPTY = 0, ONE = 1, TWO = 2, THREE = 3, FOUR = 4, FIVE = 5, SIX = 6, SEVEN = 7, EIGHT = 8, NINE = 9 }

    class SudokuNumberBox
    {
        public static SudokuNumber[] RealSudokuNumbers
        {
            get
            {
                return new SudokuNumber[] { (SudokuNumber)1, (SudokuNumber)2, (SudokuNumber)3, (SudokuNumber)4, (SudokuNumber)5, (SudokuNumber)6, (SudokuNumber)7, (SudokuNumber)8, (SudokuNumber)9 };
            }
        }

        public SudokuNumber Number { get; set; }

        public SudokuNumberBox()
        {
            Number = SudokuNumber.EMPTY;
        }

        public SudokuNumberBox(SudokuNumber number)
        {
            Number = number;
        }
    }
}