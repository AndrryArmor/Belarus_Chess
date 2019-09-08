using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BelarusChess
{
    class Chessboard : ICloneable
    {
        private readonly Figure[,] _chessBoard;

        public Chessboard()
        {
            _chessBoard = new Figure[9, 9];
        }

        // Indexator
        public Figure this [Cell cell]
        {
            get
            {
                if (cell.Row < 0 || cell.Row > 8 || cell.Col < 0 || cell.Col > 8)
                    return null;
                return _chessBoard[cell.Row, cell.Col];
            }
            set
            {
                if (cell.Row < 0 || cell.Row > 8 || cell.Col < 0 || cell.Col > 8)
                    return;
                _chessBoard[cell.Row, cell.Col] = value;
            }
        }

        public object Clone()
        {
            return _chessBoard.Clone();
        }
    }
}
