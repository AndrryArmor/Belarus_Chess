using System;

namespace BelarusChess
{
    public class ChessboardPieceMovedEventArgs : EventArgs
    {
        public Cell OldCell { get; set; }
        public Cell NewCell { get; set; }
    }
}