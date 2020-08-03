using BelarusChess.Core.Entities;
using BelarusChess.Core.Logic;
using BelarusChess.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BelarusChess.UI.Views
{
    /// <summary> Interaction logic for MainWindow.xaml </summary>
    public partial class GameWindow : Window/*, IChessView*/
    {
        /*#region Static readonly objects

        public static int leftMargin = 10;
        public static int topMargin = 10;
        public static int cellEdge = 55;

        #endregion

        #region Image sources relative paths

        public readonly string highlightChoosedPieceUri = "..\\Resources\\Highlight choosed piece.png";
        public readonly string highlightValidMoveUri = "..\\Resources\\Highlight valid move.png";
        public readonly string highlightValidMoveOnPieceUri = "..\\Resources\\Highlight valid move on piece.png";
        public readonly string highlightMouseMoveCellUri = "..\\Resources\\Highlight choosed piece.png";
        public readonly string highlightCheckUri = "..\\Resources\\Highlight check.png";

        #endregion

        #region Private fields

        private HelpWindow helpWindow;
        private readonly GameController game;
        private readonly ImageSetChessboard imageSetChessboard;
        private readonly List<Image> cellsHighlights;
        private Image choosedPieceHighlight;
        private Image mouseMoveHighlight;

        #endregion*/

        public GameWindow()
        {
            InitializeComponent();
            /*game = new GameController(new ChessEngine(new Chessboard()));
            imageSetChessboard = new ImageSetChessboard(this);
            cellsHighlights = new List<Image>();

            // Sets margin of a chessboard Image equal to the start margin
            imageChessboard.Margin = new Thickness(leftMargin, topMargin, 0, 0);*/
        }

        /*#region IChessView implementation

        public void SetTime(TimeSpan time)
        {
            Dispatcher.Invoke(() =>
            {
                labelTime.Content = time.ToString("c");
            });
        }

        public void SetValidCells(List<Cell> validCells)
        {
            foreach (var cell in validCells)
            {
                string highlightUri = (imageSetChessboard[cell] == null ? highlightValidMoveUri : highlightValidMoveOnPieceUri);
                Image highlight = CreateHighlightImage(highlightUri, cell);
                cellsHighlights.Add(highlight);
            }
        }

        public void SetStateForPlayers(GameState stateWhite, GameState stateBlack, PlayerColor? inaugurationState = null)
        {
            SetStateForPlayer(stateWhite, PlayerColor.White);
            SetStateForPlayer(stateBlack, PlayerColor.Black);

            if (inaugurationState == PlayerColor.White)
            {
                labelWhitePlayer.Content = "Інавгурація " + labelWhitePlayer.Content;
                imageWhiteKing.Margin = imageWhitePrince.Margin;
                imageWhiteKing.Visibility = Visibility.Visible;
                imageWhitePrince.Visibility = Visibility.Hidden;
            }
            else if (inaugurationState == PlayerColor.Black)
            {
                labelBlackPlayer.Content = "Інавгурація " + labelBlackPlayer.Content;
                imageBlackKing.Margin = imageBlackPrince.Margin;
                imageBlackKing.Visibility = Visibility.Visible;
                imageBlackPrince.Visibility = Visibility.Hidden;
            }
        }
            private void SetStateForPlayer(GameState state, PlayerColor color)
            {
                string message = "";
                Cell kingCell = ImageToCell(color == PlayerColor.White ? imageWhiteKing : imageBlackKing);

                switch (state)
                {
                    case GameState.Regular:
                        break;
                    case GameState.Check:
                        message = "Шах!";
                        cellsHighlights.Add(CreateHighlightImage(highlightCheckUri, kingCell));
                        break;
                    case GameState.Stalemate:
                        message = "Пат";
                        game.Finish();
                        break;
                    case GameState.Checkmate:
                        message = "Мат";
                        cellsHighlights.Add(CreateHighlightImage(highlightCheckUri, kingCell));
                        game.Finish();
                        break;
                    case GameState.Throne:
                        message = "Престол!";
                        break;
                    case GameState.ThroneMine:
                        message = "Престол мій";
                        game.Finish();
                        break;
                }

                if (color == PlayerColor.White)
                    labelWhitePlayer.Content = message;
                else
                    labelBlackPlayer.Content = message;
            }

        public void Chessboard_ChessboardPieceMoved(object sender, ChessboardPieceMovedEventArgs e)
        {
            if (imageSetChessboard[e.NewCell] != null)
                imageSetChessboard[e.NewCell].Visibility = Visibility.Hidden;

            imageSetChessboard[e.NewCell] = imageSetChessboard[e.OldCell];

            if (imageSetChessboard[e.NewCell] != null)
                imageSetChessboard[e.NewCell].Margin = new Thickness(leftMargin + e.NewCell.Col * cellEdge,
                                                                     topMargin + e.NewCell.Row * cellEdge, 0, 0);
            imageSetChessboard[e.OldCell] = null;        
        }

        #endregion

        #region Utils

        private Cell ImageToCell(Image image)
        {
            int rows = (int)(image.Margin.Top - topMargin) / cellEdge;
            int cols = (int)(image.Margin.Left - leftMargin) / cellEdge;
            return new Cell(rows, cols);
        }

        private Image CreateHighlightImage(string imageUri, Cell cell)
        {
            Image image = new Image
            {
                Source = new BitmapImage(new Uri(imageUri, UriKind.Relative)),
                Margin = new Thickness(leftMargin + cell.Col * cellEdge, topMargin + cell.Row * cellEdge, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = cellEdge,
                Height = cellEdge
            };

            int zIndex = 1;
            if (imageUri == highlightValidMoveUri || imageUri == highlightValidMoveOnPieceUri)
            {
                image.MouseLeftButtonDown += new MouseButtonEventHandler(ValidMoveImage_MouseLeftButtonDown);
                image.MouseEnter += new MouseEventHandler(ValidMoveImage_MouseEnter);
                image.MouseLeave += new MouseEventHandler(ValidMoveImage_MouseLeave);
                zIndex = 3;
            }
            Panel.SetZIndex(image, zIndex);
            grid.Children.Add(image);
            return image;
        }

        public void ClearHighlights()
        {
            grid.Children.Remove(choosedPieceHighlight);
            grid.Children.Remove(mouseMoveHighlight);
            for (int i = 0; i < cellsHighlights.Count; i++)
                grid.Children.Remove(cellsHighlights[i]);
            cellsHighlights.Clear();
        }

        #endregion

        #region Events

        private void ButtonNewGame_Click(object sender, RoutedEventArgs e)
        {
            imageSetChessboard.Reset();
            game.Start();
            buttonNewGame.IsEnabled = false;
            buttonFinishGame.IsEnabled = true;
            labelBlackPlayer.Content = labelWhitePlayer.Content = "";
        }

        private void ButtonFinishGame_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Справді завершити гру?", "Завершення гри", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                game.Finish();
                buttonFinishGame.IsEnabled = false;
                buttonNewGame.IsEnabled = true;
                labelBlackPlayer.Content = labelWhitePlayer.Content = "Гру завершено";
            }
        }

        private void ButtonHelp_Click(object sender, RoutedEventArgs e)
        {
            if (helpWindow == null)
                helpWindow = new HelpWindow();
            helpWindow.Show();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Справді вийти з програми?", "Закриття програми", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void PieceImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (game.IsGameStarted == false)
                return;

            ClearHighlights();
            Cell pieceCell = ImageToCell((Image)sender);
            choosedPieceHighlight = CreateHighlightImage(highlightChoosedPieceUri, pieceCell);
            game.FindPieceValidMoves(pieceCell);
        }

        private void ValidMoveImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClearHighlights();
            Cell cell = ImageToCell((Image)sender);
            game.MakeMoveTo(cell);
        }

        private void ValidMoveImage_MouseEnter(object sender, MouseEventArgs e)
        {
            Cell cell = ImageToCell((Image)sender);
            mouseMoveHighlight = CreateHighlightImage(highlightMouseMoveCellUri, cell);
        }

        private void ValidMoveImage_MouseLeave(object sender, MouseEventArgs e)
        {
            grid.Children.Remove(mouseMoveHighlight);
        }

        private void ImageChessboard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClearHighlights();
        }

        #endregion

        private class ImageSetChessboard
        {
            private readonly Image[,] startBoard;

            public Image[,] Board { get; private set; }
            public int Length { get => Board.GetLength(0); }

            public ImageSetChessboard(ChessWindow window)
            {
                startBoard = new Image[9, 9];

                #region Black pieces initialisation
                startBoard[0, 0] = window.imageBlackRook1;
                startBoard[0, 1] = window.imageBlackKnight1;
                startBoard[0, 2] = window.imageBlackBishopW;
                startBoard[0, 3] = window.imageBlackQueen;
                startBoard[0, 4] = window.imageBlackKing;
                startBoard[0, 5] = window.imageBlackPrince;
                startBoard[0, 6] = window.imageBlackKnight2;
                startBoard[0, 7] = window.imageBlackBishopB;
                startBoard[0, 8] = window.imageBlackRook2;

                startBoard[1, 0] = window.imageBlackPawnA;
                startBoard[1, 1] = window.imageBlackPawnB;
                startBoard[1, 2] = window.imageBlackPawnC;
                startBoard[1, 3] = window.imageBlackPawnD;
                startBoard[1, 4] = window.imageBlackPawnE;
                startBoard[1, 5] = window.imageBlackPawnF;
                startBoard[1, 6] = window.imageBlackPawnG;
                startBoard[1, 7] = window.imageBlackPawnH;
                startBoard[1, 8] = window.imageBlackPawnI;
                #endregion

                #region White pieces initialisation
                startBoard[7, 0] = window.imageWhitePawnA;
                startBoard[7, 1] = window.imageWhitePawnB;
                startBoard[7, 2] = window.imageWhitePawnC;
                startBoard[7, 3] = window.imageWhitePawnD;
                startBoard[7, 4] = window.imageWhitePawnE;
                startBoard[7, 5] = window.imageWhitePawnF;
                startBoard[7, 6] = window.imageWhitePawnG;
                startBoard[7, 7] = window.imageWhitePawnH;
                startBoard[7, 8] = window.imageWhitePawnI;

                startBoard[8, 0] = window.imageWhiteRook1;
                startBoard[8, 1] = window.imageWhiteBishopB;
                startBoard[8, 2] = window.imageWhiteKnight1;
                startBoard[8, 3] = window.imageWhitePrince;
                startBoard[8, 4] = window.imageWhiteKing;
                startBoard[8, 5] = window.imageWhiteQueen;
                startBoard[8, 6] = window.imageWhiteBishopW;
                startBoard[8, 7] = window.imageWhiteKnight2;
                startBoard[8, 8] = window.imageWhiteRook2;
                #endregion

                Reset();
            }
            public Image this[Cell cell]
            {
                get => (cell == null ? null : Board[cell.Row, cell.Col]);
                set
                {
                    if (cell == null)
                        return;
                    Board[cell.Row, cell.Col] = value;
                }
            }

            public void Reset()
            {
                Board = (Image[,])startBoard.Clone();
                // Resets Images location
                for (int i = 0; i < Length; i++)
                {
                    for (int j = 0; j < Length; j++)
                    {
                        if (Board[i, j] != null)
                        {
                            Board[i, j].Margin = new Thickness(leftMargin + j * cellEdge, topMargin + i * cellEdge, 0, 0);
                            Board[i, j].Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }*/
    }
}