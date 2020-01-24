using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelarusChess
{
    public enum FigureType
    {
        Rook, Bishop, Knight, Prince, Queen, King, WhitePawn, BlackPawn
    }
    public enum PlayerColor
    {
        White, Black
    }
    public static class PlayerColorExtension
    {
        /// <summary>Provides the right of move to the next player</summary>
        public static PlayerColor Next(this PlayerColor color)
        {
            return (color == PlayerColor.White ? PlayerColor.Black : PlayerColor.White);
        }
    }

    public enum MoveType
    {
        Regular, Check, Checkmate, Inauguration, Throne, ThroneMine
    }
}
