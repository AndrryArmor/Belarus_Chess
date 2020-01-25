using BelarusChess.Figures;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace BelarusChess
{
    public class ChessGame
    {
        private readonly IChessView view;
        private readonly Timer oneSecond;
        private TimeSpan time;
        private Chessboard Chessboard;
        private Figure choosedFigure;

        public PlayerColor CurrentColor { get; private set; }
        public bool IsGameStarted { get; set; } = false;

        public ChessGame(IChessView view)
        {
            this.view = view;
            Chessboard = new Chessboard();
            oneSecond = new System.Timers.Timer { Interval = 1000 };
            oneSecond.Elapsed += OneSecond_Elapsed;
        }

        public void Start()
        {
            IsGameStarted = true;
            Chessboard.Reset();
            time = TimeSpan.Zero;
            view.SetTime(time);
            oneSecond.Start();
        }

        public void FindFigureValidMoves(Cell figureCell)
        {
            choosedFigure = Chessboard[figureCell];
            // If figure does not exist or there is not turn of current player
            if (choosedFigure == null || choosedFigure.Color != CurrentColor)
                return;            

            view.SetValidCells(choosedFigure.ValidCells(Chessboard, CurrentColor));
        }

        public void MakeMoveTo(Cell cell)
        {
            view.SetFigureNewCell(choosedFigure.Cell, cell);
            Chessboard[choosedFigure.Cell] = null;
            Chessboard[cell] = choosedFigure;
            CurrentColor = CurrentColor.Next();
        }

        private bool IsCheck(Chessboard chessboard)
        {
            return false;
        }

        public void Finish() 
        {
            IsGameStarted = false;
            oneSecond.Stop();
        }

        // Events
        private void OneSecond_Elapsed(object sender, ElapsedEventArgs e)
        {
            long second = TimeSpan.TicksPerSecond;
            time.Add(new TimeSpan(second));
            view.SetTime(time);
        }
    }
}
