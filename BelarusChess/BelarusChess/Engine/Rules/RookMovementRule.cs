using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelarusChess.Engine.Rules
{
    /// <summary> Movement rule for <see cref="Pieces.Rook"/> </summary>
    public class RookMovementRule : IRule
    {
        // Private fields to use by RecursivelyFindNextValidCell() method
        private readonly List<Cell> validCells;
        private Piece piece;
        private Chessboard chessboard;

        public RookMovementRule()
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

            RecursivelyFindNextValidCell(RookDirection.Up,    Cell.Create(piece.Cell.Row - 1, piece.Cell.Col));
            RecursivelyFindNextValidCell(RookDirection.Right, Cell.Create(piece.Cell.Row, piece.Cell.Col + 1));
            RecursivelyFindNextValidCell(RookDirection.Down,  Cell.Create(piece.Cell.Row + 1, piece.Cell.Col));
            RecursivelyFindNextValidCell(RookDirection.Left,  Cell.Create(piece.Cell.Row, piece.Cell.Col - 1));

            return validCells;
        }

        private void RecursivelyFindNextValidCell(RookDirection direction, Cell currentCell)
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
                case RookDirection.Up:
                    currentCell = Cell.Create(currentCell.Row - 1, currentCell.Col);
                    break;
                case RookDirection.Right:
                    currentCell = Cell.Create(currentCell.Row, currentCell.Col + 1);
                    break;
                case RookDirection.Down:
                    currentCell = Cell.Create(currentCell.Row + 1, currentCell.Col);
                    break;
                case RookDirection.Left:
                    currentCell = Cell.Create(currentCell.Row, currentCell.Col - 1);
                    break;
            }

            RecursivelyFindNextValidCell(direction, currentCell);
        }

        private enum RookDirection
        {
            Up, Right, Down, Left
        }
    }
}
