using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BelarusChess.Figures
{
    public class WhitePawn : Figure
    {
        public override Move[,] Moves { get => WhitePawnMoves(); }

        public WhitePawn(Image image, Cell cell) : base(image, PlayerColor.White, FigureType.WhitePawn, cell) { }

        private static Move[,] WhitePawnMoves()
        {
            Move[,] legalMoves = new Move[3, 2];

            // Up
            legalMoves[0, 0] = new Move(-1, 0);
            // Double up
            legalMoves[0, 1] = new Move(-2, 0);
            // Up-left
            legalMoves[1, 0] = new Move(-1, -1);
            // Up-right
            legalMoves[2, 0] = new Move(-1, 1);

            return legalMoves;
        }
    }
}
