using System.Windows.Controls;

namespace BelarusChess
{
    public class King : Figure
    {
        public King(Image image, PlayerColor color)
        {
            Image = image;
            Color = color;
            Type = FigureType.King;
        }
        public King() { }
        /// <summary> Returns all legal moves for king </summary>
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
    }
}
