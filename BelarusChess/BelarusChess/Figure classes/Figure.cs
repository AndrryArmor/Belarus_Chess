using System.Windows.Controls;

namespace BelarusChess
{
    /// <summary> Describes the coordinates on the board as a pair of numbers </summary>
    public struct Moves
    {
        public int X;
        public int Y;
        public Moves(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    /// <summary> Describes the abstract chess figure </summary>
    public abstract class Figure
    {
        public Image Image { get; set; }
        public PlayerColor Color { get; set; }
        public FigureType Type { get; set; }
        public abstract Moves[,] Moves();
    }
}
