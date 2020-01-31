using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelarusChess.Engine.Rules
{
    public class PawnMovementRule : IRule
    {
        public PawnMovementRule() { }

        // TODO in v1.1: add en passant checking
        public List<Cell> ValidCells(Piece piece, Chessboard chessboard)
        {
            if (chessboard[piece.Cell] != piece)
                throw new ArgumentException("Piece must belong to the board");

            var validCells = new List<Cell>();
            Cell cellUp = null;
            Cell cellDoubleUp = null;
            Cell cellUpLeft = null;
            Cell cellUpRight = null;
            int startPawnRow = 1;

            switch (piece.Color)
            {
                case PlayerColor.White:
                    cellUp = Cell.Create(piece.Cell.Row - 1, piece.Cell.Col);
                    cellDoubleUp = Cell.Create(piece.Cell.Row - 2, piece.Cell.Col);
                    cellUpLeft = Cell.Create(piece.Cell.Row - 1, piece.Cell.Col - 1);
                    cellUpRight = Cell.Create(piece.Cell.Row - 1, piece.Cell.Col + 1);
                    startPawnRow = 7;
                    break;
                case PlayerColor.Black:
                    cellUp = Cell.Create(piece.Cell.Row + 1, piece.Cell.Col);
                    cellDoubleUp = Cell.Create(piece.Cell.Row + 2, piece.Cell.Col);
                    cellUpLeft = Cell.Create(piece.Cell.Row + 1, piece.Cell.Col + 1);
                    cellUpRight = Cell.Create(piece.Cell.Row + 1, piece.Cell.Col - 1);
                    startPawnRow = 1;
                    break;
            }

            #region Getting valid pawn moves

            // Move up (if cell is empty)
            if (cellUp != null && chessboard[cellUp] == null)
            {
                validCells.Add(cellUp);
                // Double move (if pawn has not moved yet and cell is empty)
                if (piece.Cell.Row == startPawnRow)
                {
                    if (cellDoubleUp != null && chessboard[cellDoubleUp] == null)
                        validCells.Add(cellDoubleUp);
                }
            }

            // Beat up-left (if cell contains opponent's piece)
            if (chessboard[cellUpLeft] != null && chessboard[cellUpLeft].Color != piece.Color)
                validCells.Add(cellUpLeft);

            // Beat up-right (if cell contains opponent's piece)
            if (chessboard[cellUpRight] != null && chessboard[cellUpRight].Color != piece.Color)
                validCells.Add(cellUpRight);

            #endregion

            return validCells;
        }
    }
}
