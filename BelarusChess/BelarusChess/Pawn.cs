using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BelarusChess
{
    public class Pawn : Figure
    {
        public Pawn(Image image, Chess.PlayerColor color)
        {
            Image = image;
            Color = color;
            Type = Chess.FigureType.Pawn;
        }
        public override Moves[,] Moves(Chess chess)
        {
            if (chess. == PlayerColor.White && rowImage > 0)
            {
                if (chessBoard[rowImage - 1, columnImage] == null)
                {
                    movesBoard[rowImage - 1, columnImage] = NewImage(attackImageUri, rowImage - 1, columnImage, 3);
                    if (rowImage == 7 && chessBoard[rowImage - 2, columnImage] == null)     // En passant
                    {
                        movesBoard[rowImage - 2, columnImage] = NewImage(attackImageUri, rowImage - 2, columnImage, 3);
                    }
                }
                if (columnImage < 8 && chessBoard[rowImage - 1, columnImage + 1] != null && PlayerColor(chessBoard[rowImage - 1, columnImage + 1]) != player)
                {
                    movesBoard[rowImage - 1, columnImage + 1] = NewImage(attackFigureImageUri, rowImage - 1, columnImage + 1, 3);
                }
                if (columnImage > 0 && chessBoard[rowImage - 1, columnImage - 1] != null && PlayerColor(chessBoard[rowImage - 1, columnImage - 1]) != player)
                {
                    movesBoard[rowImage - 1, columnImage - 1] = NewImage(attackFigureImageUri, rowImage - 1, columnImage - 1, 3);
                }
            }
            else if (player == PlayerColor.Black && rowImage < 8)
            {
                if (chessBoard[rowImage + 1, columnImage] == null)
                {
                    movesBoard[rowImage + 1, columnImage] = NewImage(attackImageUri, rowImage + 1, columnImage, 3);
                    if (rowImage == 1 && chessBoard[rowImage + 2, columnImage] == null)     // En passant
                    {
                        movesBoard[rowImage + 2, columnImage] = NewImage(attackImageUri, rowImage + 2, columnImage, 3);
                    }
                }
                if (columnImage < 8 && chessBoard[rowImage + 1, columnImage + 1] != null && PlayerColor(chessBoard[rowImage + 1, columnImage + 1]) != player)
                {
                    movesBoard[rowImage + 1, columnImage + 1] = NewImage(attackFigureImageUri, rowImage + 1, columnImage + 1, 3);
                }
                if (columnImage > 0 && chessBoard[rowImage + 1, columnImage - 1] != null && PlayerColor(chessBoard[rowImage + 1, columnImage - 1]) != player)
                {
                    movesBoard[rowImage + 1, columnImage - 1] = NewImage(attackFigureImageUri, rowImage + 1, columnImage - 1, 3);
                }
            }
        }
    }
}
