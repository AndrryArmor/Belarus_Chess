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
    public class Chess
    {
        private Figure[,] figures;        /// A massive of chess figures on the board
        private Grid grid;                   /// Binding of the MainWindow's grid to create new controls
        private Image boardPlane;            /// Binding of the MainWindow's chess board to control mouse clicks on it
        private Figure[,] chessBoard;         /// A massive of Images that shows the location of the figures on the board
        private Image[,] movesBoard;         /// A massive of Images that shows the location of the attacked fields
        private PlayerColor currentColor = PlayerColor.White;

        private Figure choosedFigure;         /// An Image of a figure which is choosed to make a move
        private Image tempTile;
        private bool isOnlyKingOrPrinceWhite = false;
        private bool isOnlyKingOrPrinceBlack = false;
        // Inherited and constant objects
        private Image[,] white;              /// Binding of the MainWindow's white chess figures to control them
        private Image[,] black;              /// Binding of the MainWindow's black chess figures to control them
        private readonly double xMargin;
        private readonly double yMargin;
        private const double edge = 55;
        // Image source paths
        private static readonly string projectDirectory = Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.Parent.FullName;
        private readonly string choosedFigureUri = projectDirectory + "\\Resources\\Attack tile.png";
        private readonly string attackImageUri = projectDirectory + "\\Resources\\Attack.png";
        private readonly string attackFigureImageUri = projectDirectory + "\\Resources\\Attack figure.png";

        public enum PlayerColor
        {
            White, Black
        }
        public enum FigureType
        {
            Rook, Bishop, Knight, Prince, King, Queen, Pawn
        }
        private PlayerColor Next(PlayerColor color)
        {
            if (color == PlayerColor.Black)
                return PlayerColor.White;
            else
                return PlayerColor.Black;
        }
        public Chess(Grid grid, Image boardPlane, Figure[,] figures)
        {
            this.grid = grid;
            this.boardPlane = boardPlane;
            this.figures = figures;

            chessBoard = new Figure[9, 9];
            movesBoard = new Image[9, 9];
            boardPlane.MouseLeftButtonDown += new MouseButtonEventHandler(BoardPlane_MouseLeftButtonDown);
            // Start margin equal to the margin of a board Image
            xMargin = boardPlane.Margin.Left;
            yMargin = boardPlane.Margin.Top;
        }

        public void NewGame()
        {
            currentColor = 0;
            ClearMoves();
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
            {
                for (int j = 0; j < 9; j++)
                {
                    chessBoard[i, j] = null;
                }
            }
            // Start positions of white figures
            for (int i = 7; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    chessBoard[i, j] = figures[i - 7, j];
                    chessBoard[i, j].Image.Visibility = Visibility.Visible;
                    figures[i - 7, j].Image.Margin = new Thickness(xMargin + j * edge, yMargin + i * edge, 0, 0);
                }
            }
        }
        public void FindMoves(Image imageFigure)
        {
            /// Finds the figure which is represented by imageFigure
            int row = (int)((Point)imageFigure.Tag).X;
            int column = (int)((Point)imageFigure.Tag).Y;
            Figure figure = chessBoard[row, column];

            if (currentColor != figure.Color) return;
            if (choosedFigure != null)
            {
                ClearMoves();
            }

            choosedFigure = figure;
            int rowImage = (int)((imageFigure.Margin.Top - yMargin) / edge);
            int columnImage = (int)((imageFigure.Margin.Left - xMargin) / edge);
            movesBoard[rowImage, columnImage] = NewImage(choosedFigureUri, rowImage, columnImage, 1);

            Moves[,] moves = null;
            switch (figure)
            {
                case FigureType.Rook:
                    moves = MovesOf.Rook();
                    break;
                case FigureType.Bishop:
                    moves = MovesOf.Bishop();
                    break;
                case FigureType.Knight:
                    moves = MovesOf.Knight();
                    break;
                case FigureType.Prince:
                    moves = MovesOf.Prince();
                    break;
                case FigureType.King:
                    moves = MovesOf.King();
                    break;
                case FigureType.Queen:
                    moves = MovesOf.Queen();
                    break;
                case FigureType.Pawn:
                    
                    return;
                default:
                    break;
            }
            for (int i = 0; i < moves.GetLength(0); i++)
            {
                for (int j = 0; j < moves.GetLength(1); j++)
                {
                    int row = rowImage + moves[i, j].Y;
                    int column = columnImage + moves[i, j].X;
                    if (row < 0 || row > 8 || column < 0 || column > 8) break; // Borders case
                    if (figure != FigureType.King && figure != FigureType.Prince &&
                        row == 4 && column == 4)                               // Simple figures cannot move over the throne
                    {
                        if (chessBoard[row, column] == null)
                        {
                            movesBoard[row, column] = NewImage(attackImageUri, row, column, 3);
                        }
                        else if (PlayerColor(chessBoard[row, column]) != player)
                        {
                            movesBoard[row, column] = NewImage(attackFigureImageUri, row, column, 3);
                        }
                        break;
                    }

                    if (chessBoard[row, column] == null)
                    {
                        movesBoard[row, column] = NewImage(attackImageUri, row, column, 3);
                    }
                    else if (PlayerColor(chessBoard[row, column]) != player)
                    {
                        movesBoard[row, column] = NewImage(attackFigureImageUri, row, column, 3);
                        break;
                    }
                    else break;
                }
            }
        }
        public void MakeMove(int row, int column)
        {
            int r = (int)((choosedFigure.Margin.Top - yMargin) / edge);
            int c = (int)((choosedFigure.Margin.Left - xMargin) / edge);
            chessBoard[r, c] = null;
            if (chessBoard[row, column] != null)
            {
                if ((FigureType)chessBoard[row, column].Tag == FigureType.King || (FigureType)chessBoard[row, column].Tag == FigureType.Prince)
                {
                    if (PlayerColor(chessBoard[row, column]) == PlayerColor.White)
                        isOnlyKingOrPrinceWhite = true;
                    else
                        isOnlyKingOrPrinceBlack = true;
                    if ((FigureType)chessBoard[row, column].Tag == FigureType.King)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            for (int j = 0; j < 9; j++)
                            {
                                if (PlayerColor(chessBoard[i, j]) == PlayerColor(chessBoard[row, column]) && (FigureType)chessBoard[i, j].Tag == FigureType.Prince)
                                {
                                    chessBoard[i, j] = chessBoard[row, column];
                                }
                            }
                        }
                    }
                }
                chessBoard[row, column].Visibility = Visibility.Hidden;
            }
            chessBoard[row, column] = choosedFigure;
            chessBoard[row, column].Margin = new Thickness(xMargin + column * edge, yMargin + row * edge, 0, 0);
            currentColor++;

            if (IsCheck() == true)
            {
                MessageBox.Show("Check!");
            }
        }
        private bool IsCheck()
        {
            string findString = currentColor % 2 == 0 ? "imageWhiteKing" : "imageBlackKing";
            if (currentColor % 2 == 0 && isOnlyKingOrPrinceWhite == false)
                return false;
            else if (currentColor % 2 == 1 && isOnlyKingOrPrinceBlack == false)
                return false;
            if ((FigureType)choosedFigure.Tag == FigureType.King)
                return false;

            bool result = false;
            int rowImage = (int)((choosedFigure.Margin.Top - yMargin) / edge);
            int columnImage = (int)((choosedFigure.Margin.Left - xMargin) / edge);
            PlayerColor player = PlayerColor(chessBoard[rowImage - 1, columnImage + 1]);
            Moves[,] moves = null;
            switch (choosedFigure.Tag)
            {
                case FigureType.Rook:
                    moves = MovesOf.Rook();
                    break;
                case FigureType.Bishop:
                    moves = MovesOf.Bishop();
                    break;
                case FigureType.Knight:
                    moves = MovesOf.Knight();
                    break;
                case FigureType.Prince:
                    moves = MovesOf.Prince();
                    break;
                case FigureType.Queen:
                    moves = MovesOf.Queen();
                    break;
                case FigureType.Pawn:
                    if (currentColor % 2 == 0 && rowImage > 0)
                    {
                        if (player == PlayerColor.Black && columnImage < 8 && chessBoard[rowImage - 1, columnImage + 1] != null && chessBoard[rowImage - 1, columnImage + 1].Name == "imageWhiteKing")
                        {
                            result = true;
                        }
                        else if (player == PlayerColor.Black && columnImage > 0 && chessBoard[rowImage - 1, columnImage - 1] != null && chessBoard[rowImage - 1, columnImage - 1].Name == "imageWhiteKing")
                        {
                            result = true;
                        }
                    }
                    else if (currentColor % 2 == 1 && rowImage < 8)
                    {
                        if (player == PlayerColor.White && columnImage < 8 && chessBoard[rowImage + 1, columnImage + 1] != null && chessBoard[rowImage - 1, columnImage + 1].Name == "imageBlackKing")
                        {
                            result = true;
                        }
                        else if (player == PlayerColor.White && columnImage > 0 && chessBoard[rowImage + 1, columnImage - 1] != null && chessBoard[rowImage - 1, columnImage - 1].Name == "imageBlackKing")
                        {
                            result = true;
                        }
                    }
                    break;
                default:
                    break;
            }
            for (int i = 0; i < moves.GetLength(0); i++)
            {
                for (int j = 0; j < moves.GetLength(1); j++)
                {
                    int row = rowImage + moves[i, j].Y;
                    int column = columnImage + moves[i, j].X;
                    if (row < 0 || row > 8 || column < 0 || column > 8)
                        break; // Borders case
                    if ((FigureType)choosedFigure.Tag != FigureType.Prince && row == 4 && column == 4)    // Simple figures cannot move over the throne
                    {
                        if (chessBoard[row, column] != null && chessBoard[row, column].Name == findString)
                        {
                            result = true;
                        }
                        break;
                    }
                    if (chessBoard[row, column] != null)
                    {
                        if (chessBoard[row, column].Name == findString)
                            result = true;
                        break;
                    }
                    else
                        break;
                }
            }
            return result;
        }
        private PlayerColor PlayerC(Image image)
        {
            bool isWhite = false;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (white[i, j] == image)
                    {
                        isWhite = true;
                        break;
                    }
                }
            }
            return (isWhite == true ? PlayerColor.White : PlayerColor.Black);
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
            };
            Panel.SetZIndex(image, zIndex);
            image.MouseLeftButtonDown += new MouseButtonEventHandler(Image_MouseLeftButtonDown);
            image.MouseEnter += new MouseEventHandler(Image_MouseEnter);
            image.MouseLeave += new MouseEventHandler(Image_MouseLeave);
            grid.Children.Add(image);
            return image;
        }
        public void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClearMoves();
            Image image = (Image)sender;
            int row = (int)((image.Margin.Top - yMargin) / edge);
            int column = (int)((image.Margin.Left - xMargin) / edge);
            MakeMove(row, column);
        }
        public void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            // Creates a temporary tile that shows a current position of a mouse on the board 
            Image image = (Image)sender;
            int row = (int)((image.Margin.Top - yMargin) / edge);
            int column = (int)((image.Margin.Left - xMargin) / edge);
            tempTile = NewImage(choosedFigureUri, row, column, 1);
        }
        public void Image_MouseLeave(object sender, MouseEventArgs e) // TODO
        {
            grid.Children.Remove(tempTile);
        }
        public void BoardPlane_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) // TODO
        {
            if (choosedFigure != null)
            {
                ClearMoves();
                choosedFigure = null;
            }
        }
    }
}
