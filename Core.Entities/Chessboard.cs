using Core.Entities.Pieces;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Core.Entities
{
    public class Chessboard
    {
        public Chessboard()
        {
            StartBoard = new Piece[9, 9];
            Board = new Piece[9, 9];

            #region Black pieces initialisation

            StartBoard[0, 0] = new Rook(PlayerColor.Black, new Cell(0, 0), this);
            StartBoard[0, 1] = new Knight(PlayerColor.Black, new Cell(0, 1), this);
            StartBoard[0, 2] = new Bishop(PlayerColor.Black, new Cell(0, 2), this);
            StartBoard[0, 3] = new Queen(PlayerColor.Black, new Cell(0, 3), this);
            StartBoard[0, 4] = new King(PlayerColor.Black, new Cell(0, 4), this);
            StartBoard[0, 5] = new Prince(PlayerColor.Black, new Cell(0, 5), this);
            StartBoard[0, 6] = new Knight(PlayerColor.Black, new Cell(0, 6), this);
            StartBoard[0, 7] = new Bishop(PlayerColor.Black, new Cell(0, 7), this);
            StartBoard[0, 8] = new Rook(PlayerColor.Black, new Cell(0, 8), this);

            StartBoard[1, 0] = new Pawn(PlayerColor.Black, new Cell(1, 0), this);
            StartBoard[1, 1] = new Pawn(PlayerColor.Black, new Cell(1, 1), this);
            StartBoard[1, 2] = new Pawn(PlayerColor.Black, new Cell(1, 2), this);
            StartBoard[1, 3] = new Pawn(PlayerColor.Black, new Cell(1, 3), this);
            StartBoard[1, 4] = new Pawn(PlayerColor.Black, new Cell(1, 4), this);
            StartBoard[1, 5] = new Pawn(PlayerColor.Black, new Cell(1, 5), this);
            StartBoard[1, 6] = new Pawn(PlayerColor.Black, new Cell(1, 6), this);
            StartBoard[1, 7] = new Pawn(PlayerColor.Black, new Cell(1, 7), this);
            StartBoard[1, 8] = new Pawn(PlayerColor.Black, new Cell(1, 8), this);

            #endregion

            #region White pieces initialisation

            StartBoard[7, 0] = new Pawn(PlayerColor.White, new Cell(7, 0), this);
            StartBoard[7, 1] = new Pawn(PlayerColor.White, new Cell(7, 1), this);
            StartBoard[7, 2] = new Pawn(PlayerColor.White, new Cell(7, 2), this);
            StartBoard[7, 3] = new Pawn(PlayerColor.White, new Cell(7, 3), this);
            StartBoard[7, 4] = new Pawn(PlayerColor.White, new Cell(7, 4), this);
            StartBoard[7, 5] = new Pawn(PlayerColor.White, new Cell(7, 5), this);
            StartBoard[7, 6] = new Pawn(PlayerColor.White, new Cell(7, 6), this);
            StartBoard[7, 7] = new Pawn(PlayerColor.White, new Cell(7, 7), this);
            StartBoard[7, 8] = new Pawn(PlayerColor.White, new Cell(7, 8), this);

            StartBoard[8, 0] = new Rook(PlayerColor.White, new Cell(8, 0), this);
            StartBoard[8, 1] = new Bishop(PlayerColor.White, new Cell(8, 1), this);
            StartBoard[8, 2] = new Knight(PlayerColor.White, new Cell(8, 2), this);
            StartBoard[8, 3] = new Prince(PlayerColor.White, new Cell(8, 3), this);
            StartBoard[8, 4] = new King(PlayerColor.White, new Cell(8, 4), this);
            StartBoard[8, 5] = new Queen(PlayerColor.White, new Cell(8, 5), this);
            StartBoard[8, 6] = new Bishop(PlayerColor.White, new Cell(8, 6), this);
            StartBoard[8, 7] = new Knight(PlayerColor.White, new Cell(8, 7), this);
            StartBoard[8, 8] = new Rook(PlayerColor.White, new Cell(8, 8), this);

            #endregion

            WhiteKing = StartBoard[8, 4];
            BlackKing = StartBoard[0, 4];
            WhitePrince = StartBoard[8, 3];
            BlackPrince = StartBoard[0, 5];

            Reset();
        }

        private const int length = 9;
        
        public int Length => length;
        public Piece[,] StartBoard { get; }
        public Piece[,] Board { get; }
        public Piece WhiteKing { get; set; }
        public Piece BlackKing { get; set; }
        public Piece WhitePrince { get; set; }
        public Piece BlackPrince { get; set; }

        public Piece this[Cell cell]
        {
            get => cell.IsValid
                ? Board[cell.Row, cell.Col]
                : null;
        }

        public void Reset()
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    Board[i, j] = StartBoard[i, j];

                    if (Board[i, j] != null)
                        Board[i, j].Cell = new Cell(i, j);
                }
            }
        }
    }
}