﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public abstract class Piece
    {
        protected Chessboard _chessboard;

        protected Piece(PieceType type, PlayerColor color, Cell cell, Chessboard chessboard)
        {
            Type = type;
            Color = color;
            Cell = cell;
            _chessboard = chessboard;
        }

        private Cell cell;

        public PieceType Type { get; }
        public PlayerColor Color { get; }
        public Cell Cell
        {
            get => cell;
            set
            {
                cell = value;
                OnCellChange?.Invoke(this, Cell);
            }
        }

        public event EventHandler<Cell> OnCellChange;

        protected enum MoveDirection
        {
            Up, Right, Down, Left,
            UpLeft, UpRight, DownRight, DownLeft,
            UpUpLeft, UpUpRight, RightRightUp, RightRightDown, DownDownRight, DownDownLeft, LeftLeftDown, LeftLeftUp
        }

        public abstract IEnumerable<Cell> GetAvailableCells();

        protected virtual IEnumerable<Cell> FindAvailableCellsInDirection(MoveDirection direction)
        {
            var availableCells = new List<Cell>();
            var currentCell = GetNextCell(direction, Cell);

            while (IsCellAvailable(currentCell))
            {
                availableCells.Add(currentCell);

                // If cell is empty and not a throne
                if (_chessboard[currentCell] == null && (currentCell.Row != 4 || currentCell.Col != 4))
                    currentCell = GetNextCell(direction, currentCell);
                else
                    break;
            }

            return availableCells;
        }

        protected bool IsCellAvailable(Cell cell)
        {
            if (!cell.IsValid)
                return false;

            // Returns true if cell is empty or contains opponent's piece
            return _chessboard[cell] == null || _chessboard[cell].Color != Color;
        }

        protected Cell GetNextCell(MoveDirection direction, Cell currentCell)
        {
            Cell newCell = null;

            switch (direction)
            {
                case MoveDirection.Up:
                    newCell = new Cell(currentCell.Row - 1, currentCell.Col);
                    break;
                case MoveDirection.Right:
                    newCell = new Cell(currentCell.Row, currentCell.Col + 1);
                    break;
                case MoveDirection.Down:
                    newCell = new Cell(currentCell.Row + 1, currentCell.Col);
                    break;
                case MoveDirection.Left:
                    newCell = new Cell(currentCell.Row, currentCell.Col - 1);
                    break;
                case MoveDirection.UpLeft:
                    newCell = new Cell(currentCell.Row - 1, currentCell.Col - 1);
                    break;
                case MoveDirection.UpRight:
                    newCell = new Cell(currentCell.Row - 1, currentCell.Col + 1);
                    break;
                case MoveDirection.DownRight:
                    newCell = new Cell(currentCell.Row + 1, currentCell.Col + 1);
                    break;
                case MoveDirection.DownLeft:
                    newCell = new Cell(currentCell.Row + 1, currentCell.Col - 1);
                    break;
                case MoveDirection.UpUpLeft:
                    newCell = new Cell(currentCell.Row - 2, currentCell.Col - 1);
                    break;
                case MoveDirection.UpUpRight:
                    newCell = new Cell(currentCell.Row - 2, currentCell.Col + 1);
                    break;
                case MoveDirection.RightRightUp:
                    newCell = new Cell(currentCell.Row - 1, currentCell.Col + 2);
                    break;
                case MoveDirection.RightRightDown:
                    newCell = new Cell(currentCell.Row + 1, currentCell.Col + 2);
                    break;
                case MoveDirection.DownDownRight:
                    newCell = new Cell(currentCell.Row + 2, currentCell.Col + 1);
                    break;
                case MoveDirection.DownDownLeft:
                    newCell = new Cell(currentCell.Row + 2, currentCell.Col - 1);
                    break;
                case MoveDirection.LeftLeftDown:
                    newCell = new Cell(currentCell.Row + 1, currentCell.Col - 2);
                    break;
                case MoveDirection.LeftLeftUp:
                    newCell = new Cell(currentCell.Row - 1, currentCell.Col - 2);
                    break;
            }

            return newCell;
        }
    }
}