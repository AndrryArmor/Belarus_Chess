using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelarusChess.Engine.Rules
{
    /// <summary> Movement rule for <see cref="Pieces.Prince"/> </summary>
    public class PrinceMovementRule : IRule
    {
        public PrinceMovementRule() { }

        public List<Cell> ValidCells(Piece piece, Chessboard chessboard)
        {
            if (chessboard[piece.Cell] != piece)
                throw new ArgumentException("Piece must belong to the board");

            var validCells = new List<Cell>();
            var queenMovementRule = new QueenMovementRule();

            validCells.AddRange(queenMovementRule.ValidCells(piece, chessboard));
            validCells.RemoveAll(cell =>
            {
                int rowDifference = Math.Abs(cell.Row - piece.Cell.Row);
                int colDifference = Math.Abs(cell.Col - piece.Cell.Col);
                return (rowDifference > 2 || colDifference > 2 ? true : false);
            });

            return validCells;
        }
    }
}
