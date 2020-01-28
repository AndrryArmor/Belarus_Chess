using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelarusChess.Engine.Rules
{
    /// <summary>
    /// Interface for creating rules for chess engine.
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// Returns valid cells for piece on the current chessboard
        /// </summary>
        List<Cell> ValidCells(Piece piece, Chessboard chessboard);
    }
}
