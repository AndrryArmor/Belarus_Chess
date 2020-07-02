using System;

namespace BelarusChess
{
    /// <summary> 
    /// A pair of coordinates (row, col) for board {numeration starts with left top corner}
    /// </summary>
    public class Cell : ICloneable
    {
        public int Row { get; }
        public int Col { get; }

        private Cell(int row, int col)
        {
            Row = row;
            Col = col;
        }

        /// <summary>
        /// Creator that uses private constructor. Returns null, if coordinates are not valid.
        /// </summary>
        public static Cell Create(int row, int col)
        {
            return (row < 0 || row > 8 || col < 0 || col > 8) ? null : new Cell(row, col);
        }

        public object Clone()
        {
            return new Cell(Row, Col);
        }

        public static bool operator ==(Cell left, Cell right)
        {
            if (left?.Row == right?.Row && left?.Col == right?.Col)
                return true;
            else return false;
        }

        public static bool operator !=(Cell left, Cell right)
        {
            if (left?.Row == right?.Row && left?.Col == right?.Col)
                return false;
            else return true;
        }
    }
}
