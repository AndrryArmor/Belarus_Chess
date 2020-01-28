using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelarusChess.Engine.Rules
{
    /// <summary> Movement rule for <see cref="Pieces.Queen"/> </summary>
    public class QueenMovementRule : IRule
    {
        public QueenMovementRule() { }

        public List<Cell> ValidCells(Piece piece, Chessboard chessboard)
        {
            if (chessboard[piece.Cell] != piece)
                throw new ArgumentException("Piece must belong to the board");

            var validCells = new List<Cell>();
            var rookMovementRule = new RookMovementRule();
            var bishopMovementRule = new BishopMovementRule();

            validCells.AddRange(rookMovementRule.ValidCells(piece, chessboard));
            validCells.AddRange(bishopMovementRule.ValidCells(piece, chessboard));

            return validCells;
        }
    }
}
