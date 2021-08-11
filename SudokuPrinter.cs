namespace Sudoku
{
    class SudokuPrinter
    {
        SudokuGrid _grid;

        public string LeftSpacing { get; set; }

        public bool PrintEmptyAsZero { get; set; }

        public SudokuPrinter(SudokuGrid grid)
        {
            _grid = grid;
            LeftSpacing = "";
            PrintEmptyAsZero = true;
        }

        public string SmallPrint()
        {
            string print = "";
            for (int i = 0; i < _grid.Grid.GetLength(0); i++)
            {
                string spacingLine = LeftSpacing;
                print += LeftSpacing;
                for (int j = 0; j < _grid.Grid.GetLength(1); j++)
                {
                    if (j % 3 == 0)
                    {
                        print += "|";
                        spacingLine += "+";
                    }

                    string number = ((int)_grid.Grid[i, j].Number).ToString();
                    if (!PrintEmptyAsZero && number == "0")
                    {
                        number = " ";
                    }

                    print += " " + number + " ";
                    spacingLine += "---";
                    if (j == 8)
                    {
                        print += "|\n";
                        spacingLine += "+\n";
                    }
                }
                if (i == 0)
                {
                    print = spacingLine + print;
                }
                else if (i % 3 == 2)
                {
                    print += spacingLine;
                }
            }
            return print;
        }
    }
}