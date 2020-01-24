using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BelarusChess
{
    /// <summary> Логика взаимодействия для MainWindow.xaml </summary>
    public partial class ChessWindow : Window, IChessView
    {
        // Static readonly objects
        public static int leftMargin = 10;
        public static int topMargin = 10;
        public static int cellEdge = 55;

        // Image relative source paths
        public readonly string highlightChoosedFigureUri = "Resources\\Highlight choosed figure.png";
        public readonly string highlightValidMoveUri = "Resources\\Highlight valid move.png";
        public readonly string highlightValidMoveOnFigureUri = "Resources\\Highlight valid move on figure.png";
        public readonly string highlightMouseMoveCellUri = "Resources\\Highlight choosed figure.png";
        public readonly string highlightCheckUri = "Resources\\Highlight check.png";

        private HelpWindow helpWindow;
        private ChessGame game;
        private ImageSetChessboard imageSetChessboard;
        private List<Image> cellsHighlights;
        private Image choosedFigureHighlight;
        private Image mouseMoveHighlight;

        public ChessWindow()
        {
            InitializeComponent();
            game = new ChessGame(this);
            imageSetChessboard = new ImageSetChessboard(this);
            cellsHighlights = new List<Image>();

            // Set margin of a board Image equal to the start margin
            imageChessboard.Margin = new Thickness(leftMargin, topMargin, 0, 0);
        }

        public void SetTime(TimeSpan time)
        {
            Dispatcher.Invoke(() =>
            {
                labelTime.Content = time.Seconds;
            });
        }

        public void SetValidCells(List<Cell> validCells)
        {
            foreach (var cell in validCells)
            {
                string highlightUri = (imageSetChessboard[cell] == null ? highlightValidMoveUri : highlightValidMoveOnFigureUri);
                Image highlight = CreateHighlightImage(highlightUri, cell);
                cellsHighlights.Add(highlight);
            }
        }

        public void SetFigureNewCell(Cell oldCell, Cell newCell)
        {
            if (imageSetChessboard[newCell] != null)
                imageSetChessboard[newCell].Visibility = Visibility.Hidden;

            imageSetChessboard[newCell] = imageSetChessboard[oldCell];
            imageSetChessboard[newCell].Margin = new Thickness(leftMargin + newCell.Col * cellEdge, topMargin + newCell.Row * cellEdge, 0, 0);
            imageSetChessboard[oldCell] = null;
        }

        public void SetMessageWhite(string message)
        {
            labelWhitePlayer.Content = message;
        }

        public void SetMessageBlack(string message)
        {
            labelBlackPlayer.Content = message;
        }

        public void SetCheck()
        {
            throw new NotImplementedException();
        }

        private Cell ImageToCell(Image image)
        {
            int rows = (int)(image.Margin.Top - topMargin) / cellEdge;
            int cols = (int)(image.Margin.Left - leftMargin) / cellEdge;
            return Cell.Create(rows, cols);
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
            if (imageUri == highlightValidMoveUri || imageUri == highlightValidMoveOnFigureUri)
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
            grid.Children.Remove(choosedFigureHighlight);
            grid.Children.Remove(mouseMoveHighlight);
            for (int i = 0; i < cellsHighlights.Count; i++)
                grid.Children.Remove(cellsHighlights[i]);
            cellsHighlights.Clear();
        }

        // Events
        private void ButtonNewGame_Click(object sender, RoutedEventArgs e)
        {
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

        private void FigureImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (game.IsGameStarted == false)
                return;

            ClearHighlights();
            Cell figureCell = ImageToCell((Image)sender);
            choosedFigureHighlight = CreateHighlightImage(highlightChoosedFigureUri, figureCell);
            game.FindFigureValidMoves(figureCell);
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

        private class ImageSetChessboard
        {
            public Image[,] Board { get; private set; }
            public int Length { get => Board.GetLength(0); }

            public ImageSetChessboard(ChessWindow window)
            {
                Board = new Image[9, 9];
                // Black figures
                Board[0, 0] = window.imageBlackRook1;
                Board[0, 1] = window.imageBlackKnight1;
                Board[0, 2] = window.imageBlackBishopW;
                Board[0, 3] = window.imageBlackQueen;
                Board[0, 4] = window.imageBlackKing;
                Board[0, 5] = window.imageBlackPrince;
                Board[0, 6] = window.imageBlackKnight2;
                Board[0, 7] = window.imageBlackBishopB;
                Board[0, 8] = window.imageBlackRook2;

                Board[1, 0] = window.imageBlackPawnA;
                Board[1, 1] = window.imageBlackPawnB;
                Board[1, 2] = window.imageBlackPawnC;
                Board[1, 3] = window.imageBlackPawnD;
                Board[1, 4] = window.imageBlackPawnE;
                Board[1, 5] = window.imageBlackPawnF;
                Board[1, 6] = window.imageBlackPawnG;
                Board[1, 7] = window.imageBlackPawnH;
                Board[1, 8] = window.imageBlackPawnI;

                // Fills middle part of the chessboard
                for (int i = 2; i < 7; i++)
                    for (int j = 0; j < 9; j++)
                        Board[i, j] = null;

                // White figures
                Board[7, 0] = window.imageWhitePawnA;
                Board[7, 1] = window.imageWhitePawnB;
                Board[7, 2] = window.imageWhitePawnC;
                Board[7, 3] = window.imageWhitePawnD;
                Board[7, 4] = window.imageWhitePawnE;
                Board[7, 5] = window.imageWhitePawnF;
                Board[7, 6] = window.imageWhitePawnG;
                Board[7, 7] = window.imageWhitePawnH;
                Board[7, 8] = window.imageWhitePawnI;

                Board[8, 0] = window.imageWhiteRook1;
                Board[8, 1] = window.imageWhiteBishopB;
                Board[8, 2] = window.imageWhiteKnight1;
                Board[8, 3] = window.imageWhitePrince;
                Board[8, 4] = window.imageWhiteKing;
                Board[8, 5] = window.imageWhiteQueen;
                Board[8, 6] = window.imageWhiteBishopW;
                Board[8, 7] = window.imageWhiteKnight2;
                Board[8, 8] = window.imageWhiteRook2;
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
        }
    }
}