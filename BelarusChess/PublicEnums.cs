using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelarusChess
{
    public enum PieceType
    {
        Rook, Bishop, Knight, Prince, Queen, King, Pawn
    }

    public enum PlayerColor
    {
        White, Black
    }

    public enum GameState
    {
        Regular, Check, Checkmate, Stalemate, Throne, ThroneMine
    }
}
