using System.Windows.Controls;

namespace BelarusChess.Pieces
{
    public class Prince : Piece
    {
        protected override Move[,] Moves { get => PrinceMoves(); }

        public Prince(PlayerColor color, Cell cell) : base(color, PieceType.Prince, cell) { }

        private static Move[,] PrinceMoves()
        {
            Move[,] validMoves = new Move[8, 2];
            for (int i = 0; i < 2; i++)
            {
                // Up
                validMoves[0, i] = new Move(-(i + 1), 0);
                // Up-left
                validMoves[1, i] = new Move(-(i + 1), -(i + 1));
                // Left
                validMoves[2, i] = new Move(0, -(i + 1));
                // Down-left
                validMoves[3, i] = new Move(i + 1, -(i + 1));
                // Down
                validMoves[4, i] = new Move(i + 1, 0);
                // Down-right
                validMoves[5, i] = new Move(i + 1, i + 1);
                // Right
                validMoves[6, i] = new Move(0, i + 1);
                // Up-right
                validMoves[7, i] = new Move(-(i + 1), i + 1);
            }
            return validMoves;
        }
    }
}
