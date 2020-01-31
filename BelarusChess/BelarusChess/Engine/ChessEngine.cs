using BelarusChess.Engine.Rules;
using System;
using System.Collections.Generic;

namespace BelarusChess.Engine
{
    public class ChessEngine
    {
        private readonly Chessboard chessboard;

        public GameState WhitePlayerState { get; private set; }
        public GameState BlackPlayerState { get; private set; }

        public ChessEngine(EventHandler<ChessboardPieceMovedEventArgs> eventHandler)
        {
            chessboard = new Chessboard(eventHandler);
            WhitePlayerState = BlackPlayerState = GameState.Regular;
        }

        public void ResetChessboard()
        {
            chessboard.Reset();
        }

        public Piece GetPieceAt(Cell cell)
        {
            return chessboard[cell];
        }

        public List<Cell> GetPieceValidCells(Piece piece)
        {
            IRule movementRule = GetMovementRuleFromPiece(piece);
            return movementRule.ValidCells(piece, chessboard);
        }

        public void MakeMove(Piece piece, Cell cell)
        {
            Cell pieceCell = piece.Cell;
            chessboard[cell] = piece;
            chessboard[pieceCell] = null;

            UpdateGameState(piece.Color.Next());
        }

        private GameState UpdateGameState(PlayerColor color)
        {
            bool isCheck = IsCheck(color);
            bool isStalemate = IsStalemate(color);
            if (isCheck == true && isStalemate == true)
                return GameState.Checkmate;
            else if (isCheck == true && isStalemate == false)
                return GameState.Check;
            else if (isCheck == false && isStalemate == true)
                return GameState.Stalemate;
            else
                return GameState.Regular;
        }

        private bool IsCheck(PlayerColor color)
        {
            Piece prince = (color == PlayerColor.White ? chessboard.WhitePrince : chessboard.BlackPrince);
            if (prince != null)
                return false;

            var opponentPlayerValidCells = new List<Cell>();
            foreach (var boardPiece in chessboard.Board)
            {
                // Continue if cell is empty or contains friendly figure
                if (boardPiece == null || boardPiece.Color == color)
                    continue;

                IRule movementRule = GetMovementRuleFromPiece(boardPiece);
                opponentPlayerValidCells.AddRange(movementRule.ValidCells(boardPiece, chessboard));
            }

            Piece king = (color == PlayerColor.White ? chessboard.WhiteKing : chessboard.BlackKing);
            return opponentPlayerValidCells.Contains(king.Cell);
        }

        private bool IsStalemate(PlayerColor color)
        {
            var currentPlayerValidCells = new List<Cell>();
            foreach (var boardPiece in chessboard.Board)
            {
                // Continue if cell is empty or contains opponent's figure
                if (boardPiece == null || boardPiece.Color != color)
                    continue;

                IRule movementRule = GetMovementRuleFromPiece(boardPiece);
                currentPlayerValidCells.AddRange(movementRule.ValidCells(boardPiece, chessboard));
            }
            return (currentPlayerValidCells.Count == 0 ? true : false);
        }

        private IRule GetMovementRuleFromPiece(Piece piece)
        {
            IRule movementRule = null;

            switch (piece.Type)
            {
                case PieceType.Rook:
                    movementRule = new RookMovementRule();
                    break;
                case PieceType.Bishop:
                    movementRule = new BishopMovementRule();
                    break;
                case PieceType.Knight:
                    movementRule = new KnightMovementRule();
                    break;
                case PieceType.Prince:
                    movementRule = new PrinceMovementRule();
                    break;
                case PieceType.Queen:
                    movementRule = new QueenMovementRule();
                    break;
                case PieceType.King:
                    movementRule = new KingMovementRule();
                    break;
                case PieceType.Pawn:
                    movementRule = new PawnMovementRule();
                    break;
                case PieceType.WhitePawn:
                    movementRule = new PawnMovementRule();
                    break;
                case PieceType.BlackPawn:
                    movementRule = new PawnMovementRule();
                    break;
            }

            return movementRule;
        }
    }
}
