using System.Windows;
using System.Windows.Controls;

namespace BelarusChess
{
    /// <summary> Describes the chess cell as a pair of numbers </summary>
    public struct Cell
    {
        public uint Row;
        public uint Col;
        public Cell(uint row, uint col)
        {
            Row = row;
            Col = col;
        }
    }

    /// <summary> Describes the chess figure </summary>
    public class Figure
    {
        public Image Image { get; set; }
        public PlayerColor Color { get; set; }
        public FigureType Type { get; set; }
        public Cell Cell { get; set; }
        public Figure(Image image, PlayerColor color, FigureType type, Cell cell)
        {
            Image = image;
            Color = color;
            Type = type;
            Cell = cell;

            image.Visibility = Visibility.Visible;
            image.Margin = new Thickness(MainWindow.xMargin + cell.Col * MainWindow.cellEdge,
                                         MainWindow.yMargin + cell.Row * MainWindow.cellEdge, 0, 0);
        }
    }
}
