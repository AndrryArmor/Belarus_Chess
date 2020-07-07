using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Cell : ICloneable
    {
        public Cell(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; }
        public int Col { get; }
        public bool IsValid { get => (Row >= 0 && Row <= 8 && Col >= 0 && Col <= 8); }

        public object Clone()
        {
            return new Cell(Row, Col);
        }
    }
}
