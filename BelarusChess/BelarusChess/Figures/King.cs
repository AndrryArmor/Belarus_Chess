using System.Windows.Controls;

namespace BelarusChess
{
    public class King : Figure
    {
        public override Move[,] Moves { get => KingMoves(); }

        public King(Image image, PlayerColor color, Cell cell) : base(image, color, FigureType.King, cell) { }

        private static Move[,] KingMoves()
        {
            Move[,] legalMoves = new Move[8, 1];

            // Up
            legalMoves[0, 0] = new Move(-1, 0);
            // Up-left
            legalMoves[1, 0] = new Move(-1, -1);
            // Left
            legalMoves[2, 0] = new Move(0, -1);
            // Down-left
            legalMoves[3, 0] = new Move(1, -1);
            // Down
            legalMoves[4, 0] = new Move(1, 0);
            // Down-right
            legalMoves[5, 0] = new Move(1, 1);
            // Right
            legalMoves[6, 0] = new Move(0, 1);
            // Up-right
            legalMoves[7, 0] = new Move(-1, 1);

            return legalMoves;
        }
    }
}
