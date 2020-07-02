using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelarusChess.Engine.Rules
{
    public class KnightMovementRule : IRule
    {
        public KnightMovementRule() { }

        public List<Cell> ValidCells(Piece piece, Chessboard chessboard)
        {
            if (chessboard[piece.Cell] != piece)
                throw new ArgumentException("Piece must belong to the board");

            var validCells = new List<Cell>
            {                
                // Left-up
                Cell.Create(piece.Cell.Row - 1, piece.Cell.Col - 2),
                // Up-left
                Cell.Create(piece.Cell.Row - 2, piece.Cell.Col - 1),
                // Up-right
                Cell.Create(piece.Cell.Row - 2, piece.Cell.Col + 1),
                // Right-up
                Cell.Create(piece.Cell.Row - 1, piece.Cell.Col + 2),
                // Right-down
                Cell.Create(piece.Cell.Row + 1, piece.Cell.Col + 2),
                // Down-right
                Cell.Create(piece.Cell.Row + 2, piece.Cell.Col + 1),
                // Down-left
                Cell.Create(piece.Cell.Row + 2, piece.Cell.Col - 1),
                // Left-down
                Cell.Create(piece.Cell.Row + 1, piece.Cell.Col - 2),
            };

            // Removes all non-valid cells (that are outside of the chessboard)
            validCells.RemoveAll(cell => cell == null);
            // Removes all cells that contain friendly piece
            validCells.RemoveAll(cell => chessboard[cell]?.Color == piece.Color);

            return validCells;
        }
    }
}
