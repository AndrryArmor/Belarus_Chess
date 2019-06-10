using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static BelarusChess.MainWindow;

namespace BelarusChess
{
    public class Knight : Figure
    {
        public Knight(Image image, MainWindow.PlayerColor color)
        {
            Image = image;
            Color = color;
            Type = MainWindow.FigureType.Knight;
        }
        public Knight() { }
        public override Moves[,] Moves()
        {
            Moves[,] moves = new Moves[8, 1];
            // Up-left
            moves[0, 0] = new Moves(-1, -2);
            // Left-up
            moves[1, 0] = new Moves(-2, -1);
            // Left-down
            moves[2, 0] = new Moves(-2, 1);
            // Down-left
            moves[3, 0] = new Moves(-1, 2);
            // Down-right
            moves[4, 0] = new Moves(1, 2);
            // Right-down
            moves[5, 0] = new Moves(2, 1);
            // Right-up
            moves[6, 0] = new Moves(2, -1);
            // Up-right
            moves[7, 0] = new Moves(1, -2);

            return moves;
        }
    }
}
