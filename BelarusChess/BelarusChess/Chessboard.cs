using BelarusChess.Figures;
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
        public Figure[,] Board { get; private set; }
        public int Length { get => Board.GetLength(0); }

        public Chessboard()
        {
            Board = new Figure[9, 9];
            // Black figures
            Board[0, 0] = new Rook(PlayerColor.Black, Cell.Create(0, 0));
            Board[0, 1] = new Knight(PlayerColor.Black, Cell.Create(0, 1));
            Board[0, 2] = new Bishop(PlayerColor.Black, Cell.Create(0, 2));
            Board[0, 3] = new Queen(PlayerColor.Black, Cell.Create(0, 3));
            Board[0, 4] = new King(PlayerColor.Black, Cell.Create(0, 4));
            Board[0, 5] = new Prince(PlayerColor.Black, Cell.Create(0, 5));
            Board[0, 6] = new Knight(PlayerColor.Black, Cell.Create(0, 6));
            Board[0, 7] = new Bishop(PlayerColor.Black, Cell.Create(0, 7));
            Board[0, 8] = new Rook(PlayerColor.Black, Cell.Create(0, 8));

            Board[1, 0] = new BlackPawn(Cell.Create(1, 0));
            Board[1, 1] = new BlackPawn(Cell.Create(1, 1));
            Board[1, 2] = new BlackPawn(Cell.Create(1, 2));
            Board[1, 3] = new BlackPawn(Cell.Create(1, 3));
            Board[1, 4] = new BlackPawn(Cell.Create(1, 4));
            Board[1, 5] = new BlackPawn(Cell.Create(1, 5));
            Board[1, 6] = new BlackPawn(Cell.Create(1, 6));
            Board[1, 7] = new BlackPawn(Cell.Create(1, 7));
            Board[1, 8] = new BlackPawn(Cell.Create(1, 8));

            // Fills middle part of the chessboard
            for (int i = 2; i < 7; i++)
                for (int j = 0; j < 9; j++)
                    Board[i, j] = null;

            // White figures
            Board[7, 0] = new WhitePawn(Cell.Create(7, 0));
            Board[7, 1] = new WhitePawn(Cell.Create(7, 1));
            Board[7, 2] = new WhitePawn(Cell.Create(7, 2));
            Board[7, 3] = new WhitePawn(Cell.Create(7, 3));
            Board[7, 4] = new WhitePawn(Cell.Create(7, 4));
            Board[7, 5] = new WhitePawn(Cell.Create(7, 5));
            Board[7, 6] = new WhitePawn(Cell.Create(7, 6));
            Board[7, 7] = new WhitePawn(Cell.Create(7, 7));
            Board[7, 8] = new WhitePawn(Cell.Create(7, 8));

            Board[8, 0] = new Rook(PlayerColor.White, Cell.Create(8, 0));
            Board[8, 1] = new Bishop(PlayerColor.White, Cell.Create(8, 1));
            Board[8, 2] = new Knight(PlayerColor.White, Cell.Create(8, 2));
            Board[8, 3] = new Prince(PlayerColor.White, Cell.Create(8, 3));
            Board[8, 4] = new King(PlayerColor.White, Cell.Create(8, 4));
            Board[8, 5] = new Queen(PlayerColor.White, Cell.Create(8, 5));
            Board[8, 6] = new Bishop(PlayerColor.White, Cell.Create(8, 6));
            Board[8, 7] = new Knight(PlayerColor.White, Cell.Create(8, 7));
            Board[8, 8] = new Rook(PlayerColor.White, Cell.Create(8, 8));
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
    }
}
