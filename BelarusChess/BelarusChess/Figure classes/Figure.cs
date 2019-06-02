using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BelarusChess
{
    abstract public class Figure
    {
        public Image Image { get; set; }
        public Chess.PlayerColor Color { get; set; }
        public Chess.FigureType Type { get; set; }
        public abstract Moves[,] Moves();
    }
}
