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
            imageBlackRook1.Tag = Chess.Figure.Rook;        imageWhiteRook1.Tag = Chess.Figure.Rook;   
            imageBlackKnight1.Tag = Chess.Figure.Knight;    imageWhiteKnight1.Tag = Chess.Figure.Knight;
            imageBlackBishopW.Tag = Chess.Figure.Bishop;    imageWhiteBishopW.Tag = Chess.Figure.Bishop;
            imageBlackQueen.Tag = Chess.Figure.Queen;       imageWhiteQueen.Tag = Chess.Figure.Queen;
            imageBlackKing.Tag = Chess.Figure.King;         imageWhiteKing.Tag = Chess.Figure.King;
            imageBlackPrince.Tag = Chess.Figure.Prince;     imageWhitePrince.Tag = Chess.Figure.Prince;
            imageBlackKnight2.Tag = Chess.Figure.Knight;    imageWhiteKnight2.Tag = Chess.Figure.Knight;
            imageBlackBishopB.Tag = Chess.Figure.Bishop;    imageWhiteBishopB.Tag = Chess.Figure.Bishop;
            imageBlackRook2.Tag = Chess.Figure.Rook;        imageWhiteRook2.Tag = Chess.Figure.Rook;
            imageBlackPawnA.Tag = Chess.Figure.Pawn;        imageWhitePawnA.Tag = Chess.Figure.Pawn;
            imageBlackPawnB.Tag = Chess.Figure.Pawn;        imageWhitePawnB.Tag = Chess.Figure.Pawn;
            imageBlackPawnC.Tag = Chess.Figure.Pawn;        imageWhitePawnC.Tag = Chess.Figure.Pawn;
            imageBlackPawnD.Tag = Chess.Figure.Pawn;        imageWhitePawnD.Tag = Chess.Figure.Pawn;
            imageBlackPawnE.Tag = Chess.Figure.Pawn;        imageWhitePawnE.Tag = Chess.Figure.Pawn;
            imageBlackPawnE.Tag = Chess.Figure.Pawn;        imageWhitePawnE.Tag = Chess.Figure.Pawn;
            imageBlackPawnG.Tag = Chess.Figure.Pawn;        imageWhitePawnG.Tag = Chess.Figure.Pawn;
            imageBlackPawnH.Tag = Chess.Figure.Pawn;        imageWhitePawnH.Tag = Chess.Figure.Pawn;
            imageBlackPawnI.Tag = Chess.Figure.Pawn;        imageWhitePawnI.Tag = Chess.Figure.Pawn;

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
            chess.FindMoves((Image)sender, Chess.Player.Black, Chess.Figure.Rook);
        }

        private void ImageBlackKnight1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.Black, Chess.Figure.Knight);
        }

        private void ImageBlackBishopW_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.Black, Chess.Figure.Bishop);
        }

        private void ImageBlackQueen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.Black, Chess.Figure.Queen);
        }

        private void ImageBlackKing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.Black, Chess.Figure.King);
        }

        private void ImageBlackPrince_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.Black, Chess.Figure.Prince);
        }

        private void ImageBlackKnight2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.Black, Chess.Figure.Knight);
        }

        private void ImageBlackBishopB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.Black, Chess.Figure.Bishop);
        }

        private void ImageBlackRook2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.Black, Chess.Figure.Rook);
        }

        private void ImageBlackPawnA_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image) sender, Chess.Player.Black, Chess.Figure.Pawn);
        }

        private void ImageBlackPawnB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.Black, Chess.Figure.Pawn);
        }

        private void ImageBlackPawnC_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.Black, Chess.Figure.Pawn);
        }

        private void ImageBlackPawnD_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.Black, Chess.Figure.Pawn);
        }

        private void ImageBlackPawnE_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.Black, Chess.Figure.Pawn);
        }

        private void ImageBlackPawnF_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.Black, Chess.Figure.Pawn);
        }

        private void ImageBlackPawnG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.Black, Chess.Figure.Pawn);
        }

        private void ImageBlackPawnH_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.Black, Chess.Figure.Pawn);
        }

        private void ImageBlackPawnI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.Black, Chess.Figure.Pawn);
        }

        private void ImageWhitePawnA_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.White, Chess.Figure.Pawn);
        }

        private void ImageWhitePawnB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.White, Chess.Figure.Pawn);
        }

        private void ImageWhitePawnC_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.White, Chess.Figure.Pawn);
        }

        private void ImageWhitePawnD_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.White, Chess.Figure.Pawn);
        }

        private void ImageWhitePawnE_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.White, Chess.Figure.Pawn);
        }

        private void ImageWhitePawnF_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.White, Chess.Figure.Pawn);
        }

        private void ImageWhitePawnG_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.White, Chess.Figure.Pawn);
        }

        private void ImageWhitePawnH_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.White, Chess.Figure.Pawn);
        }

        private void ImageWhitePawnI_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.White, Chess.Figure.Pawn);
        }
        private void ImageWhiteRook1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.White, Chess.Figure.Rook);
        }

        private void ImageWhiteBishopB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.White, Chess.Figure.Bishop);
        }

        private void ImageWhiteKnight1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.White, Chess.Figure.Knight);
        }

        private void ImageWhitePrince_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.White, Chess.Figure.Prince);
        }

        private void ImageWhiteKing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.White, Chess.Figure.King);
        }

        private void ImageWhiteQueen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.White, Chess.Figure.Queen);
        }

        private void ImageWhiteBishopW_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.White, Chess.Figure.Bishop);
        }

        private void ImageWhiteKnight2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.White, Chess.Figure.Knight);
        }

        private void ImageWhiteRook2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            chess.FindMoves((Image)sender, Chess.Player.White, Chess.Figure.Rook);
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
