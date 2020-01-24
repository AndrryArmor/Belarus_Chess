using System.Windows.Controls;

namespace BelarusChess.Figures
{
    public class Rook : Figure
    {
        protected override Move[,] Moves { get => RookMoves(); }

        public Rook(PlayerColor color, Cell cell) : base(color, FigureType.Rook, cell) { }

        private static Move[,] RookMoves()
        {
            Move[,] validMoves = new Move[4, 8];
            for (int i = 0; i < 8; i++)
            {
                // Up
                validMoves[0, i] = new Move(-(i + 1), 0);
                // Left
                validMoves[1, i] = new Move(0, -(i + 1));
                // Down
                validMoves[2, i] = new Move(i + 1, 0);
                // Right
                validMoves[3, i] = new Move(0, i + 1);
            }
            return validMoves;
        }
    }
}
