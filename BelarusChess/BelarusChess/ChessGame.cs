using BelarusChess.Pieces;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace BelarusChess
{
    /// <summary>
    /// Class that provides chess game
    /// </summary>
    public class ChessGame
    {
        #region Private fields

        private readonly IChessView view;
        private readonly Timer oneSecond;
        private TimeSpan time;
        private readonly Chessboard Chessboard;
        private Piece choosedPiece;

        #endregion

        #region Public properties

        public PlayerColor CurrentColor { get; private set; }
        public GameState GameState { get; private set; }
        public bool IsGameStarted { get; set; }

        #endregion

        public ChessGame(IChessView view)
        {
            this.view = view;
            Chessboard = new Chessboard(view.Chessboard_ChessboardPieceMoved);
            oneSecond = new System.Timers.Timer { Interval = 1000 };
            oneSecond.Elapsed += OneSecond_Elapsed;
        }

        public void Start()
        {
            Chessboard.Reset();
            CurrentColor = PlayerColor.White;
            IsGameStarted = true;
            time = TimeSpan.Zero;
            //view.SetTime(time);
            oneSecond.Start();
        }

        public void FindPieceValidMoves(Cell pieceCell)
        {
            choosedPiece = Chessboard[pieceCell];
            // If piece does not exist or there is not turn of current player
            if (choosedPiece == null || choosedPiece.Color != CurrentColor)
                return;

            List<Cell> validCells = choosedPiece.ValidCells(Chessboard, CurrentColor);


            view.SetValidCells(validCells);
        }

        public void MakeMoveTo(Cell cell)
        {
            Cell choosedPieceCell = choosedPiece.Cell;
            Chessboard[cell] = choosedPiece;
            Chessboard[choosedPieceCell] = null;
            CurrentColor = CurrentColor.Next();

            CheckGameState();
        }

        private void CheckGameState()
        {

        }

        private bool IsCheck(PlayerColor playerColor, Chessboard chessboard)
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
            time += new TimeSpan(second);
            view.SetTime(time);
        }

        #endregion
    }
}
