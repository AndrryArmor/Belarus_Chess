using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BelarusChess.Pieces
{
    public class WhitePawn : Piece
    {
        public WhitePawn(Cell cell) : base(PlayerColor.White, PieceType.WhitePawn, cell) { }

        public override List<Cell> ValidCells(Chessboard chessboard, PlayerColor playerColor)
        {
            var validCells = new List<Cell>();

            // Move up
            Cell cellUp = Cell.Create(Cell.Row - 1, Cell.Col);
            if (cellUp != null && chessboard[cellUp] == null)
            {
                validCells.Add(cellUp);
                // Double move
                if (Cell.Row == 7)
                {
                    Cell cellDoubleUp = Cell.Create(Cell.Row - 2, Cell.Col);
                    if (cellDoubleUp != null && chessboard[cellDoubleUp] == null)
                        validCells.Add(cellDoubleUp);
                }
            }

            // Beat up-left
            Cell cellUpLeft = Cell.Create(Cell.Row - 1, Cell.Col - 1);
            if (cellUpLeft != null && chessboard[cellUpLeft] != null && chessboard[cellUpLeft].Color != playerColor)
                validCells.Add(cellUpLeft);

            // Beat up-right
            Cell cellUpRight = Cell.Create(Cell.Row - 1, Cell.Col + 1);
            if (cellUpRight != null && chessboard[cellUpRight] != null && chessboard[cellUpRight].Color != playerColor)
                validCells.Add(cellUpRight);

            return validCells;
        }
    }
}
