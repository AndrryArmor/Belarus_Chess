using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BelarusChess
{
    public class Bishop : Figure
    {
        public Bishop(Image image, Chess.PlayerColor color)
        {
            Image = image;
            Color = color;
            Type = Chess.FigureType.Bishop;
        }
        public override Moves[,] Moves()
        {
            Moves[,] moves = new Moves[4, 8];
            for (int i = 0; i < 8; i++)
            {
                // Up-left
                moves[0, i] = new Moves(-(i + 1), -(i + 1));
                // Down-left
                moves[1, i] = new Moves(-(i + 1), i + 1);
                // Down-right
                moves[2, i] = new Moves(i + 1, i + 1);
                // Up-right
                moves[3, i] = new Moves(i + 1, -(i + 1));
            }
            return moves;
        }
    }
}
