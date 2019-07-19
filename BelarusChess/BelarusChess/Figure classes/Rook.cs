using System.Windows.Controls;

namespace BelarusChess
{
    public class Rook : Figure
    {
        public Rook(Image image, PlayerColor color)
        {
            Image = image;
            Color = color;
            Type = FigureType.Rook;
        }
        public Rook() { }
        /// <summary> Returns all legal moves for rook </summary>
        public override Moves[,] Moves()
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
    }
}
