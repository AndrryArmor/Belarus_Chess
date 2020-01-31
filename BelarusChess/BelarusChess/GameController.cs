using BelarusChess.Engine;
using BelarusChess.Pieces;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace BelarusChess
{
    public class GameController
    {
        #region Private fields

        private readonly IChessView view;
        private readonly ChessEngine engine;
        private readonly Timer oneSecond;
        private TimeSpan time;
        private Piece choosedPiece;

        #endregion

        #region Public properties

        public PlayerColor CurrentColor { get; private set; }
        public bool IsGameStarted { get; set; }

        #endregion

        public GameController(IChessView chessView)
        {
            view = chessView;
            engine = new ChessEngine(view.Chessboard_ChessboardPieceMoved);
            oneSecond = new System.Timers.Timer { Interval = 1000 };
            oneSecond.Elapsed += OneSecond_Elapsed;
        }

        public void Start()
        {
            engine.ResetChessboard();
            CurrentColor = PlayerColor.White;
            IsGameStarted = true;
            time = TimeSpan.Zero;
            oneSecond.Start();
        }

        public void FindPieceValidMoves(Cell pieceCell)
        {
            choosedPiece = engine.GetPieceAt(pieceCell);
            // If piece does not exist or there is not turn of current player
            if (choosedPiece == null || choosedPiece.Color != CurrentColor)
                return;

            List<Cell> validCells = engine.GetPieceValidCells(choosedPiece);
            view.SetValidCells(validCells);
        }

        public void MakeMoveTo(Cell cell)
        {
            engine.MakeMove(choosedPiece, cell);

            view.SetMessageWhite(GetMessageForState(engine.WhitePlayerState));
            view.SetMessageBlack(GetMessageForState(engine.BlackPlayerState));

            SwitchPlayer();
        }

            private string GetMessageForState(GameState gameState)
            {
                string message = "";

                switch (gameState)
                {
                    case GameState.Regular:
                        break;
                    case GameState.Check:
                        message += "Шах! ";
                        break;
                    case GameState.Throne:
                        message += "Престол!";
                        break;
                    case GameState.Inauguration:
                        message += "Інавгурація";
                        break;
                    case GameState.Stalemate:
                        message += "Пат";
                        break;
                    case GameState.Checkmate:
                        message += "Мат";
                        break;
                    case GameState.ThroneMine:
                        message += "Престол мій";
                        break;
                }

                return message;
            }

        private void SwitchPlayer()
        {
            switch (CurrentColor)
            {
                case PlayerColor.White:
                    CurrentColor = PlayerColor.Black;
                    break;
                case PlayerColor.Black:
                    CurrentColor = PlayerColor.White;
                    break;
            }
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
