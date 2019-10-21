using System.Windows.Controls;

namespace BelarusChess
{
    public class Bishop : Figure
    {
        public override Move[,] Moves { get => BishopMoves(); }

        public Bishop(Image image, PlayerColor color, FigureType type, Cell cell) : base(image, color, FigureType.Bishop, cell) { }

        private static Move[,] BishopMoves()
        {
            Move[,] legalMoves = new Move[4, 8];
            for (int i = 0; i < 8; i++)
            {
                // Up-left
                legalMoves[0, i] = new Move(-(i + 1), -(i + 1));
                // Down-left
                legalMoves[1, i] = new Move(i + 1, -(i + 1));
                // Down-right
                legalMoves[2, i] = new Move(i + 1, i + 1);
                // Up-right
                legalMoves[3, i] = new Move(-(i + 1), i + 1);
            }
            return legalMoves;
        }
    }
}
