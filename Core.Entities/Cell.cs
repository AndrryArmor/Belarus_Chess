using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelarusChess.Core.Entities
{
    public class Cell
    {
        public Cell(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; }
        public int Col { get; }
        public bool IsValid => (Row >= 0 && Row <= 8 && Col >= 0 && Col <= 8);

        public override bool Equals(object obj)
        {
            return obj is Cell cell &&
                   Row == cell.Row &&
                   Col == cell.Col;
        }

        public static bool operator ==(Cell left, Cell right)
        {
            return EqualityComparer<Cell>.Default.Equals(left, right);
        }

        public static bool operator !=(Cell left, Cell right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            int hashCode = 1084646500;
            hashCode = hashCode * -1521134295 + Row.GetHashCode();
            hashCode = hashCode * -1521134295 + Col.GetHashCode();
            return hashCode;
        }
    }
}
