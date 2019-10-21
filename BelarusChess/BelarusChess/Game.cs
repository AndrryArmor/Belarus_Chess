using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace BelarusChess
{
    public class Game
    {
        private Timer timer;

        public Chessboard Chessboard { get; private set; }

        Game()
        {

        }

        public void Start() { }

        public void MakeMove() { }

        public void Finish() { }

        private PlayerColor Next(PlayerColor color) 
        {
            return (color == PlayerColor.White ? PlayerColor.Black : PlayerColor.White);
        }
    }
}
