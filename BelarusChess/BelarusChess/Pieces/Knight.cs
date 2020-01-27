using System.Windows.Controls;

namespace BelarusChess.Pieces
{
    public class Knight : Piece
    {
        protected override Move[,] Moves { get => KnightMoves(); }

        public Knight(PlayerColor color, Cell cell) : base(color, PieceType.Knight, cell) { }

        private static Move[,] KnightMoves()
        {
            Move[,] validMoves = new Move[8, 1];

            // Up-left
            validMoves[0, 0] = new Move(-2, -1);
            // Left-up
            validMoves[1, 0] = new Move(-1, -2);
            // Left-down
            validMoves[2, 0] = new Move(1, -2);
            // Down-left
            validMoves[3, 0] = new Move(2, -1);
            // Down-right
            validMoves[4, 0] = new Move(2, 1);
            // Right-down
            validMoves[5, 0] = new Move(1, 2);
            // Right-up
            validMoves[6, 0] = new Move(-1, 2);
            // Up-right
            validMoves[7, 0] = new Move(-2, 1);

            return validMoves;
        }
    }
}
