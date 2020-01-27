using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BelarusChess.Pieces
{
    public class BlackPawn : Piece
    {
        public BlackPawn(Cell cell) : base(PlayerColor.Black, PieceType.BlackPawn, cell) { }

        public override List<Cell> ValidCells(Chessboard chessboard, PlayerColor playerColor)
        {
            var validCells = new List<Cell>();

            // Move up
            Cell cellUp = Cell.Create(Cell.Row + 1, Cell.Col);
            if (cellUp != null && chessboard[cellUp] == null)
            {
                validCells.Add(cellUp);
                // Double move
                if (Cell.Row == 1)
                {                    
                    Cell cellDoubleUp = Cell.Create(Cell.Row + 2, Cell.Col);
                    if (cellDoubleUp != null && chessboard[cellDoubleUp] == null)
                        validCells.Add(cellDoubleUp);
                }
            }

            // Beat down-left
            Cell cellDownLeft = Cell.Create(Cell.Row + 1, Cell.Col + 1);
            if (cellDownLeft != null && chessboard[cellDownLeft] != null && chessboard[cellDownLeft].Color != playerColor)
                validCells.Add(cellDownLeft);

            // Beat down-right
            Cell cellDownRight = Cell.Create(Cell.Row + 1, Cell.Col - 1);
            if (cellDownRight != null && chessboard[cellDownRight] != null && chessboard[cellDownRight].Color != playerColor)
                validCells.Add(cellDownRight);

            return validCells;
        }
    }
}
