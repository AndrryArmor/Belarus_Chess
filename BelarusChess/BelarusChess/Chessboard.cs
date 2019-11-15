using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BelarusChess
{
    public class Chessboard : ICloneable
    {
        private readonly Figure[,] startBoard;

        public Figure[,] Board { get; private set; }
        public int Length { get => Board.GetLength(0); }

        public Chessboard(Figure[,] chessboard)
        {
            startBoard = chessboard;
            Reset();
        }

        public Figure this [Cell cell]
        {
            get
            {
                if (cell == null)
                    throw new ArgumentNullException("Invalid cell coordinates");
                return Board[cell.Row, cell.Col];
            }
            set
            {
                if (cell == null)
                    throw new ArgumentNullException("Invalid cell coordinates");
                if (value != null)
                    value.Cell = Cell.Create(cell.Row, cell.Col);
                Board[cell.Row, cell.Col] = value;
            }
        }

        public void Reset()
        {
            Board = startBoard;
        }

        public object Clone()
        {
            return new Chessboard(Board);
        }
    }
}
