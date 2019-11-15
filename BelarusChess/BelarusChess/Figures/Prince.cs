using System.Windows.Controls;

namespace BelarusChess.Figures
{
    public class Prince : Figure
    {
        public override Move[,] Moves { get => PrinceMoves(); }

        public Prince(Image image, PlayerColor color, Cell cell) : base(image, color, FigureType.Prince, cell) { }

        private static Move[,] PrinceMoves()
        {
            Move[,] legalMoves = new Move[8, 2];
            for (int i = 0; i < 2; i++)
            {
                // Up
                legalMoves[0, i] = new Move(-(i + 1), 0);
                // Up-left
                legalMoves[1, i] = new Move(-(i + 1), -(i + 1));
                // Left
                legalMoves[2, i] = new Move(0, -(i + 1));
                // Down-left
                legalMoves[3, i] = new Move(i + 1, -(i + 1));
                // Down
                legalMoves[4, i] = new Move(i + 1, 0);
                // Down-right
                legalMoves[5, i] = new Move(i + 1, i + 1);
                // Right
                legalMoves[6, i] = new Move(0, i + 1);
                // Up-right
                legalMoves[7, i] = new Move(-(i + 1), i + 1);
            }
            return legalMoves;
        }
    }
}
