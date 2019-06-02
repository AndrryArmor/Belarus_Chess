using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BelarusChess
{
    /// <summary>
    /// Describes the translation of a location of the figure
    /// </summary>
    public struct Moves
    {
        public int X;
        public int Y;
        public Moves(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    /// <summary>
    /// Class for determining possible moves of figures
    /// </summary>
    public static class MovesOf
    {
        public static Moves[,] Rook()
        {
            Moves[,] moves = new Moves[4, 8];
            for (int i = 0; i < 8; i++)
            {
                // Up
                moves[0, i] = new Moves(0, -(i + 1));
                // Left
                moves[1, i] = new Moves(-(i + 1), 0);
                // Down
                moves[2, i] = new Moves(0, i + 1);
                // Right
                moves[3, i] = new Moves(i + 1, 0);
            }
            return moves;
        }
        public static Moves[,] Bishop()
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
        public static Moves[,] Knight()
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
        public static Moves[,] Prince()
        {
            Moves[,] moves = new Moves[8, 2];
            for (int i = 0; i < 2; i++)
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
        public static Moves[,] King()
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
        public static Moves[,] Queen()
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
