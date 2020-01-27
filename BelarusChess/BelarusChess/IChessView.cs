using System;
using System.Collections.Generic;

namespace BelarusChess
{
    /// <summary>
    /// Interface for manipulating the visual elements in the window by <see cref="ChessEngine"/> class
    /// </summary>
    public interface IChessView
    {
        void SetTime(TimeSpan time);
        void SetValidCells(List<Cell> validCells);
        void SetPieceNewCell(Cell oldCell, Cell newCell);
        void SetMessageWhite(string message);
        void SetMessageBlack(string message);
        void SetCheck();
    }
}