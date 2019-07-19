using System.Windows.Controls;

namespace BelarusChess
{
    public class Bishop : Figure
    {
        public Bishop(Image image, PlayerColor color)
        {
            Image = image;
            Color = color;
            Type = FigureType.Bishop;
        }
        public Bishop() { }
        /// <summary> Returns all legal moves for bishop </summary>
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
