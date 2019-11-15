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

    public enum MoveType
    {
        Regular, Check, Checkmate, Inauguration, Throne, ThroneMine
    }
}
