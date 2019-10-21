using System.Windows.Controls;

namespace BelarusChess
{
    public class Rook : Figure
    {
        public override Move[,] Moves { get => RookMoves(); }

        public Rook(Image image, PlayerColor color, Cell cell) : base(image, color, FigureType.Rook, cell) { }

        private static Move[,] RookMoves()
        {
            Move[,] legalMoves = new Move[4, 8];
            for (int i = 0; i < 8; i++)
            {
                // Up
                legalMoves[0, i] = new Move(-(i + 1), 0);
                // Left
                legalMoves[1, i] = new Move(0, -(i + 1));
                // Down
                legalMoves[2, i] = new Move(i + 1, 0);
                // Right
                legalMoves[3, i] = new Move(0, i + 1);
            }
            return legalMoves;
        }
    }
}
