using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BelarusChess
{
    public partial class MainWindow : Window
    {
        private Figure[,] chessBoard;
        private Image[,] movesBoard;                    // A massive of Images that shows the location of the attacked fields
        private Figure choosedFigure;
        private PlayerColor currentColor;
        private Image tempTile;
        private bool isOnlyKingOrPrinceWhite;
        private bool isOnlyKingOrPrinceBlack;
        private int movesSinceThroneOrRokash;

        // Enumerators
        public enum PlayerColor
        {
            White, Black
        }
        private PlayerColor Next(PlayerColor color)
        {
            if (color == PlayerColor.Black)
                return PlayerColor.White;
            else
                return PlayerColor.Black;
        }
        public enum FigureType
        {
            Rook, Bishop, Knight, Prince, Queen, King, Pawn
        }
        public enum MoveType
        {
            Regular, Check, Checkmate, Inauguration, Throne, ThroneMine
        }

        public void NewGame()
        {
            ClearMoves();
            currentColor = PlayerColor.White;
            InitializeImages(figures);
            movesSinceThroneOrRokash = 0;
            isOnlyKingOrPrinceBlack = false;
            isOnlyKingOrPrinceWhite = false;
            // Start positions of black figures
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    chessBoard[i, j] = figures[i, j];
                    chessBoard[i, j].Image.Visibility = Visibility.Visible;
                    figures[i, j].Image.Margin = new Thickness(xMargin + j * edge, yMargin + i * edge, 0, 0);
                }
            }
            // Clearing middle part of the chess board
            for (int i = 2; i < 7; i++)
                for (int j = 0; j < 9; j++)
                    chessBoard[i, j] = null;
            // Start positions of white figures
            for (int i = 7; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    chessBoard[i, j] = figures[i - 5, j];
                    chessBoard[i, j].Image.Visibility = Visibility.Visible;
                    figures[i - 5, j].Image.Margin = new Thickness(xMargin + j * edge, yMargin + i * edge, 0, 0);
                }
            }
        }

        public int FindMoves(Image imageFigure, bool isCheckmateCheck = false)
        {
            /// Finds the figure which is represented by imageFigure
            int rowClicked = (int)((Point)imageFigure.Tag).Y;
            int columnClicked = (int)((Point)imageFigure.Tag).X;
            Figure figure = chessBoard[rowClicked, columnClicked];
            /// If not turn of current figure
            if (currentColor != figure.Color)
                return 0;
            if (choosedFigure != null)
                ClearMoves();

            choosedFigure = figure;
            if (isCheckmateCheck == false)
                movesBoard[rowClicked, columnClicked] = NewImage(choosedFigureUri, rowClicked, columnClicked, 1);

            Moves[,] moves = choosedFigure.Moves();
            /// Special case for pawn
            if (choosedFigure.Type == FigureType.Pawn)
            {
                PawnMoves(moves, rowClicked, columnClicked);
                return 0;
            }
            int accessableMoves = 0;
            /// Finds accessable moves for figure
            for (int i = 0; i < moves.GetLength(0); i++)
            {
                for (int j = 0; j < moves.GetLength(1); j++)
                {
                    int row = rowClicked + moves[i, j].Y;
                    int column = columnClicked + moves[i, j].X;
                    if (row < 0 || row > 8 || column < 0 || column > 8)
                        break;
                    /// If not prince goes onto the throne tile
                    if (figure.Type != FigureType.Prince && row == 4 && column == 4)    
                    {
                        if (chessBoard[row, column] == null)
                        {
                            /// If move causes check
                            if (IsCheck(rowClicked, columnClicked, row, column) == MoveType.Check)
                                break;
                            if (isCheckmateCheck == false)
                                movesBoard[row, column] = NewImage(attackImageUri, row, column, 3);
                            accessableMoves++;
                        }
                        else if (chessBoard[row, column].Color != currentColor)
                        {
                            /// If move causes check
                            if (IsCheck(rowClicked, columnClicked, row, column) == MoveType.Check)
                                break;
                            if (isCheckmateCheck == false)
                                movesBoard[row, column] = NewImage(attackFigureImageUri, row, column, 3);
                            accessableMoves++;
                        }
                        break;
                    }
                   
                    /// If empty tile
                    if (chessBoard[row, column] == null)
                    {
                        /// If move causes check
                        if (IsCheck(rowClicked, columnClicked, row, column) == MoveType.Check)
                            break;
                        if (isCheckmateCheck == false)
                            movesBoard[row, column] = NewImage(attackImageUri, row, column, 3);
                        accessableMoves++;
                    }
                    /// If tile contains opponent's figure
                    else if (chessBoard[row, column].Color != currentColor)
                    {
                        /// If move causes check
                        if (IsCheck(rowClicked, columnClicked, row, column) == MoveType.Check)
                            break;
                        if (isCheckmateCheck == false)
                            movesBoard[row, column] = NewImage(attackFigureImageUri, row, column, 3);
                        accessableMoves++;
                        break;
                    }
                    else break;
                }
            }
            return accessableMoves;
        }
        private void PawnMoves(Moves[,] moves, int row, int column)
        {
            if (currentColor == PlayerColor.White && row > 0)
            {
                // Move up playing white figures
                if (chessBoard[row - 1, column] == null)
                {
                    if (IsCheck(row, column, row - 1, column) == MoveType.Regular)
                    {
                        movesBoard[row - 1, column] = NewImage(attackImageUri, row - 1, column, 3);
                        // Double move
                        if (row == 7 && chessBoard[row - 2, column] == null)
                            movesBoard[row - 2, column] = NewImage(attackImageUri, row - 2, column, 3);
                    }
                }
                // Beat up-right
                if (column < 8 && chessBoard[row - 1, column + 1] != null && chessBoard[row - 1, column + 1].Color != currentColor)
                {
                    if (IsCheck(row, column, row - 1, column + 1) == MoveType.Regular)
                    {
                        movesBoard[row - 1, column + 1] = NewImage(attackFigureImageUri, row - 1, column + 1, 3);
                    }
                }
                // Beat up-left
                if (column > 0 && chessBoard[row - 1, column - 1] != null && chessBoard[row - 1, column - 1].Color != currentColor)
                {
                    if (IsCheck(row, column, row - 1, column - 1) == MoveType.Regular)
                    {
                        movesBoard[row - 1, column - 1] = NewImage(attackFigureImageUri, row - 1, column - 1, 3);
                    }
                }
            }
            else if (currentColor == PlayerColor.Black && row < 8)
            {
                // Move down playing white figures
                if (chessBoard[row + 1, column] == null)
                {
                    if (IsCheck(row, column, row + 1, column) == MoveType.Regular)
                    {
                        movesBoard[row + 1, column] = NewImage(attackImageUri, row + 1, column, 3);
                        // Double move
                        if (row == 1 && chessBoard[row + 2, column] == null)
                            movesBoard[row + 2, column] = NewImage(attackImageUri, row + 2, column, 3);
                    }
                }
                // Beat down-right
                if (column < 8 && chessBoard[row + 1, column + 1] != null && chessBoard[row + 1, column + 1].Color != currentColor)
                {
                    if (IsCheck(row, column, row + 1, column + 1) == MoveType.Regular)
                    {
                        movesBoard[row + 1, column + 1] = NewImage(attackFigureImageUri, row + 1, column + 1, 3);
                    }
                }
                // Beat down-left
                if (column > 0 && chessBoard[row + 1, column - 1] != null && chessBoard[row + 1, column - 1].Color != currentColor)
                {
                    if (IsCheck(row, column, row + 1, column - 1) == MoveType.Regular)
                    {
                        movesBoard[row + 1, column - 1] = NewImage(attackFigureImageUri, row + 1, column - 1, 3);
                    }
                }
            }
        }

        public void MakeMove(int row, int column)
        {
            // Deletes the old location of the figure
            int oldRow = (int)((Point)choosedFigure.Image.Tag).Y;
            int oldColumn = (int)((Point)choosedFigure.Image.Tag).X;
            /*if (movesBoard[oldRow, oldColumn] != null)
            {
                grid.Children.Remove(movesBoard[oldRow, oldColumn]);
                movesBoard[oldRow, oldColumn] = null;
            }*/
            chessBoard[oldRow, oldColumn] = null;
            Figure oldFigure = chessBoard[row, column];
            // Moves the choosed figure
            if (chessBoard[row, column] != null)
                chessBoard[row, column].Image.Visibility = Visibility.Hidden;
            chessBoard[row, column] = choosedFigure;
            chessBoard[row, column].Image.Tag = new Point(column, row);
            chessBoard[row, column].Image.Margin = new Thickness(xMargin + column * edge, yMargin + row * edge, 0, 0);
            currentColor = Next(currentColor);
            CheckForSpecialCases(oldFigure);
        }
        private void CheckForSpecialCases(Figure figure)
        {
            string message = "";
            int accessibleMoves = 0;
            if (IsInauguration(figure) == MoveType.Inauguration)
                message += "Інавгурація ";
            if (IsThroneOrThroneMine() == MoveType.Throne)
            {
                message = "Трон! ";
            }
            if (IsThroneOrThroneMine() == MoveType.ThroneMine)
            {
                isStarted = false;
                buttonFinishGame.IsEnabled = false;
                buttonNewGame.IsEnabled = true;
                timer.Stop();
                message = "Трон мій! ";
                MessageBox.Show((currentColor == PlayerColor.Black ? "Чорні" : "Білі") + " перемогли!");
            }
            if (IsCheck() == MoveType.Check)
            {
                Figure king;
                if (currentColor == PlayerColor.Black)
                    king = figures[0, 4];
                else
                    king = figures[3, 4];
                accessibleMoves = FindMoves(king.Image, true);
                if (accessibleMoves == 0)
                {
                    isStarted = false;
                    buttonFinishGame.IsEnabled = false;
                    buttonNewGame.IsEnabled = true;
                    timer.Stop();
                    message += "Шах і мат! ";
                    MessageBox.Show((currentColor == PlayerColor.Black ? "Чорні" : "Білі") + " перемогли!");
                }
                else
                {
                    movesSinceThroneOrRokash = 0;
                    message += "Шах! ";
                }
            }

            if (currentColor == PlayerColor.White)
            {
                labelWhitePlayer.Content = message;
                labelBlackPlayer.Content = "";
            }
            else
            {
                labelBlackPlayer.Content = message;
                labelWhitePlayer.Content = "";
            }

            if (movesSinceThroneOrRokash == 1)
                movesSinceThroneOrRokash = 2;
        }
        private MoveType IsInauguration(Figure figure)
        {
            // Checks whether two contenders (претенденти) of throne are both alive (king and prince)
            if (figure != null && figure.Color == PlayerColor.Black && isOnlyKingOrPrinceBlack == false &&
                (figure.Type == FigureType.King || figure.Type == FigureType.Prince))
            {
                isOnlyKingOrPrinceBlack = true;
                // If black king was killed
                if (figure.Type == FigureType.King)
                {
                    int rowKing = (int)((Point)figures[0, 4].Image.Tag).Y;
                    int columnKing = (int)((Point)figures[0, 4].Image.Tag).X;
                    int rowPrince = (int)((Point)figures[0, 5].Image.Tag).Y;
                    int columnPrince = (int)((Point)figures[0, 5].Image.Tag).X;

                    figure.Image.Tag = figures[0, 5].Image.Tag;
                    figure.Image.Margin = new Thickness(xMargin + columnPrince * edge, yMargin + rowPrince * edge, 0, 0);
                    chessBoard[rowPrince, columnPrince].Image.Visibility = Visibility.Hidden;
                    chessBoard[rowPrince, columnPrince] = figure;
                    chessBoard[rowPrince, columnPrince].Image.Visibility = Visibility.Visible;
                    return MoveType.Inauguration;
                }
            }
            else if (figure != null && figure.Color == PlayerColor.White && isOnlyKingOrPrinceWhite == false &&
                     (figure.Type == FigureType.King || figure.Type == FigureType.Prince))
            {
                isOnlyKingOrPrinceWhite = true;
                // If white king was killed
                if (figure.Type == FigureType.King)
                {
                    int rowKing = (int)((Point)figures[0, 4].Image.Tag).Y;
                    int columnKing = (int)((Point)figures[0, 4].Image.Tag).X;
                    int rowPrince = (int)((Point)figures[3, 3].Image.Tag).Y;
                    int columnPrince = (int)((Point)figures[3, 3].Image.Tag).X;

                    figure.Image.Tag = chessBoard[rowPrince, columnPrince].Image.Tag;
                    figure.Image.Margin = new Thickness(xMargin + columnPrince * edge, yMargin + rowPrince * edge, 0, 0);
                    chessBoard[rowPrince, columnPrince].Image.Visibility = Visibility.Hidden;
                    chessBoard[rowPrince, columnPrince] = figure;
                    chessBoard[rowPrince, columnPrince].Image.Visibility = Visibility.Visible;
                    return MoveType.Inauguration;
                }
            }
            return MoveType.Regular;
        }
        private MoveType IsCheck(int rowFrom = 0, int columnFrom = 0, int rowTo = 0, int columnTo = 0)
        {
            King currentKing;
            if (currentColor == PlayerColor.Black && isOnlyKingOrPrinceBlack == true)
                currentKing = (King)figures[0, 4];
            else if (currentColor == PlayerColor.White && isOnlyKingOrPrinceWhite == true)
                currentKing = (King)figures[3, 4];
            else
                return MoveType.Regular;
            int rowKing = (int)((Point)currentKing.Image.Tag).Y;
            int columnKing = (int)((Point)currentKing.Image.Tag).X;
            Figure[,] tempChessBoard = chessBoard.Clone() as Figure[,];
            if (rowFrom != rowTo || columnFrom != columnTo)
            {
                tempChessBoard[rowTo, columnTo] = tempChessBoard[rowFrom, columnFrom];
                tempChessBoard[rowFrom, columnFrom] = null;
                if (rowKing == rowFrom && columnKing == columnFrom)
                {
                    rowKing = rowTo;
                    columnKing = columnTo;
                }
            }

            Moves[,] rook = new Rook().Moves();
            Moves[,] bishop = new Bishop().Moves();
            Moves[,] knight = new Knight().Moves();
            Moves[,] prince = new Prince().Moves();
            Moves[,] queen = new Queen().Moves();
            Moves[,] king = new King().Moves();
            // Finds at least one figure that attacks king (order is important!)
            Moves[][,] figuresMoves = new Moves[][,] {rook, bishop, knight, prince, queen, king };
            for (int type = 0; type < 6; type++)
            {
                Moves[,] moves = figuresMoves[type];
                for (int i = 0; i < moves.GetLength(0); i++)
                {
                    for (int j = 0; j < moves.GetLength(1); j++)
                    {
                        int row = rowKing + moves[i, j].Y;
                        int column = columnKing + moves[i, j].X;
                        if (row < 0 || row > 8 || column < 0 || column > 8)
                            break;
                        /// If figure goes onto the throne tile
                        if (row == 4 && column == 4)
                        {
                            if (tempChessBoard[row, column] != null && 
                                (int)(tempChessBoard[row, column].Type) == type &&
                                tempChessBoard[row, column].Color != currentColor)
                            {
                                return MoveType.Check;
                            }
                            break;
                        }
                        /// If there is a figure
                        if (tempChessBoard[row, column] != null)
                        {
                            if ((int)(tempChessBoard[row, column].Type) == type && tempChessBoard[row, column].Color != currentColor)
                            {
                                return MoveType.Check;
                            }
                            break;
                        }
                    }
                }
            }
            // Pawn check
            if (currentColor == PlayerColor.White && rowKing > 0)
            {
                // Is beaten from up-right black pawn
                if (columnKing < 8 && tempChessBoard[rowKing - 1, columnKing + 1] != null &&
                    tempChessBoard[rowKing - 1, columnKing + 1].Type == FigureType.Pawn &&
                    tempChessBoard[rowKing - 1, columnKing + 1].Color != currentColor)
                { return MoveType.Check; }
                // Is beaten from up-left black pawn
                else if (columnKing > 0 && tempChessBoard[rowKing - 1, columnKing - 1] != null &&
                    tempChessBoard[rowKing - 1, columnKing - 1].Type == FigureType.Pawn &&
                    tempChessBoard[rowKing - 1, columnKing - 1].Color != currentColor)
                { return MoveType.Check; }
            }
            else if (currentColor == PlayerColor.Black && rowKing < 8)
            {
                // Is beaten from down-right white pawn
                if (columnKing < 8 && tempChessBoard[rowKing + 1, columnKing + 1] != null &&
                    tempChessBoard[rowKing + 1, columnKing + 1].Type == FigureType.Pawn &&
                    tempChessBoard[rowKing + 1, columnKing + 1].Color != currentColor)
                { return MoveType.Check; }
                // Is beaten from down-left white pawn
                else if (columnKing > 0 && tempChessBoard[rowKing + 1, columnKing - 1] != null &&
                    tempChessBoard[rowKing + 1, columnKing - 1].Type == FigureType.Pawn &&
                    tempChessBoard[rowKing + 1, columnKing - 1].Color != currentColor)
                { return MoveType.Check; }
            }
            return MoveType.Regular;                
        }
        private MoveType IsThroneOrThroneMine()
        {
            if (choosedFigure.Type == FigureType.King && ((Point)choosedFigure.Image.Tag).X == 4
                                                      && ((Point)choosedFigure.Image.Tag).Y == 4)
            {
                movesSinceThroneOrRokash++;
                return MoveType.Throne;
            }
            else if (movesSinceThroneOrRokash == 2)
                return MoveType.ThroneMine;
            else
                return MoveType.Regular;

        }

        private void ClearMoves()
        {
            grid.Children.Remove(tempTile);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    grid.Children.Remove(movesBoard[i, j]);
                    movesBoard[i, j] = null;
                }
            }
        }
    }
}
