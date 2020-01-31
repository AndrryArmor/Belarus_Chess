using BelarusChess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BelarusChess
{
    /// <summary>
    /// Represents a 2-dimensional chessboard with <see cref="Piece"/>s.
    /// </summary>
    public class Chessboard
    {
        private readonly Piece[,] startBoard;
        private event EventHandler<ChessboardPieceMovedEventArgs> ChessboardPieceMoved;

        public Piece WhiteKing { get; private set; }
        public Piece BlackKing { get; private set; }
        public Piece WhitePrince { get; private set; }
        public Piece BlackPrince { get; private set; }
        public Piece[,] Board { get; private set; }
        public int Length { get => Board.GetLength(0); }


        public Chessboard(EventHandler<ChessboardPieceMovedEventArgs> eventHandler)
        {
            ChessboardPieceMoved += eventHandler;

            startBoard = new Piece[9, 9];

            #region Black pieces initialisation

            startBoard[0, 0] = new Rook(PlayerColor.Black, Cell.Create(0, 0));
            startBoard[0, 1] = new Knight(PlayerColor.Black, Cell.Create(0, 1));
            startBoard[0, 2] = new Bishop(PlayerColor.Black, Cell.Create(0, 2));
            startBoard[0, 3] = new Queen(PlayerColor.Black, Cell.Create(0, 3));
            startBoard[0, 4] = new King(PlayerColor.Black, Cell.Create(0, 4));
            startBoard[0, 5] = new Prince(PlayerColor.Black, Cell.Create(0, 5));
            startBoard[0, 6] = new Knight(PlayerColor.Black, Cell.Create(0, 6));
            startBoard[0, 7] = new Bishop(PlayerColor.Black, Cell.Create(0, 7));
            startBoard[0, 8] = new Rook(PlayerColor.Black, Cell.Create(0, 8));

            startBoard[1, 0] = new BlackPawn(Cell.Create(1, 0));
            startBoard[1, 1] = new BlackPawn(Cell.Create(1, 1));
            startBoard[1, 2] = new BlackPawn(Cell.Create(1, 2));
            startBoard[1, 3] = new BlackPawn(Cell.Create(1, 3));
            startBoard[1, 4] = new BlackPawn(Cell.Create(1, 4));
            startBoard[1, 5] = new BlackPawn(Cell.Create(1, 5));
            startBoard[1, 6] = new BlackPawn(Cell.Create(1, 6));
            startBoard[1, 7] = new BlackPawn(Cell.Create(1, 7));
            startBoard[1, 8] = new BlackPawn(Cell.Create(1, 8));

            #endregion

            #region White pieces initialisation

            startBoard[7, 0] = new WhitePawn(Cell.Create(7, 0));
            startBoard[7, 1] = new WhitePawn(Cell.Create(7, 1));
            startBoard[7, 2] = new WhitePawn(Cell.Create(7, 2));
            startBoard[7, 3] = new WhitePawn(Cell.Create(7, 3));
            startBoard[7, 4] = new WhitePawn(Cell.Create(7, 4));
            startBoard[7, 5] = new WhitePawn(Cell.Create(7, 5));
            startBoard[7, 6] = new WhitePawn(Cell.Create(7, 6));
            startBoard[7, 7] = new WhitePawn(Cell.Create(7, 7));
            startBoard[7, 8] = new WhitePawn(Cell.Create(7, 8));

            startBoard[8, 0] = new Rook(PlayerColor.White, Cell.Create(8, 0));
            startBoard[8, 1] = new Bishop(PlayerColor.White, Cell.Create(8, 1));
            startBoard[8, 2] = new Knight(PlayerColor.White, Cell.Create(8, 2));
            startBoard[8, 3] = new Prince(PlayerColor.White, Cell.Create(8, 3));
            startBoard[8, 4] = new King(PlayerColor.White, Cell.Create(8, 4));
            startBoard[8, 5] = new Queen(PlayerColor.White, Cell.Create(8, 5));
            startBoard[8, 6] = new Bishop(PlayerColor.White, Cell.Create(8, 6));
            startBoard[8, 7] = new Knight(PlayerColor.White, Cell.Create(8, 7));
            startBoard[8, 8] = new Rook(PlayerColor.White, Cell.Create(8, 8));

            #endregion

            WhiteKing = startBoard[8, 4];
            BlackKing = startBoard[0, 4];
            WhitePrince = startBoard[8, 3];
            BlackPrince = startBoard[0, 5];

            Reset();
        }

        public Piece this [Cell cell]
        {
            get => (cell == null ? null : Board[cell.Row, cell.Col]);
            set
            {
                if (cell == null)
                    return;

                #region Princes syncronisation

                if (Board[cell.Row, cell.Col]?.Type == PieceType.Prince)
                {
                    if (Board[cell.Row, cell.Col].Color == PlayerColor.White)
                        WhitePrince = null;
                    else
                        BlackPrince = null;
                }
                else if (value?.Type == PieceType.Prince)
                {
                    if (value.Color == PlayerColor.White && WhitePrince == null)
                        WhitePrince = value;
                    else if (value.Color == PlayerColor.Black && BlackPrince == null)
                        BlackPrince = value;
                }

                #endregion

                ChessboardPieceMovedEventArgs args = new ChessboardPieceMovedEventArgs()
                {
                    OldCell = value?.Cell,
                    NewCell = cell
                };

                Board[cell.Row, cell.Col] = value;
                if (value != null)
                    value.Cell = cell;

                OnChessboardPieceMoved(args);
            }
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
        }

        private void OnChessboardPieceMoved(ChessboardPieceMovedEventArgs e)
        {
            ChessboardPieceMoved?.Invoke(this, e);
        }
    }
}
