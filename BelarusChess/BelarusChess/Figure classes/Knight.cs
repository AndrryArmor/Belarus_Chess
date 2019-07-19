using System.Windows.Controls;

namespace BelarusChess
{
    public class Knight : Figure
    {
        public Knight(Image image, PlayerColor color)
        {
            Image = image;
            Color = color;
            Type = FigureType.Knight;
        }
        public Knight() { }
        /// <summary> Returns all legal moves for knight </summary>
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
