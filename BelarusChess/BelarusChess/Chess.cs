using System.Windows;
using System.Windows.Controls;

namespace BelarusChess
{
    public partial class MainWindow : Window
    {
        /// <summary> Describes the chess board 9x9 for belarus chess </summary>
        private Figure[,] chessBoard;
        /// <summary> Describes the chess board which contains images of legal moves for some figure </summary>
        private Image[,] legalMovesBoard;
        private Figure choosedFigure;
        private PlayerColor currentColor;
        /// <summary> The cell of legal move under the mouse cursor </summary>
        private Image cellUnderCursor;
        private bool isOnlyKingOrPrinceWhite;
        private bool isOnlyKingOrPrinceBlack;
        private int movesSinceThroneOrRokash;

        /// <summary> Sets parameters to begin a new game </summary>
        private void NewGame()
        {
            ClearMoves();
            SetBeginChessBoard();
            currentColor = PlayerColor.White;
            movesSinceThroneOrRokash = 0;
            isOnlyKingOrPrinceBlack = false;
            isOnlyKingOrPrinceWhite = false;
        }

        /// <summary> Set chess board to the begin state </summary>
        private void SetBeginChessBoard()
        {
            // Black figures
            chessBoard[0, 0] = new Figure(imageBlackRook1, PlayerColor.Black, FigureType.Rook, new Cell(0, 0));
            chessBoard[0, 1] = new Figure(imageBlackKnight1, PlayerColor.Black, FigureType.Knight, new Cell(0, 1));
            chessBoard[0, 2] = new Figure(imageBlackBishopW, PlayerColor.Black, FigureType.Bishop, new Cell(0, 2));
            chessBoard[0, 3] = new Figure(imageBlackQueen, PlayerColor.Black, FigureType.Queen, new Cell(0, 3));
            chessBoard[0, 4] = new Figure(imageBlackKing, PlayerColor.Black, FigureType.King, new Cell(0, 4));
            chessBoard[0, 5] = new Figure(imageBlackPrince, PlayerColor.Black, FigureType.Prince, new Cell(0, 5));
            chessBoard[0, 6] = new Figure(imageBlackKnight2, PlayerColor.Black, FigureType.Knight, new Cell(0, 6));
            chessBoard[0, 7] = new Figure(imageBlackBishopB, PlayerColor.Black, FigureType.Bishop, new Cell(0, 7));
            chessBoard[0, 8] = new Figure(imageBlackRook2, PlayerColor.Black, FigureType.Rook, new Cell(0, 8));

            chessBoard[1, 0] = new Figure(imageBlackPawnA, PlayerColor.Black, FigureType.Pawn, new Cell(1, 0));
            chessBoard[1, 1] = new Figure(imageBlackPawnB, PlayerColor.Black, FigureType.Pawn, new Cell(1, 1));
            chessBoard[1, 2] = new Figure(imageBlackPawnC, PlayerColor.Black, FigureType.Pawn, new Cell(1, 2));
            chessBoard[1, 3] = new Figure(imageBlackPawnD, PlayerColor.Black, FigureType.Pawn, new Cell(1, 3));
            chessBoard[1, 4] = new Figure(imageBlackPawnE, PlayerColor.Black, FigureType.Pawn, new Cell(1, 4));
            chessBoard[1, 5] = new Figure(imageBlackPawnF, PlayerColor.Black, FigureType.Pawn, new Cell(1, 5));
            chessBoard[1, 6] = new Figure(imageBlackPawnG, PlayerColor.Black, FigureType.Pawn, new Cell(1, 6));
            chessBoard[1, 7] = new Figure(imageBlackPawnH, PlayerColor.Black, FigureType.Pawn, new Cell(1, 7));
            chessBoard[1, 8] = new Figure(imageBlackPawnI, PlayerColor.Black, FigureType.Pawn, new Cell(1, 8));

            // Clears middle part of the chess board
            for (int i = 2; i < 7; i++)
                for (int j = 0; j < 9; j++)
                    chessBoard[i, j] = null;

            // White figures
            chessBoard[7, 0] = new Figure(imageWhitePawnA, PlayerColor.White, FigureType.Pawn, new Cell(7, 0));
            chessBoard[7, 1] = new Figure(imageWhitePawnB, PlayerColor.White, FigureType.Pawn, new Cell(7, 1));
            chessBoard[7, 2] = new Figure(imageWhitePawnC, PlayerColor.White, FigureType.Pawn, new Cell(7, 2));
            chessBoard[7, 3] = new Figure(imageWhitePawnD, PlayerColor.White, FigureType.Pawn, new Cell(7, 3));
            chessBoard[7, 4] = new Figure(imageWhitePawnE, PlayerColor.White, FigureType.Pawn, new Cell(7, 4));
            chessBoard[7, 5] = new Figure(imageWhitePawnF, PlayerColor.White, FigureType.Pawn, new Cell(7, 5));
            chessBoard[7, 6] = new Figure(imageWhitePawnG, PlayerColor.White, FigureType.Pawn, new Cell(7, 6));
            chessBoard[7, 7] = new Figure(imageWhitePawnH, PlayerColor.White, FigureType.Pawn, new Cell(7, 7));
            chessBoard[7, 8] = new Figure(imageWhitePawnI, PlayerColor.White, FigureType.Pawn, new Cell(7, 8));

            chessBoard[8, 0] = new Figure(imageWhiteRook1, PlayerColor.White, FigureType.Rook, new Cell(8, 0));
            chessBoard[8, 1] = new Figure(imageWhiteBishopB, PlayerColor.White, FigureType.Bishop, new Cell(8, 1));
            chessBoard[8, 2] = new Figure(imageWhiteKnight1, PlayerColor.White, FigureType.Knight, new Cell(8, 2));
            chessBoard[8, 3] = new Figure(imageWhitePrince, PlayerColor.White, FigureType.Prince, new Cell(8, 3));
            chessBoard[8, 4] = new Figure(imageWhiteKing, PlayerColor.White, FigureType.King, new Cell(8, 4));
            chessBoard[8, 5] = new Figure(imageWhiteQueen, PlayerColor.White, FigureType.Queen, new Cell(8, 5));
            chessBoard[8, 6] = new Figure(imageWhiteBishopW, PlayerColor.White, FigureType.Bishop, new Cell(8, 6));
            chessBoard[8, 7] = new Figure(imageWhiteKnight2, PlayerColor.White, FigureType.Knight, new Cell(8, 7));
            chessBoard[8, 8] = new Figure(imageWhiteRook2, PlayerColor.White, FigureType.Rook, new Cell(8, 8));
        }

        private PlayerColor Next(PlayerColor color)
        {
            if (color == PlayerColor.Black)
                return PlayerColor.White;
            else
                return PlayerColor.Black;
        }

        /// <summary> Finds legal moves for clicked figure </summary>
        private int FindMoves(Image imageFigure, bool isCheckmateCheck = false)
        {
            /// Finds the figure which is represented by imageFigure
            int rowClicked = (int)((Point)imageFigure.Tag).Y;
            int columnClicked = (int)((Point)imageFigure.Tag).X;
            Figure figure = chessBoard[rowClicked, columnClicked];
            /// If not turn of current figure
            if (choosedFigure != null)
                ClearMoves();
            if (currentColor != figure.Color)
                return 0;

            choosedFigure = figure;
            if (isCheckmateCheck == false)
                legalMovesBoard[rowClicked, columnClicked] = NewImage(choosedFigureUri, rowClicked, columnClicked, 1);

            Move[,] moves = FigureMoves.GetFor(choosedFigure.Type, choosedFigure.Color);
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
                    int row = rowClicked + moves[i, j].Cols;
                    int column = columnClicked + moves[i, j].Rows;
                    if (row < 0 || row > 8 || column < 0 || column > 8)
                        break;
                    /// If not prince goes onto the throne cell
                    if (figure.Type != FigureType.Prince && row == 4 && column == 4)    
                    {
                        if (chessBoard[row, column] == null)
                        {
                            /// If move causes check
                            if (IsCheck(rowClicked, columnClicked, row, column) == MoveType.Check)
                                break;
                            if (isCheckmateCheck == false)
                                legalMovesBoard[row, column] = NewImage(attackImageUri, row, column, 3);
                            accessableMoves++;
                        }
                        else if (chessBoard[row, column].Color != currentColor)
                        {
                            /// If move causes check
                            if (IsCheck(rowClicked, columnClicked, row, column) == MoveType.Check)
                                break;
                            if (isCheckmateCheck == false)
                                legalMovesBoard[row, column] = NewImage(attackFigureImageUri, row, column, 3);
                            accessableMoves++;
                        }
                        break;
                    }
                   
                    /// If empty cell
                    if (chessBoard[row, column] == null)
                    {
                        /// If move causes check
                        if (IsCheck(rowClicked, columnClicked, row, column) == MoveType.Check)
                            break;
                        if (isCheckmateCheck == false)
                            legalMovesBoard[row, column] = NewImage(attackImageUri, row, column, 3);
                        accessableMoves++;
                    }
                    /// If cell contains opponent's figure
                    else if (chessBoard[row, column].Color != currentColor)
                    {
                        /// If move causes check
                        if (IsCheck(rowClicked, columnClicked, row, column) == MoveType.Check)
                            break;
                        if (isCheckmateCheck == false)
                            legalMovesBoard[row, column] = NewImage(attackFigureImageUri, row, column, 3);
                        accessableMoves++;
                        break;
                    }
                    else break;
                }
            }
            return accessableMoves;
        }

        /// <summary> Sets legal moves for pawn </summary>
        private void PawnMoves(Move[,] moves, int row, int column)
        {
            if (currentColor == PlayerColor.White && row > 0)
            {
                // Move up playing white figures
                if (chessBoard[row - 1, column] == null)
                {
                    if (IsCheck(row, column, row - 1, column) == MoveType.Regular)
                    {
                        legalMovesBoard[row - 1, column] = NewImage(attackImageUri, row - 1, column, 3);
                        // Double move
                        if (row == 7 && chessBoard[row - 2, column] == null)
                            legalMovesBoard[row - 2, column] = NewImage(attackImageUri, row - 2, column, 3);
                    }
                }
                // Beat up-right
                if (column < 8 && chessBoard[row - 1, column + 1] != null && chessBoard[row - 1, column + 1].Color != currentColor)
                {
                    if (IsCheck(row, column, row - 1, column + 1) == MoveType.Regular)
                    {
                        legalMovesBoard[row - 1, column + 1] = NewImage(attackFigureImageUri, row - 1, column + 1, 3);
                    }
                }
                // Beat up-left
                if (column > 0 && chessBoard[row - 1, column - 1] != null && chessBoard[row - 1, column - 1].Color != currentColor)
                {
                    if (IsCheck(row, column, row - 1, column - 1) == MoveType.Regular)
                    {
                        legalMovesBoard[row - 1, column - 1] = NewImage(attackFigureImageUri, row - 1, column - 1, 3);
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
                        legalMovesBoard[row + 1, column] = NewImage(attackImageUri, row + 1, column, 3);
                        // Double move
                        if (row == 1 && chessBoard[row + 2, column] == null)
                            legalMovesBoard[row + 2, column] = NewImage(attackImageUri, row + 2, column, 3);
                    }
                }
                // Beat down-right
                if (column < 8 && chessBoard[row + 1, column + 1] != null && chessBoard[row + 1, column + 1].Color != currentColor)
                {
                    if (IsCheck(row, column, row + 1, column + 1) == MoveType.Regular)
                    {
                        legalMovesBoard[row + 1, column + 1] = NewImage(attackFigureImageUri, row + 1, column + 1, 3);
                    }
                }
                // Beat down-left
                if (column > 0 && chessBoard[row + 1, column - 1] != null && chessBoard[row + 1, column - 1].Color != currentColor)
                {
                    if (IsCheck(row, column, row + 1, column - 1) == MoveType.Regular)
                    {
                        legalMovesBoard[row + 1, column - 1] = NewImage(attackFigureImageUri, row + 1, column - 1, 3);
                    }
                }
            }
        }

        /// <summary> Make a move </summary>
        private void MakeMove(int row, int column)
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
            {
                if (chessBoard[row, column].Type == FigureType.King)
                    movesSinceThroneOrRokash = 0;
                chessBoard[row, column].Image.Visibility = Visibility.Hidden;
            }
            chessBoard[row, column] = choosedFigure;
            chessBoard[row, column].Image.Tag = new Point(column, row);
            chessBoard[row, column].Image.Margin = new Thickness(xMargin + column * cellEdge, yMargin + row * cellEdge, 0, 0);
            currentColor = Next(currentColor);
            CheckForSpecialCases(oldFigure);
        }

        /// <summary> Checkes if there is special situations like check or checkmate </summary>
        private void CheckForSpecialCases(Figure figure)
        {
            string messageBlack = "";
            string messageWhite = "";
            int accessibleMoves = 0;
            if (IsInauguration(figure) == MoveType.Inauguration)
                messageBlack += "Інавгурація ";
            if (IsCheck() == MoveType.Check)
            {
                movesSinceThroneOrRokash = 0;
                Figure king;
                if (currentColor == PlayerColor.Black)
                    king = figures[0, 4];
                else
                    king = figures[3, 4];
                accessibleMoves = FindMoves(king.Image, true);
                if (accessibleMoves == 0)
                {
                    isGameStarted = false;
                    buttonFinishGame.IsEnabled = false;
                    buttonNewGame.IsEnabled = true;
                    oneSecond.Stop();
                    messageBlack += "Шах і мат! ";
                    MessageBox.Show((currentColor == PlayerColor.White ? "Чорні" : "Білі") + " перемогли!");
                }
                else
                    messageBlack += "Шах! ";
            }
            if (IsThroneOrThroneMine(figure) == MoveType.Throne)
            {
                messageWhite += "Трон! ";
            }
            if (IsThroneOrThroneMine(figure) == MoveType.ThroneMine)
            {
                isGameStarted = false;
                buttonFinishGame.IsEnabled = false;
                buttonNewGame.IsEnabled = true;
                oneSecond.Stop();
                if (messageBlack == "")
                    MessageBox.Show((currentColor == PlayerColor.Black ? "Чорні" : "Білі") + " перемогли!");
                messageBlack += "Трон мій! ";
            }

            if (currentColor == PlayerColor.White)
            {
                labelWhitePlayer.Content = messageBlack;
                labelBlackPlayer.Content = messageWhite;
            }
            else
            {
                labelBlackPlayer.Content = messageBlack;
                labelWhitePlayer.Content = messageWhite;
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
                    figure.Image.Margin = new Thickness(xMargin + columnPrince * cellEdge, yMargin + rowPrince * cellEdge, 0, 0);
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
                    figure.Image.Margin = new Thickness(xMargin + columnPrince * cellEdge, yMargin + rowPrince * cellEdge, 0, 0);
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

            Move[,] rook = FigureMoves.GetFor(FigureType.Rook, currentColor);
            Move[,] bishop = FigureMoves.GetFor(FigureType.Bishop, currentColor);
            Move[,] knight = FigureMoves.GetFor(FigureType.Knight, currentColor);
            Move[,] prince = FigureMoves.GetFor(FigureType.Prince, currentColor);
            Move[,] queen = FigureMoves.GetFor(FigureType.Queen, currentColor);
            Move[,] king = FigureMoves.GetFor(FigureType.King, currentColor);
            // Finds at least one figure that attacks king (order is important!)
            Move[][,] figuresMoves = new Move[][,] {rook, bishop, knight, prince, queen, king };
            for (int type = 0; type < 6; type++)
            {
                Move[,] moves = figuresMoves[type];
                for (int i = 0; i < moves.GetLength(0); i++)
                {
                    for (int j = 0; j < moves.GetLength(1); j++)
                    {
                        int row = rowKing + moves[i, j].Cols;
                        int column = columnKing + moves[i, j].Rows;
                        if (row < 0 || row > 8 || column < 0 || column > 8)
                            break;
                        /// If figure goes onto the throne cell
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
        private MoveType IsThroneOrThroneMine(Figure figure)
        {
            if (movesSinceThroneOrRokash == 2)
                return MoveType.ThroneMine;
            else if (choosedFigure.Type == FigureType.King && ((Point)choosedFigure.Image.Tag).X == 4
														   && ((Point)choosedFigure.Image.Tag).Y == 4)
            {
                movesSinceThroneOrRokash++;
                return MoveType.Throne;
            }
            else
                return MoveType.Regular;
        }

        private void ClearMoves()
        {
            grid.Children.Remove(cellUnderCursor);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    grid.Children.Remove(legalMovesBoard[i, j]);
                    legalMovesBoard[i, j] = null;
                }
            }
        }
    }
}
