﻿using System;
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
        Chess chess;
        Figure[,] figures;
        System.Timers.Timer timer;
        int time;
        HelpWindow helpWindow;
        public MainWindow()
        {
            InitializeComponent();
            figures = InitializeFigures();
            InitializeImages(figures);
            chess = new Chess(grid, imageChessBoard, figures);

            // Set parameters of a timer
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
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
                { new Rook(imageBlackRook1, Chess.PlayerColor.Black),     new Knight(imageBlackKnight1, Chess.PlayerColor.Black),
                  new Bishop(imageBlackBishopW, Chess.PlayerColor.Black), new Queen(imageBlackQueen, Chess.PlayerColor.Black),
                  new King(imageBlackKing, Chess.PlayerColor.Black),      new Prince(imageBlackPrince, Chess.PlayerColor.Black),
                  new Knight(imageBlackKnight2, Chess.PlayerColor.Black), new Bishop(imageBlackBishopB, Chess.PlayerColor.Black),
                  new Rook(imageBlackRook2, Chess.PlayerColor.Black) },

                { new Pawn(imageBlackPawnA, Chess.PlayerColor.Black),     new Pawn(imageBlackPawnB, Chess.PlayerColor.Black),
                  new Pawn(imageBlackPawnC, Chess.PlayerColor.Black),     new Pawn(imageBlackPawnD, Chess.PlayerColor.Black),
                  new Pawn(imageBlackPawnE, Chess.PlayerColor.Black),     new Pawn(imageBlackPawnF, Chess.PlayerColor.Black),
                  new Pawn(imageBlackPawnG, Chess.PlayerColor.Black),     new Pawn(imageBlackPawnH, Chess.PlayerColor.Black),
                  new Pawn(imageBlackPawnI, Chess.PlayerColor.Black), },
                // White figures
                { new Pawn(imageWhitePawnA, Chess.PlayerColor.White),     new Pawn(imageWhitePawnB, Chess.PlayerColor.White),
                  new Pawn(imageWhitePawnC, Chess.PlayerColor.White),     new Pawn(imageWhitePawnD, Chess.PlayerColor.White),
                  new Pawn(imageWhitePawnE, Chess.PlayerColor.White),     new Pawn(imageWhitePawnF, Chess.PlayerColor.White),
                  new Pawn(imageWhitePawnG, Chess.PlayerColor.White),     new Pawn(imageWhitePawnH, Chess.PlayerColor.White),
                  new Pawn(imageWhitePawnI, Chess.PlayerColor.White), },

                { new Rook(imageWhiteRook1, Chess.PlayerColor.White),     new Bishop(imageWhiteBishopB, Chess.PlayerColor.White),
                  new Knight(imageWhiteKnight1, Chess.PlayerColor.White), new Prince(imageWhitePrince, Chess.PlayerColor.White),
                  new King(imageWhiteKing, Chess.PlayerColor.White),      new Queen(imageWhiteQueen, Chess.PlayerColor.White),
                  new Bishop(imageWhiteBishopW, Chess.PlayerColor.White), new Knight(imageWhiteKnight2, Chess.PlayerColor.White),
                  new Rook(imageWhiteRook2, Chess.PlayerColor.White) }
            };
        }
        /// <summary>
        /// Binds a coordinates of the figures on the board 
        /// (begin in the left top corner)
        /// to the Images of figures
        /// </summary>
        private void InitializeImages(Figure[,] figures)
        {
            for (int i = 0; i < figures.GetLength(0); i++)
            {
                for (int j = 0; j < figures.GetLength(1); j++)
                {
                    if (figures[i, j].Color == Chess.PlayerColor.Black)
                        figures[i, j].Image.Tag = new Point(i, j);
                    else
                        figures[i + 5, j].Image.Tag = new Point(i + 5, j);

                }
            }
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
            chess.FindMoves((Image)sender);
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