using System.Windows;
using System.Windows.Controls;
using System;
using System.Collections.Generic;

namespace BelarusChess
{
    /// <summary> Describes the chess figure </summary>
    public abstract class Figure
    {
        private Cell cell;
        protected virtual Move[,] Moves { get; }
        public PlayerColor Color { get; }
        public FigureType Type { get; }
        public Cell Cell
        {
            get => cell;
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                cell = value;
            }
        }
        
        protected Figure(PlayerColor color, FigureType type, Cell cell)
        {
            Color = color;
            Type = type;
            Cell = cell;
        }

        public virtual List<Cell> ValidCells(Chessboard chessboard, PlayerColor playerColor)
        {
            var validCells = new List<Cell>();

            /// Finds valid moves for figure
            for (int i = 0; i < Moves.GetLength(0); i++)
            {
                for (int j = 0; j < Moves.GetLength(1); j++)
                {
                    Cell newCell = Cell.Create(Cell.Row + Moves[i, j].Rows, Cell.Col + Moves[i, j].Cols);

                    /// If cell not valid or contains friendly figure
                    if (newCell == null || (chessboard[newCell] != null && chessboard[newCell].Color == playerColor))
                        break;

                    validCells.Add(newCell);

                    /// If cell contains opponents figure or it stays on throne (except prince)
                    if (chessboard[newCell] != null || (newCell.Row == 4 && newCell.Col == 4 && Type != FigureType.Prince))
                        break;
                }
            }
            return validCells;
        }
    }
}
