using System.Windows.Controls;

namespace BelarusChess
{
    public class Pawn : Figure
    {
        public Pawn(Image image, PlayerColor color)
        {
            Image = image;
            Color = color;
            Type = FigureType.Pawn;
        }
        /// <summary> Returns all legal moves for pawn </summary>
        public override Moves[,] Moves()
        {
            Moves[,] moves = new Moves[3, 2];
            if (Color == PlayerColor.White)
            {
                // Up
                moves[0, 0] = new Moves(-1, 0);
                // Double up
                moves[0, 1] = new Moves(-2, 0);
                // Up-left
                moves[1, 0] = new Moves(-1, -1);
                // Up-right
                moves[2, 0] = new Moves(-1, 1);
            }
            else
            {
                // Up
                moves[0, 0] = new Moves(1, 0);
                // Double up
                moves[0, 1] = new Moves(2, 0);
                // Up-left
                moves[1, 0] = new Moves(1, -1);
                // Up-right
                moves[2, 0] = new Moves(1, 1);
            }
            return moves;
        }
    }
}
