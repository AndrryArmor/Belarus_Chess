using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BelarusChess
{
    /// <summary>
    /// Represents a 2-dimensional chessboard with <see cref="Piece"/>s.
    /// </summary>
    public class Chessboard : ICloneable
    {
        private readonly Piece[,] startBoard;
        private event EventHandler<ChessboardPieceMovedEventArgs> ChessboardPieceMoved;

        public const int Length = 9;
        public Piece WhiteKing { get; private set; }
        public Piece BlackKing { get; private set; }
        public Piece WhitePrince { get; private set; }
        public Piece BlackPrince { get; private set; }
        public Piece[,] Board { get; private set; }

        public Chessboard(EventHandler<ChessboardPieceMovedEventArgs> eventHandler)
        {
            ChessboardPieceMoved += eventHandler;

            #region StartBoard initialisation

            startBoard = new Piece[Length, Length];

            var blackPiecesTypesFirstRow = new PieceType[]
            { PieceType.Rook, PieceType.Knight, PieceType.Bishop, PieceType.Queen, PieceType.King, PieceType.Prince, PieceType.Knight, PieceType.Bishop, PieceType.Rook };

            var whitePiecesTypesFirstRow = new PieceType[]
            { PieceType.Rook, PieceType.Bishop, PieceType.Knight, PieceType.Prince, PieceType.King, PieceType.Queen, PieceType.Bishop, PieceType.Knight, PieceType.Rook };

            for (int col = 0; col < Length; col++)
            {
                // First row initialisation
                startBoard[0, col] = new Piece(PlayerColor.Black, blackPiecesTypesFirstRow[col], Cell.Create(0, col));
                // Second row initialisation
                startBoard[1, col] = new Piece(PlayerColor.Black, PieceType.Pawn, Cell.Create(1, col));

                // Seventh row initialisation
                startBoard[7, col] = new Piece(PlayerColor.White, PieceType.Pawn, Cell.Create(7, col));
                // Eighth row initialisation
                startBoard[8, col] = new Piece(PlayerColor.White, whitePiecesTypesFirstRow[col], Cell.Create(8, col));
            }

            #endregion

            Reset();
        }

        public Piece this [Cell cell]
        {
            get => (cell == null ? null : Board[cell.Row, cell.Col]);
            set
            {
                if (cell == null)
                    return;

                ChessboardPieceMovedEventArgs args = new ChessboardPieceMovedEventArgs()
                {
                    OldCell = value?.Cell,
                    NewCell = cell
                };

                Board[cell.Row, cell.Col] = value;
                if (value != null)
                    value.Cell = cell;

                ChessboardPieceMoved?.Invoke(this, args);
            }
        }

        public object Clone()
        {
            Chessboard chessboard = new Chessboard(null);

            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col < Length; col++)
                {
                    chessboard.Board[row, col] = (Piece)Board[row, col]?.Clone();
                }
            }

            chessboard.WhiteKing = chessboard[WhiteKing?.Cell];
            chessboard.BlackKing = chessboard[BlackKing?.Cell];
            chessboard.WhitePrince = chessboard[WhitePrince?.Cell];
            chessboard.BlackPrince = chessboard[BlackPrince?.Cell];

            return chessboard;
        }

        public void Reset()
        {
            Board = (Piece[,])startBoard.Clone();
            // Resets pieces location
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    if (Board[i, j] != null)
                        Board[i, j].Cell = Cell.Create(i, j);
                }
            }

            WhiteKing = startBoard[8, 4];
            BlackKing = startBoard[0, 4];
            WhitePrince = startBoard[8, 3];
            BlackPrince = startBoard[0, 5];
        }

        public void Inauguration(PlayerColor color)
        {
            if (color == PlayerColor.White)
            {
                this[WhitePrince.Cell] = WhiteKing;
                WhitePrince = null;
            }
            else
            {
                this[BlackPrince.Cell] = BlackKing;
                BlackPrince = null;
            }
        }

        public void PrinceKilled(PlayerColor color)
        {
            if (color == PlayerColor.White)
                WhitePrince = null;
            else
                BlackPrince = null;
        }
    }
}
