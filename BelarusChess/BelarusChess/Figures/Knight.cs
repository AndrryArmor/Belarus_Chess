using System.Windows.Controls;

namespace BelarusChess
{
    public class Knight : Figure
    {
        public override Move[,] Moves { get => KnightMoves(); }

        public Knight(Image image, PlayerColor color, FigureType type, Cell cell) : base(image, color, FigureType.Knight, cell) { }

        private static Move[,] KnightMoves()
        {
            Move[,] legalMoves = new Move[8, 1];

            // Up-left
            legalMoves[0, 0] = new Move(-2, -1);
            // Left-up
            legalMoves[1, 0] = new Move(-1, -2);
            // Left-down
            legalMoves[2, 0] = new Move(1, -2);
            // Down-left
            legalMoves[3, 0] = new Move(2, -1);
            // Down-right
            legalMoves[4, 0] = new Move(2, 1);
            // Right-down
            legalMoves[5, 0] = new Move(1, 2);
            // Right-up
            legalMoves[6, 0] = new Move(-1, 2);
            // Up-right
            legalMoves[7, 0] = new Move(-2, 1);

            return legalMoves;
        }
    }
}
