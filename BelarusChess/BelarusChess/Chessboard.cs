using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BelarusChess
{
    public class Chessboard
    {
        private readonly Figure[,] chessBoard;

        public Chessboard()
        {
            chessBoard = new Figure[9, 9];
        }

        public Figure this [Cell cell]
        {
            get
            {
                if (cell.Row < 0 || cell.Row > 8 || cell.Col < 0 || cell.Col > 8)
                    return null;
                return chessBoard[cell.Row, cell.Col];
            }
            set
            {
                if (cell.Row < 0 || cell.Row > 8 || cell.Col < 0 || cell.Col > 8)
                    return;
                chessBoard[cell.Row, cell.Col] = value;
            }
        }
    }
}
