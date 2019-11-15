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
        public static double leftMargin = 10;
        public static double topMargin = 10;
        public static double cellEdge = 55;

        // Image relative source paths
        public readonly string clickedFigureImageUri = "Resources\\Choosed figure.png";
        public readonly string attackCellImageUri = "Resources\\Attack cell.png";
        public readonly string attackFigureImageUri = "Resources\\Attack figure.png";
        public readonly string mouseMoveCellImageUri = "Resources\\Choosed figure.png";
        public readonly string checkCellImageUri = "Resources\\Check cell.png";

        private HelpWindow helpWindow;
        private Game game;
        private List<Image> movesHighlight;
        private Image figureHighlight;
        private Image mouseMoveHighlight;

        public MainWindow()
        {
            InitializeComponent();
            game = new Game(this);
            movesHighlight = new List<Image>();

            // Set margin of a board Image equal to the start margin
            imageChessBoard.Margin = new Thickness(leftMargin, topMargin, 0, 0);
        }

        /// <summary> Creates highlight images in the necessary cell </summary>
        public Image NewImage(string imageUri, Cell cell)
        {
            Image image = new Image
            {
                Source = new BitmapImage(new Uri(imageUri, UriKind.Relative)),
                Margin = new Thickness(leftMargin + cell.Col * cellEdge, topMargin + cell.Row * cellEdge, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = cellEdge,
                Height = cellEdge,
                Tag = cell
            };

            int zIndex = 1;
            if (imageUri == attackFigureImageUri || imageUri == attackCellImageUri)
            {
                image.MouseLeftButtonDown += new MouseButtonEventHandler(AttackImage_MouseLeftButtonDown);
                image.MouseEnter += new MouseEventHandler(AttackImage_MouseEnter);
                image.MouseLeave += new MouseEventHandler(AttackImage_MouseLeave);
                zIndex = 3;
            }
            Panel.SetZIndex(image, zIndex);
            grid.Children.Add(image);
            return image;
        }

        public void CreateHighlight(Image image)
        {
            movesHighlight.Add(image);
        }

        public void ClearHighlightCells()
        {
            grid.Children.Remove(figureHighlight);
            grid.Children.Remove(mouseMoveHighlight);
            for (int i = 0; i < movesHighlight.Count; i++)
                grid.Children.Remove(movesHighlight[i]);
            movesHighlight.Clear();
        }

        private void ButtonNewGame_Click(object sender, RoutedEventArgs e)
        {
            game.Start();
            labelTime.Content = "00:00";
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

        private void Figure_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (game.IsGameStarted == true)
            {
                ClearHighlightCells();
                // Figure's Image.Tag stores a Figure that contains this Image
                Figure figure = (Figure)((Image)sender).Tag;
                figureHighlight = NewImage(clickedFigureImageUri, figure.Cell);
                game.FindLegalMoves(figure);
            }
        }

        private void AttackImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClearHighlightCells();
            // Image.Tag stores a Cell that contains this Image
            Cell cell = (Cell)((Image)sender).Tag;
            game.MakeMove(cell);
        }

        private void AttackImage_MouseEnter(object sender, MouseEventArgs e)
        {
            // Image.Tag stores a Cell that contains this Image
            Cell cell = (Cell)((Image)sender).Tag;
            mouseMoveHighlight = NewImage(mouseMoveCellImageUri, cell);
        }

        private void AttackImage_MouseLeave(object sender, MouseEventArgs e)
        {
            grid.Children.Remove(mouseMoveHighlight);
        }

        private void ImageChessBoard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClearHighlightCells();
        }
    } 
}