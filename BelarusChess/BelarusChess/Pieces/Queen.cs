using System.Windows.Controls;

namespace BelarusChess.Pieces
{
    public class Queen : Piece
    {
        protected override Move[,] Moves { get => QueenMoves(); }

        public Queen(PlayerColor color, Cell cell) : base(color, PieceType.Queen, cell) { }

        private static Move[,] QueenMoves()
        {
            Move[,] validMoves = new Move[8, 8];
            for (int i = 0; i < 8; i++)
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
