﻿using BelarusChess.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BelarusChess
{
    public class Chessboard
    {
        private readonly Figure[,] startBoard;

        public Figure[,] Board { get; private set; }
        public int Length { get => Board.GetLength(0); }

        public Chessboard()
        {
            startBoard = new Figure[9, 9];

            // Black figures
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

            // White figures
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

            Reset();
        }
        public Figure this [Cell cell]
        {
            get => (cell == null ? null : Board[cell.Row, cell.Col]);
            set
            {
                if (cell == null)
                    return;
                if (value != null)
                    value.Cell = Cell.Create(cell.Row, cell.Col);
                Board[cell.Row, cell.Col] = value;
            }
        }

        public void Reset()
        {
            Board = (Figure[,])startBoard.Clone();
            // Resets Figures location
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    if (Board[i, j] != null)
                        Board[i, j].Cell = Cell.Create(i, j);
                }
            }
        }
    }
}
