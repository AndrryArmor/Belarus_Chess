using System;

namespace BelarusChess
{
    /// <summary> Describes the chess cell as a pair of numbers </summary>
    public struct Cell
    {
        private int row;
        private int col;
        public int Row
        {
            get => row;
            set
            {
                if (value >= 0 && value < 9)
                    row = value;
                else
                    throw new ArgumentException("Row must be a non-negative number");
            }
        }
        public int Col
        {
            get => col;
            set
            {
                if (value >= 0)
                    col = value;
                else
                    throw new ArgumentException("Col must be a non-negative number");
            }
        }

        public Cell(int row, int col) : this()
        {
            Row = row;
            Col = col;
        }
    }
}
