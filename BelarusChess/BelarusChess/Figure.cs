using System.Windows;
using System.Windows.Controls;

namespace BelarusChess
{
    /// <summary> Describes the chess cell as a pair of numbers </summary>
    public struct Cell
    {
        public int Row;
        public int Col;
        public Cell(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }

    /// <summary> Describes the chess figure </summary>
    public class Figure
    {
        public Image Image { get; }
        public PlayerColor Color { get; }
        public FigureType Type { get; }
        public Cell Cell
        {
            get => Cell;
            set
            {
                Cell = value;
                Image.Margin = new Thickness(MainWindow.leftMargin + Cell.Col * MainWindow.cellEdge,
                                             MainWindow.topMargin + Cell.Row * MainWindow.cellEdge, 0, 0);
            }
        }
        public Figure(Image image, PlayerColor color, FigureType type, Cell cell)
        {
            Image = image;
            Color = color;
            Type = type;
            Cell = cell;

            image.Visibility = Visibility.Visible;
            image.Tag = this;
            image.Margin = new Thickness(MainWindow.leftMargin + Cell.Col * MainWindow.cellEdge,
                                         MainWindow.topMargin + Cell.Row * MainWindow.cellEdge, 0, 0);
        }
    }
}
