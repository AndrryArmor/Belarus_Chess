using System.Windows.Controls;

namespace BelarusChess.Figures
{
    public class Bishop : Figure
    {
        protected override Move[,] Moves { get => BishopMoves(); }

        public Bishop(PlayerColor color, Cell cell) : base(color, FigureType.Bishop, cell) { }

        private static Move[,] BishopMoves()
        {
            Move[,] validMoves = new Move[4, 8];
            for (int i = 0; i < 8; i++)
            {
                // Up-left
                validMoves[0, i] = new Move(-(i + 1), -(i + 1));
                // Down-left
                validMoves[1, i] = new Move(i + 1, -(i + 1));
                // Down-right
                validMoves[2, i] = new Move(i + 1, i + 1);
                // Up-right
                validMoves[3, i] = new Move(-(i + 1), i + 1);
            }
            return validMoves;
        }
    }
}
