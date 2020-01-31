using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BelarusChess.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(PlayerColor color, Cell cell) : base(color, PieceType.Pawn, cell) { }
    }
}
