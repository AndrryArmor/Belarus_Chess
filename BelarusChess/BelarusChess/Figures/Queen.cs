using System.Windows.Controls;

namespace BelarusChess.Figures
{
    public class Queen : Figure
    {
        public override Move[,] Moves { get => QueenMoves(); }

        public Queen(Image image, PlayerColor color, FigureType type, Cell cell) : base(image, color, FigureType.Queen, cell) { }

        private static Move[,] QueenMoves()
        {
            Move[,] legalMoves = new Move[8, 8];
            for (int i = 0; i < 8; i++)
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
