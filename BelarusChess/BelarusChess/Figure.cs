using System.Windows;
using System.Windows.Controls;
using System;

namespace BelarusChess
{
    /// <summary> Describes the chess figure </summary>
    public abstract class Figure
    {        
        private Image image;
        private Cell cell;

        public Image Image
        {
            get => image;
            private set
            {
                if (value != null)
                {
                    image = value;
                    image.Visibility = Visibility.Visible;
                    image.Tag = this;
                }
            }
        }
        public PlayerColor Color { get; }
        public FigureType Type { get; }
        public Cell Cell
        {
            get => cell;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                cell = value;
                Image.Margin = new Thickness(MainWindow.leftMargin + cell.Col * MainWindow.cellEdge,
                                             MainWindow.topMargin + cell.Row * MainWindow.cellEdge, 0, 0);
            }
        }
        public virtual Move[,] Moves { get; }

        protected Figure(Image image, PlayerColor color, FigureType type, Cell cell)
        {
            Image = image;
            Color = color;
            Type = type;
            Cell = cell;
        }
    }
}
