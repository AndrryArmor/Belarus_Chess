using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BelarusChess
{
    public class Queen : Figure
    {
        public Queen(Image image, Chess.PlayerColor color)
        {
            Image = image;
            Color = color;
            Type = Chess.FigureType.Queen;
        }
        public override Moves[,] Moves()
        {
            Moves[,] moves = new Moves[8, 8];
            for (int i = 0; i < 8; i++)
            {
                // Up
                moves[0, i] = new Moves(0, -(i + 1));
                // Up-left
                moves[1, i] = new Moves(-(i + 1), -(i + 1));
                // Left
                moves[2, i] = new Moves(-(i + 1), 0);
                // Down-left
                moves[3, i] = new Moves(-(i + 1), i + 1);
                // Down
                moves[4, i] = new Moves(0, i + 1);
                // Down-right
                moves[5, i] = new Moves(i + 1, i + 1);
                // Right
                moves[6, i] = new Moves(i + 1, 0);
                // Up-right
                moves[7, i] = new Moves(i + 1, -(i + 1));
            }
            return moves;
        }
    }
}
