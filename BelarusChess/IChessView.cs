using System;
using System.Collections.Generic;

namespace BelarusChess
{
    /// <summary>
    /// Interface for manipulating the visual elements in the window by <see cref="GameController"/>
    /// </summary>
    public interface IChessView
    {
        void SetTime(TimeSpan time);
        void SetValidCells(List<Cell> validCells);
        void SetStateForPlayers(GameState stateWhite, GameState stateBlack, PlayerColor? inaugurationState);
        void Chessboard_ChessboardPieceMoved(object sender, ChessboardPieceMovedEventArgs e);
    }
}