using System.Windows.Controls;

namespace BelarusChess.Pieces
{
    public class King : Piece
    {
        protected override Move[,] Moves { get => KingMoves(); }

        public King(PlayerColor color, Cell cell) : base(color, PieceType.King, cell) { }

        private static Move[,] KingMoves()
        {
            Move[,] validMoves = new Move[8, 1];

            // Up
            validMoves[0, 0] = new Move(-1, 0);
            // Up-left
            validMoves[1, 0] = new Move(-1, -1);
            // Left
            validMoves[2, 0] = new Move(0, -1);
            // Down-left
            validMoves[3, 0] = new Move(1, -1);
            // Down
            validMoves[4, 0] = new Move(1, 0);
            // Down-right
            validMoves[5, 0] = new Move(1, 1);
            // Right
            validMoves[6, 0] = new Move(0, 1);
            // Up-right
            validMoves[7, 0] = new Move(-1, 1);

            return validMoves;
        }
    }
}
