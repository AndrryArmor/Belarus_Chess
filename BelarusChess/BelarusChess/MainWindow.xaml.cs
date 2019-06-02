using System;
using System.Collections.Generic;
using System.Linq;
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
        HelpWindow helpWindow;
        Image[,] blackFigures;
        Image[,] whiteFigures;
        Chess chess;
        System.Timers.Timer timer;
        int time;
        public MainWindow()
        {
            InitializeComponent();
            imageBlackRook1.Tag = Chess.FigureType.Rook;        imageWhiteRook1.Tag = Chess.FigureType.Rook;   
            imageBlackKnight1.Tag = Chess.FigureType.Knight;    imageWhiteKnight1.Tag = Chess.FigureType.Knight;
            imageBlackBishopW.Tag = Chess.FigureType.Bishop;    imageWhiteBishopW.Tag = Chess.FigureType.Bishop;
            imageBlackQueen.Tag = Chess.FigureType.Queen;       imageWhiteQueen.Tag = Chess.FigureType.Queen;
            imageBlackKing.Tag = Chess.FigureType.King;         imageWhiteKing.Tag = Chess.FigureType.King;
            imageBlackPrince.Tag = Chess.FigureType.Prince;     imageWhitePrince.Tag = Chess.FigureType.Prince;
            imageBlackKnight2.Tag = Chess.FigureType.Knight;    imageWhiteKnight2.Tag = Chess.FigureType.Knight;
            imageBlackBishopB.Tag = Chess.FigureType.Bishop;    imageWhiteBishopB.Tag = Chess.FigureType.Bishop;
            imageBlackRook2.Tag = Chess.FigureType.Rook;        imageWhiteRook2.Tag = Chess.FigureType.Rook;
            imageBlackPawnA.Tag = Chess.FigureType.Pawn;        imageWhitePawnA.Tag = Chess.FigureType.Pawn;
            imageBlackPawnB.Tag = Chess.FigureType.Pawn;        imageWhitePawnB.Tag = Chess.FigureType.Pawn;
            imageBlackPawnC.Tag = Chess.FigureType.Pawn;        imageWhitePawnC.Tag = Chess.FigureType.Pawn;
            imageBlackPawnD.Tag = Chess.FigureType.Pawn;        imageWhitePawnD.Tag = Chess.FigureType.Pawn;
            imageBlackPawnE.Tag = Chess.FigureType.Pawn;        imageWhitePawnE.Tag = Chess.FigureType.Pawn;
            imageBlackPawnE.Tag = Chess.FigureType.Pawn;        imageWhitePawnE.Tag = Chess.FigureType.Pawn;
            imageBlackPawnG.Tag = Chess.FigureType.Pawn;        imageWhitePawnG.Tag = Chess.FigureType.Pawn;
            imageBlackPawnH.Tag = Chess.FigureType.Pawn;        imageWhitePawnH.Tag = Chess.FigureType.Pawn;
            imageBlackPawnI.Tag = Chess.FigureType.Pawn;        imageWhitePawnI.Tag = Chess.FigureType.Pawn;

            blackFigures = new Image[,]{ 
            { imageBlackRook1, imageBlackKnight1, imageBlackBishopW,  imageBlackQueen, imageBlackKing, imageBlackPrince, imageBlackKnight2, imageBlackBishopB, imageBlackRook2 },
            { imageBlackPawnA, imageBlackPawnB,   imageBlackPawnC,   imageBlackPawnD,  imageBlackPawnE, imageBlackPawnF, imageBlackPawnG,   imageBlackPawnH,   imageBlackPawnI } };

            whiteFigures = new Image[,]{
            { imageWhitePawnA, imageWhitePawnB,   imageWhitePawnC,   imageWhitePawnD,  imageWhitePawnE, imageWhitePawnF, imageWhitePawnG,   imageWhitePawnH,   imageWhitePawnI },
            { imageWhiteRook1, imageWhiteBishopB, imageWhiteKnight1, imageWhitePrince, imageWhiteKing,  imageWhiteQueen, imageWhiteBishopW, imageWhiteKnight2, imageWhiteRook2 } };

            chess = new Chess(grid, imageChessBoard, whiteFigures, blackFigures);
            // Set parameters of a timer
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
        }

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
            timer.Stop();
            chess.NewGame();
            labelTime.Content = "00:00";
            time = 0;
            timer.Start();
        }
        private void ButtonFinishGame_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Справді завершити гру?", "Завершення гри", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
        private void ButtonHelp_Click(object sender, RoutedEventArgs e)
        {
            helpWindow = new HelpWindow();
            helpWindow.Show();
        }


        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Справді вийти з програми?", "Закриття програми", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void ImageBlackRook1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.Black, Chess.FigureType.Rook);
        }

        private void ImageBlackKnight1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.Black, Chess.FigureType.Knight);
        }

        private void ImageBlackBishopW_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.Black, Chess.FigureType.Bishop);
        }

        private void ImageBlackQueen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.Black, Chess.FigureType.Queen);
        }

        private void ImageBlackKing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.Black, Chess.FigureType.King);
        }

        private void ImageBlackPrince_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.Black, Chess.FigureType.Prince);
        }

        private void ImageBlackKnight2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.Black, Chess.FigureType.Knight);
        }

        private void ImageBlackBishopB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.Black, Chess.FigureType.Bishop);
        }

        private void ImageBlackRook2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.Black, Chess.FigureType.Rook);
        }

        private void ImageBlackPawnA_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image) sender, Chess.PlayerColor.Black, Chess.FigureType.Pawn);
        }

        private void ImageBlackPawnB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.Black, Chess.FigureType.Pawn);
        }

        private void ImageBlackPawnC_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.Black, Chess.FigureType.Pawn);
        }

        private void ImageBlackPawnD_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.Black, Chess.FigureType.Pawn);
        }

        private void ImageBlackPawnE_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.Black, Chess.FigureType.Pawn);
        }

        private void ImageBlackPawnF_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.Black, Chess.FigureType.Pawn);
        }

        private void ImageBlackPawnG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.Black, Chess.FigureType.Pawn);
        }

        private void ImageBlackPawnH_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.Black, Chess.FigureType.Pawn);
        }

        private void ImageBlackPawnI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.Black, Chess.FigureType.Pawn);
        }

        private void ImageWhitePawnA_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.White, Chess.FigureType.Pawn);
        }

        private void ImageWhitePawnB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.White, Chess.FigureType.Pawn);
        }

        private void ImageWhitePawnC_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.White, Chess.FigureType.Pawn);
        }

        private void ImageWhitePawnD_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.White, Chess.FigureType.Pawn);
        }

        private void ImageWhitePawnE_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.White, Chess.FigureType.Pawn);
        }

        private void ImageWhitePawnF_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.White, Chess.FigureType.Pawn);
        }

        private void ImageWhitePawnG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.White, Chess.FigureType.Pawn);
        }

        private void ImageWhitePawnH_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.White, Chess.FigureType.Pawn);
        }

        private void ImageWhitePawnI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.White, Chess.FigureType.Pawn);
        }
        private void ImageWhiteRook1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.White, Chess.FigureType.Rook);
        }

        private void ImageWhiteBishopB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.White, Chess.FigureType.Bishop);
        }

        private void ImageWhiteKnight1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.White, Chess.FigureType.Knight);
        }

        private void ImageWhitePrince_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.White, Chess.FigureType.Prince);
        }

        private void ImageWhiteKing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.White, Chess.FigureType.King);
        }

        private void ImageWhiteQueen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.White, Chess.FigureType.Queen);
        }

        private void ImageWhiteBishopW_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.White, Chess.FigureType.Bishop);
        }

        private void ImageWhiteKnight2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.White, Chess.FigureType.Knight);
        }

        private void ImageWhiteRook2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.PlayerColor.White, Chess.FigureType.Rook);
        }


        /*
        Point picturePos; = pictureBox1.Location;
        Point mousePos;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
        mousePos = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
        if (e.Button == MouseButtons.Left)
        {
        int dx = e.X - mousePos.X;
        int dy = e.Y - mousePos.Y;
        pictureBox1.Location = new Point(pictureBox1.Left + dx, pictureBox1.Top + dy);
        }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
        pictureBox1.Location = picturePos;
        }*/
    }
}
