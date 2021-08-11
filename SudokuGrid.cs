using System;

namespace Sudoku
{
    class SudokuGrid
    {
        SudokuNumberBox[,] _grid;

        public SudokuNumberBox[,] Grid { get { return _grid; } }

        public SudokuGrid()
        {
            _grid = new SudokuNumberBox[9, 9];
            fill(_grid);
        }

        public SudokuGrid(SudokuGrid origin) : this()
        {
            origin.CopyTo(this);
        }

        public SudokuNumberBox[] GetRow(int row)
        {
            return getRowOrCol(row, true);
        }

        public SudokuNumberBox[] GetCol(int col)
        {
            return getRowOrCol(col, false);
        }

        public SudokuNumberBox[,] GetSquare(int sq)
        {
            if (sq < 0 || sq >= 9)
            {
                throw new IndexOutOfRangeException();
            }

            return getSquare(sq / 3 * 3, sq % 3 * 3);
        }

        public void CopyTo(SudokuGrid target)
        {
            for (int i = 0; i < _grid.GetLength(0); i++)
            {
                for (int j = 0; j < _grid.GetLength(1); j++)
                {
                    target.Grid[i, j].Number = _grid[i, j].Number;
                }
            }
        }

        private SudokuNumberBox[,] getSquare(int row, int col)
        {
            SudokuNumberBox[,] box = new SudokuNumberBox[3, 3];
            for (int rowoffset = 0; rowoffset < 3; rowoffset++)
            {
                for (int coloffset = 0; coloffset < 3; coloffset++)
                {
                    box[rowoffset, coloffset] = _grid[row + rowoffset, col + coloffset];
                }
            }
            return box;
        }

        private SudokuNumberBox[] getRowOrCol(int rc, bool isRow)
        {
            if (rc < 0 || rc >= 9)
            {
                throw new IndexOutOfRangeException();
            }

            SudokuNumberBox[] s = new SudokuNumberBox[9];
            for (int i = 0; i < 9; i++)
            {
                if (isRow)
                {
                    s[i] = _grid[rc, i];
                }
                else
                {
                    s[i] = _grid[i, rc];
                }
            }
            return s;
        }

        private void fill(SudokuNumberBox[,] box)
        {
            for (int i = 0; i < box.GetLength(0); i++)
            {
                for (int j = 0; j < box.GetLength(1); j++)
                {
                    box[i, j] = new SudokuNumberBox();
                }
            }
        }
    }
}