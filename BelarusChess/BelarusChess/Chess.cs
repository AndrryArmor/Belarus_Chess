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
        private PlayerColor currentColor;
        private Figure choosedFigure;         /// An Image of a figure which is choosed to make a move
        private Image tempTile;
        public bool isStarted = false;
        private bool isOnlyKingOrPrinceWhite = false;
        private bool isOnlyKingOrPrinceBlack = false;
        // Inherited and constant objects
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
            isStarted = true;
            ClearMoves();
            currentColor = PlayerColor.White;
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
                    chessBoard[i, j] = figures[i - 5, j];
                    chessBoard[i, j].Image.Visibility = Visibility.Visible;
                    figures[i - 5, j].Image.Margin = new Thickness(xMargin + j * edge, yMargin + i * edge, 0, 0);
                }
            }
        }
        public void FindMoves(Image imageFigure)
        {
            /// Finds the figure which is represented by imageFigure
            int rowClicked = (int)((Point)imageFigure.Tag).Y;
            int columnClicked = (int)((Point)imageFigure.Tag).X;
            Figure figure = chessBoard[rowClicked, columnClicked];

            if (currentColor != figure.Color)
                return;
            if (choosedFigure != null)
                ClearMoves();

            choosedFigure = figure;
            movesBoard[rowClicked, columnClicked] = NewImage(choosedFigureUri, rowClicked, columnClicked, 1);

            Moves[,] moves = choosedFigure.Moves();
            if (choosedFigure.Type == FigureType.Pawn)
            {
                PawnMoves(moves, rowClicked, columnClicked);
                return;
            }

            for (int i = 0; i < moves.GetLength(0); i++)
            {
                for (int j = 0; j < moves.GetLength(1); j++)
                {
                    int row = rowClicked + moves[i, j].Y;
                    int column = columnClicked + moves[i, j].X;
                    if (row < 0 || row > 8 || column < 0 || column > 8) break; // Borders case
                    if (figure.Type != FigureType.King && figure.Type != FigureType.Prince &&
                        row == 4 && column == 4)                               // Simple figures cannot move over the throne
                    {
                        if (chessBoard[row, column] == null)
                        {
                            movesBoard[row, column] = NewImage(attackImageUri, row, column, 3);
                        }
                        else if (chessBoard[row, column].Color != currentColor)
                        {
                            movesBoard[row, column] = NewImage(attackFigureImageUri, row, column, 3);
                        }
                        break;
                    }

                    if (chessBoard[row, column] == null)
                    {
                        movesBoard[row, column] = NewImage(attackImageUri, row, column, 3);
                    }
                    else if (chessBoard[row, column].Color != currentColor)
                    {
                        movesBoard[row, column] = NewImage(attackFigureImageUri, row, column, 3);
                        break;
                    }
                    else break;
                }
            }
        }

        private void PawnMoves(Moves[,] moves, int row, int column)
        {
            if (currentColor == PlayerColor.White && row > 0)
            {
                // Move up playing white figures
                if (chessBoard[row - 1, column] == null)
                {
                    movesBoard[row - 1, column] = NewImage(attackImageUri, row - 1, column, 3);
                    // Double move
                    if (row == 7 && chessBoard[row - 2, column] == null)
                    {
                        movesBoard[row - 2, column] = NewImage(attackImageUri, row - 2, column, 3);
                    }
                }
                // Beat up-right
                if (column < 8 && chessBoard[row - 1, column + 1] != null && chessBoard[row - 1, column + 1].Color != currentColor)
                {
                    movesBoard[row - 1, column + 1] = NewImage(attackFigureImageUri, row - 1, column + 1, 3);
                }
                // Beat up-left
                if (column > 0 && chessBoard[row - 1, column - 1] != null && chessBoard[row - 1, column - 1].Color != currentColor)
                {
                    movesBoard[row - 1, column - 1] = NewImage(attackFigureImageUri, row - 1, column - 1, 3);
                }
            }
            else if (currentColor == PlayerColor.Black && row < 8)
            {
                // Move up playing white figures
                if (chessBoard[row + 1, column] == null)
                {
                    movesBoard[row + 1, column] = NewImage(attackImageUri, row + 1, column, 3);
                    // Double move
                    if (row == 1 && chessBoard[row + 2, column] == null)
                    {
                        movesBoard[row + 2, column] = NewImage(attackImageUri, row + 2, column, 3);
                    }
                }
                // Beat up-right
                if (column < 8 && chessBoard[row + 1, column + 1] != null && chessBoard[row + 1, column + 1].Color != currentColor)
                {
                    movesBoard[row + 1, column + 1] = NewImage(attackFigureImageUri, row + 1, column + 1, 3);
                }
                // Beat up-left
                if (column > 0 && chessBoard[row + 1, column - 1] != null && chessBoard[row + 1, column - 1].Color != currentColor)
                {
                    movesBoard[row + 1, column - 1] = NewImage(attackFigureImageUri, row + 1, column - 1, 3);
                }
            }
        }

        public void MakeMove(int row, int column)
        {
            // Deletes the old location of the figure
            int oldRow = (int)((Point)choosedFigure.Image.Tag).Y;
            int oldColumn = (int)((Point)choosedFigure.Image.Tag).X;
            chessBoard[oldRow, oldColumn] = null;
            // Moves the figure
            if (chessBoard[row, column] != null)
            {
                /*if (chessBoard[row, column].Type == FigureType.King || chessBoard[row, column].Type == FigureType.Prince)
                {
                    // Checks the killness of the king or prince
                    if (chessBoard[row, column].Color == PlayerColor.White)
                        isOnlyKingOrPrinceWhite = true;
                    else
                        isOnlyKingOrPrinceBlack = true;
                    // Inawguration
                    if (chessBoard[row, column].Type == FigureType.King)
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
                }*/
                chessBoard[row, column].Image.Visibility = Visibility.Hidden;
            }
            chessBoard[row, column] = choosedFigure;
            chessBoard[row, column].Image.Tag = new Point(column, row);
            chessBoard[row, column].Image.Margin = new Thickness(xMargin + column * edge, yMargin + row * edge, 0, 0);
            currentColor = Next(currentColor);

            /*if (IsCheck() == true)
            {
                MessageBox.Show("Check!");
            }*/
        }
        /*private bool IsCheck()
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
        }*/
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
        public void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            grid.Children.Remove(tempTile);
        }
        public void BoardPlane_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (choosedFigure != null)
            {
                ClearMoves();
                choosedFigure = null;
            }
        }
    }
}
