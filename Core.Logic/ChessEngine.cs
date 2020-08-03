using BelarusChess.Core.Entities;
using BelarusChess.Core.Entities.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelarusChess.Core.Logic
{
    public class ChessEngine
    {
        private Chessboard _chessboard;
        private Chessboard _savedChessboard;

        public ChessEngine(Chessboard chessboard)
        {
            _chessboard = chessboard;
            _savedChessboard = chessboard;
        }

        public IEnumerable<Cell> FindAvailableCells(Piece piece)
        {
            return piece.GetAvailableCells();
        }

        public void MakeMove(Piece piece, Cell cell)
        {
            _chessboard.MakeMove(piece, cell);
        }

        public PlayerState GetPlayerState(PlayerColor player)
        {
            bool isCheck = IsCheck(player);
            bool isStalemate = IsStalemate(player);

            if (isCheck == true && isStalemate == true)
                return PlayerState.Checkmate;
            else if (isCheck == true && isStalemate == false)
                return PlayerState.Check;
            else if (isCheck == false && isStalemate == true)
                return PlayerState.Stalemate;
            else
                return PlayerState.Regular;
        }

        public void ResetChessboard()
        {
            _chessboard.Reset();
        }

        public Piece GetPieceAt(Cell cell)
        {
            return _chessboard[cell];
        }

        public IEnumerable<Piece> GetAllPieces()
        {
            var pieces = new List<Piece>();

            foreach (var piece in _chessboard.StartBoard)
                if (piece != null)
                    pieces.Add(piece);

            return pieces;
        }

        private bool IsCheck(PlayerColor player)
        {
            Piece prince = _chessboard.GetPrince(player);
            // If prince is alive there is no check
            if (prince != null)
                return false;

            var opponentPlayerAvailableCells = new List<Cell>();
            foreach (Piece piece in _chessboard.Board)
            {
                // If piece is opponent's piece
                if (piece != null && piece.Color != player)
                    opponentPlayerAvailableCells.AddRange(piece.GetAvailableCells());
            }

            Piece king = _chessboard.GetKing(player);
            return opponentPlayerAvailableCells.Contains(king.Cell);
        }

        private bool IsStalemate(PlayerColor player)
        {
            var currentPlayerAvailableCells = new List<Cell>();
            foreach (Piece piece in _chessboard.Board)
            {
                // If piece is a friendly piece
                if (piece != null && piece.Color == player)
                {
                    piece.HasToRaiseOnCellChangeEvent = false;

                    SaveChessboard();
                    foreach (Cell cell in piece.GetAvailableCells())
                    {
                        MakeMove(piece, cell);
                        if (!IsCheck(player))
                            currentPlayerAvailableCells.Add(cell);
                        RestoreChessboard();
                    }

                    piece.HasToRaiseOnCellChangeEvent = true;
                }
            }
            
            return currentPlayerAvailableCells.Count == 0;
        }

        private bool IsThrone(PlayerColor player)
        {
            return default;
        }

        private bool IsThroneMine(PlayerColor player)
        {
            return default;
        }

        private void SaveChessboard()
        {
            for (int i = 0; i < Chessboard.Length; i++)
            {
                for (int j = 0; j < Chessboard.Length; j++)
                {
                    var cell = new Cell(i, j);
                    _savedChessboard[cell] = _chessboard[cell];
                }
            }
        }

        private void RestoreChessboard()
        {
            for (int i = 0; i < Chessboard.Length; i++)
            {
                for (int j = 0; j < Chessboard.Length; j++)
                {
                    var cell = new Cell(i, j);
                    _chessboard[cell] = _savedChessboard[cell];
                }
            }
        }
    }
}
