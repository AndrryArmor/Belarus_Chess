using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelarusChess.Engine.Rules
{
    /// <summary> Rule for avoiding check for friendly <see cref="Pieces.King"/> </summary>
    public class CheckAvoidanceRule : IRule
    {
        public CheckAvoidanceRule() { }

        public List<Cell> ValidCells(Piece piece, Chessboard chessboard)
        {
            if (chessboard[piece.Cell] != piece)
                throw new ArgumentException("Piece must belong to the board");

            var validCells = new List<Cell>();
            Cell pieceStartCell = piece.Cell;
            IRule movementRule = GetMovementRuleFromPiece(piece);
            List<Cell> pieceValidCells = movementRule.ValidCells(piece, chessboard);

            foreach (var validCell in pieceValidCells)
            {
                // Saves piece before move
                Piece beatenPiece = chessboard[validCell];

                // Moves piece to the cell
                chessboard[validCell] = piece;
                piece.Cell = validCell;
                chessboard[pieceStartCell] = null;

                if (IsCheck(piece.Color, chessboard) == false)
                    validCells.Add(validCell);

                // Undo the move
                chessboard[pieceStartCell] = piece;
                piece.Cell = pieceStartCell;
                chessboard[validCell] = beatenPiece;
            }

            return validCells;
        }

        private bool IsCheck(PlayerColor color, Chessboard chessboard)
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
            }

            return movementRule;
        }
    }
}
