using System;
using System.Collections.Generic;

namespace BelarusChess
{
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