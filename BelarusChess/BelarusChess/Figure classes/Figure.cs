using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BelarusChess
{
    public abstract class Figure
    {
        public Image Image { get; set; }
        public MainWindow.PlayerColor Color { get; set; }
        public MainWindow.FigureType Type { get; set; }
        public abstract Moves[,] Moves();
    }
}
