using System;

namespace BelarusChess
{
    /// <summary> Describes the chess cell as a pair of numbers </summary>
    public class Cell
    {
        public int Row { get; }
        public int Col { get; }

        private Cell(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public static Cell Create(int row, int col)
        {
            return (row < 0 || row > 8 || col < 0 || col > 8) ? null : new Cell(row, col);
        }

        public Cell Clone()
        {
            return new Cell(Row, Col);
        }
    }
}
