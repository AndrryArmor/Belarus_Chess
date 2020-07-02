using System.Windows;
using System.Windows.Controls;
using System;
using System.Collections.Generic;

namespace BelarusChess
{
    /// <summary> Describes the chess piece </summary>
    public class Piece : ICloneable
    {
        private Cell cell;

        public PlayerColor Color { get; }
        public PieceType Type { get; }
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
        
        public Piece(PlayerColor color, PieceType type, Cell cell)
        {
            Color = color;
            Type = type;
            Cell = cell;
        }

        public object Clone()
        {
            return new Piece(Color, Type, (Cell)Cell.Clone());
        }
    }
}
