using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelarusChess.Engine.Rules
{
    public class BishopMovementRule : IRule
    {
        // Private fields to use by RecursivelyFindNextValidCell() method
        private readonly List<Cell> validCells;
        private Piece piece;
        private Chessboard chessboard;

        public BishopMovementRule()
        {
            validCells = new List<Cell>();
        }

        public List<Cell> ValidCells(Piece piece, Chessboard chessboard)
        {
            if (chessboard[piece.Cell] != piece)
                throw new ArgumentException("Piece must belong to the board");

            this.piece = piece;
            this.chessboard = chessboard;
            validCells.Clear();

            RecursivelyFindNextValidCell(BishopDirection.UpLeft,    Cell.Create(piece.Cell.Row - 1, piece.Cell.Col - 1));
            RecursivelyFindNextValidCell(BishopDirection.UpRight,   Cell.Create(piece.Cell.Row - 1, piece.Cell.Col + 1));
            RecursivelyFindNextValidCell(BishopDirection.DownRight, Cell.Create(piece.Cell.Row + 1, piece.Cell.Col + 1));
            RecursivelyFindNextValidCell(BishopDirection.DownLeft,  Cell.Create(piece.Cell.Row + 1, piece.Cell.Col - 1));

            return validCells;
        }

        private void RecursivelyFindNextValidCell(BishopDirection direction, Cell currentCell)
        {
            #region Exit conditions

            // If cell is not valid (outside of the chessboard)
            if (currentCell == null)
                return;
            // If cell contains piece
            if (chessboard[currentCell] != null)
            {
                // If cell contains opponent's piece
                if (chessboard[currentCell].Color != piece.Color)
                    validCells.Add(currentCell);
                return;
            }
            // If cell is the throne
            else if (currentCell.Row == 4 && currentCell.Col == 4)
            {
                validCells.Add(currentCell);
                return;
            }

            #endregion

            validCells.Add(currentCell);

            switch (direction)
            {
                case BishopDirection.UpLeft:
                    currentCell = Cell.Create(currentCell.Row - 1, currentCell.Col - 1);
                    break;
                case BishopDirection.UpRight:
                    currentCell = Cell.Create(currentCell.Row - 1, currentCell.Col + 1);
                    break;
                case BishopDirection.DownRight:
                    currentCell = Cell.Create(currentCell.Row + 1, currentCell.Col + 1);
                    break;
                case BishopDirection.DownLeft:
                    currentCell = Cell.Create(currentCell.Row + 1, currentCell.Col - 1);
                    break;
            }

            RecursivelyFindNextValidCell(direction, currentCell);
        }

        private enum BishopDirection
        {
            UpLeft, UpRight, DownRight, DownLeft
        }
    }
}
