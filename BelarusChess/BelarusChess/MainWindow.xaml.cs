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
    public partial class MainWindow : Window
    {
        // Static readonly objects
        public static readonly double leftMargin = 10;
        public static readonly double topMargin = 10;
        public static readonly double cellEdge = 55;

        // Image relative source paths
        private readonly string clickedFigureUri = "Resources\\Attack cell.png";
        private readonly string attackImageUri = "Resources\\Attack.png";
        private readonly string attackFigureImageUri = "Resources\\Attack figure.png";
        private readonly string checkImageUri = "Resources\\Check cell.png";

        /// <summary> Adds a second to the time </summary>
        private System.Timers.Timer oneSecond;
        private HelpWindow helpWindow;
        private bool isGameStarted = false;
        private int time;

        public MainWindow()
        {
            InitializeComponent();

            // Initializing
            chessboard = new Chessboard();
            legalMoves = new List<Image>();

            // Set margin of a board Image equal to the start margin
            imageChessBoard.Margin = new Thickness(leftMargin, topMargin, 0, 0);

            // Set parameters of a timer
            oneSecond = new System.Timers.Timer {Interval = 1000};
            oneSecond.Elapsed += OneSecond_Elapsed;
        }

        /// <summary> Creates highlight images in the necessary cell </summary>
        private Image NewImage(string imageUri, Cell cell, int zIndex)
        {
            Image image = new Image
            {
                Source = new BitmapImage(new Uri(imageUri, UriKind.Relative)),
                Margin = new Thickness(leftMargin + cell.Col * cellEdge, topMargin + cell.Row * cellEdge, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = cellEdge,
                Height = cellEdge,
                Tag = imageUri // Why it is necessary?
            };
            Panel.SetZIndex(image, zIndex);
            if (imageUri != checkImageUri)
            {
                image.MouseLeftButtonDown += new MouseButtonEventHandler(Image_MouseLeftButtonDown);
                image.MouseEnter += new MouseEventHandler(Image_MouseEnter);
                image.MouseLeave += new MouseEventHandler(Image_MouseLeave);
            }
            grid.Children.Add(image);
            return image;
        }

        // Events
        private void OneSecond_Elapsed(object sender, ElapsedEventArgs e)
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
            StartNewGame();
            labelTime.Content = "00:00";
            time = 0;
            isGameStarted = true;
            buttonNewGame.IsEnabled = false;
            buttonFinishGame.IsEnabled = true;
            labelBlackPlayer.Content = labelWhitePlayer.Content = "";
            oneSecond.Start();
        }
        private void ButtonFinishGame_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Справді завершити гру?", "Завершення гри", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                oneSecond.Stop();
                ClearLegalMoves();
                isGameStarted = false;
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
        private void Figure_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isGameStarted == true)
            {
                // Image.Tag stores a Figure which contains this Image
                Figure figure = (Figure)((Image)sender).Tag;
                FindLegalMoves(figure);
            }
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClearLegalMoves();
            Image image = (Image)sender;
            int row = (int)((image.Margin.Top - topMargin) / cellEdge);
            int column = (int)((image.Margin.Left - leftMargin) / cellEdge);
            MakeMove(new Cell(row, column));
        }
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            Image image = (Image)sender;
            int row = (int)((image.Margin.Top - topMargin) / cellEdge);
            int column = (int)((image.Margin.Left - leftMargin) / cellEdge);
            cellUnderCursor = NewImage(clickedFigureUri, new Cell(row, column), 1);
        }
        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            grid.Children.Remove(cellUnderCursor);
        }
        private void ImageChessBoard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (clickedFigure != null)
            {
                ClearLegalMoves();
                clickedFigure = null;
            }
        }
    }

    
    /// <summary> Determines all possible types of moves </summary>
    public enum MoveType
    {
        Regular, Check, Checkmate, Inauguration, Throne, ThroneMine
    }
    public enum PlayerColor
    {
        White, Black
    }
}