using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BelarusChess
{
    public class King : Figure
    {
        public King(Image image, Chess.PlayerColor color)
        {
            Image = image;
            Color = color;
            Type = Chess.FigureType.King;
        }
        public override Moves[,] Moves()
        {
            Moves[,] moves = new Moves[8, 1];
            // Up
            moves[0, 0] = new Moves(0, -1);
            // Up-left
            moves[1, 0] = new Moves(-1, -1);
            // Left
            moves[2, 0] = new Moves(-1, 0);
            // Down-left
            moves[3, 0] = new Moves(-1, 1);
            // Down
            moves[4, 0] = new Moves(0, 1);
            // Down-right
            moves[5, 0] = new Moves(1, 1);
            // Right
            moves[6, 0] = new Moves(1, 0);
            // Up-right
            moves[7, 0] = new Moves(1, -1);

            return moves;
        }
        public Moves[,] attackDirections()
        {
            Moves[,] moves = new Moves[16, 8];
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
            Knight knight = new Knight();
            Moves[,] knightMoves = knight.Moves();
            for (int i = 0; i < 8; i++)
            {
                moves[i + 8, 0] = knightMoves[i, 0];
            }
            return moves;
        }
    }
}
