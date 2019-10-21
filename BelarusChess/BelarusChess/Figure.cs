using System.Windows;
using System.Windows.Controls;

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

    /// <summary> Determines all possible types of figures </summary>
    public enum FigureType
    {
        Rook, Bishop, Knight, Prince, Queen, King, WhitePawn, BlackPawn
    }
}
