using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelarusChess
{
    public enum PieceType
    {
        Rook, Bishop, Knight, Prince, Queen, King, WhitePawn, BlackPawn, Pawn
    }

    public enum PlayerColor
    {
        White, Black
    }
    public static class PlayerColorExtension
    {
        /// <summary> Provides the right of move to the next player </summary>
        public static PlayerColor Next(this PlayerColor color)
        {
            return (color == PlayerColor.White ? PlayerColor.Black : PlayerColor.White);
        }
    }

    public enum GameState
    {
        Regular, Check, Checkmate, Stalemate, Inauguration, Throne, ThroneMine
    }
}
