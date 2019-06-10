using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BelarusChess
{
    /// <summary> 
    /// Логика взаимодействия для MainWindow.xaml 
    /// </summary>
    public partial class MainWindow : Window
    {
        private Figure[,] figures;
        private System.Timers.Timer timer;
        private int time;
        private HelpWindow helpWindow;
        public bool isStarted = false;

        // Inherited and constant objects
        private readonly double xMargin;
        private readonly double yMargin;
        private const double edge = 55;
        // Image source paths
        private readonly string choosedFigureUri;
        private readonly string attackImageUri;
        private readonly string attackFigureImageUri;
        private readonly string checkImageUri;
        public MainWindow()
        {
            InitializeComponent();
            // String constants
            string projectDirectory = Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.Parent.FullName;
            choosedFigureUri = projectDirectory + "\\Resources\\Attack tile.png";
            attackImageUri = projectDirectory + "\\Resources\\Attack.png";
            attackFigureImageUri = projectDirectory + "\\Resources\\Attack figure.png";
            checkImageUri = projectDirectory + "\\Resources\\Check tile.png";
            // Initializing
            figures = InitializeFigures();
            chessBoard = new Figure[9, 9];
            movesBoard = new Image[9, 9];
            // Start margin equal to the margin of a board Image
            xMargin = imageChessBoard.Margin.Left;
            yMargin = imageChessBoard.Margin.Top;
            // Set parameters of a timer
            timer = new System.Timers.Timer {Interval = 1000};
            timer.Elapsed += Timer_Elapsed;
        }
        /// <summary> 
        /// Creates a matrix that contains all the Images of figures 
        /// </summary>
        private Figure[,] InitializeFigures()
        {
            return new Figure[,]
            {
                // Black figures
                { new Rook(imageBlackRook1, PlayerColor.Black),     new Knight(imageBlackKnight1, PlayerColor.Black),
                  new Bishop(imageBlackBishopW, PlayerColor.Black), new Queen(imageBlackQueen, PlayerColor.Black),
                  new King(imageBlackKing, PlayerColor.Black),      new Prince(imageBlackPrince, PlayerColor.Black),
                  new Knight(imageBlackKnight2, PlayerColor.Black), new Bishop(imageBlackBishopB, PlayerColor.Black),
                  new Rook(imageBlackRook2, PlayerColor.Black) },

                { new Pawn(imageBlackPawnA, PlayerColor.Black),     new Pawn(imageBlackPawnB, PlayerColor.Black),
                  new Pawn(imageBlackPawnC, PlayerColor.Black),     new Pawn(imageBlackPawnD, PlayerColor.Black),
                  new Pawn(imageBlackPawnE, PlayerColor.Black),     new Pawn(imageBlackPawnF, PlayerColor.Black),
                  new Pawn(imageBlackPawnG, PlayerColor.Black),     new Pawn(imageBlackPawnH, PlayerColor.Black),
                  new Pawn(imageBlackPawnI, PlayerColor.Black), },
                // White figures
                { new Pawn(imageWhitePawnA, PlayerColor.White),     new Pawn(imageWhitePawnB, PlayerColor.White),
                  new Pawn(imageWhitePawnC, PlayerColor.White),     new Pawn(imageWhitePawnD, PlayerColor.White),
                  new Pawn(imageWhitePawnE, PlayerColor.White),     new Pawn(imageWhitePawnF, PlayerColor.White),
                  new Pawn(imageWhitePawnG, PlayerColor.White),     new Pawn(imageWhitePawnH, PlayerColor.White),
                  new Pawn(imageWhitePawnI, PlayerColor.White), },

                { new Rook(imageWhiteRook1, PlayerColor.White),     new Bishop(imageWhiteBishopB, PlayerColor.White),
                  new Knight(imageWhiteKnight1, PlayerColor.White), new Prince(imageWhitePrince, PlayerColor.White),
                  new King(imageWhiteKing, PlayerColor.White),      new Queen(imageWhiteQueen, PlayerColor.White),
                  new Bishop(imageWhiteBishopW, PlayerColor.White), new Knight(imageWhiteKnight2, PlayerColor.White),
                  new Rook(imageWhiteRook2, PlayerColor.White) }
            };
        }
        /// <summary> 
        /// Binds coordinates of the figures on the board to the Images of the figures 
        /// </summary>
        private void InitializeImages(Figure[,] figures)
        {
            for (int i = 0; i < figures.GetLength(0); i++)
            {
                for (int j = 0; j < figures.GetLength(1); j++)
                {
                    if (figures[i, j].Color == PlayerColor.Black)
                        figures[i, j].Image.Tag = new Point(j, i);
                    else
                        figures[i, j].Image.Tag = new Point(j, i + 5);
                }
            }
        }
        /// <summary>
        /// Creates highlight images in the position chessBoard[row, column]
        /// </summary>
        private Image NewImage(string uri, int row, int column, int zIndex)
        {
            Image image = new Image
            {
                Source = new BitmapImage(new Uri(uri)),
                Margin = new Thickness(xMargin + column * edge, yMargin + row * edge, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = edge,
                Height = edge,
                Tag = uri
            };
            Panel.SetZIndex(image, zIndex);
            if (uri != checkImageUri)
            {
                image.MouseLeftButtonDown += new MouseButtonEventHandler(Image_MouseLeftButtonDown);
                image.MouseEnter += new MouseEventHandler(Image_MouseEnter);
                image.MouseLeave += new MouseEventHandler(Image_MouseLeave);
            }
            grid.Children.Add(image);
            return image;
        }

        // Events
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            time++;
            int minutes = (time / 60) % 60;
            int seconds = time % 60;
            Dispatcher.Invoke(() =>
            {
                labelTime.Content = string.Format($"{minutes:00}:{seconds:00}");
            });
        }
        private void ButtonNewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
            labelTime.Content = "00:00";
            time = 0;
            timer.Start();
            isStarted = true;
            buttonNewGame.IsEnabled = false;
            buttonFinishGame.IsEnabled = true;
            labelBlackPlayer.Content = labelWhitePlayer.Content = "";
        }
        private void ButtonFinishGame_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Справді завершити гру?", "Завершення гри", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                isStarted = false;
                buttonFinishGame.IsEnabled = false;
                buttonNewGame.IsEnabled = true;
                labelBlackPlayer.Content = labelWhitePlayer.Content = "Гру завершено";
                timer.Stop();
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
        private void Figure_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isStarted == true)
                FindMoves((Image)sender);
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClearMoves();
            Image image = (Image)sender;
            int row = (int)((image.Margin.Top - yMargin) / edge);
            int column = (int)((image.Margin.Left - xMargin) / edge);
            MakeMove(row, column);
        }
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            // Creates a temporary tile that shows a current position of a mouse on the board 
            Image image = (Image)sender;
            int row = (int)((image.Margin.Top - yMargin) / edge);
            int column = (int)((image.Margin.Left - xMargin) / edge);
            tempTile = NewImage(choosedFigureUri, row, column, 1);
        }
        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            grid.Children.Remove(tempTile);
        }
        private void ImageChessBoard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (choosedFigure != null)
            {
                ClearMoves();
                choosedFigure = null;
            }
        }
        /// <summary>
        /// Describes the translation of a location of the figure
        /// </summary>
        public struct Moves
        {
            public int X;
            public int Y;
            public Moves(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}