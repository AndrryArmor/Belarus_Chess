using BelarusChess.Pieces;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace BelarusChess
{
    public class ChessEngine
    {
        #region Private fields

        private readonly IChessView view;
        private readonly Timer oneSecond;
        private TimeSpan time;
        private Chessboard Chessboard;
        private Piece choosedPiece;

        #endregion

        #region Public properties

        public PlayerColor CurrentColor { get; private set; }
        public bool IsGameStarted { get; set; } = false;

        #endregion

        public ChessEngine(IChessView view)
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

        public void FindPieceValidMoves(Cell pieceCell)
        {
            choosedPiece = Chessboard[pieceCell];
            // If piece does not exist or there is not turn of current player
            if (choosedPiece == null || choosedPiece.Color != CurrentColor)
                return;            

            view.SetValidCells(choosedPiece.ValidCells(Chessboard, CurrentColor));
        }

        public void MakeMoveTo(Cell cell)
        {
            view.SetPieceNewCell(choosedPiece.Cell, cell);
            Chessboard[choosedPiece.Cell] = null;
            Chessboard[cell] = choosedPiece;
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

        #region Events 

        private void OneSecond_Elapsed(object sender, ElapsedEventArgs e)
        {
            long second = TimeSpan.TicksPerSecond;
            time.Add(new TimeSpan(second));
            view.SetTime(time);
        }

        #endregion
    }
}
