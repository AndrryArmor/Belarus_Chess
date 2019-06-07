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
        private Label labelBlack;           /// Binding of the label of black player
        private Label labelWhite;           /// Binding of the label of white player
        private Figure[,] chessBoard;         /// A massive of Images that shows the location of the figures on the board
        private Image[,] movesBoard;         /// A massive of Images that shows the location of the attacked fields
        private PlayerColor currentColor;
        private Figure choosedFigure;         /// An Image of a figure which is choosed to make a move
        private Image tempTile;
        public bool isStarted = false;
        private bool isOnlyKingOrPrinceWhite = false;
        private bool isOnlyKingOrPrinceBlack = false;
        private int movesSinceThroneOrRokash = 0;
        // Inherited and constant objects
        private readonly double xMargin;
        private readonly double yMargin;
        private const double edge = 55;
        // Image source paths
        private static readonly string projectDirectory = Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.Parent.FullName;
        private readonly string choosedFigureUri = projectDirectory + "\\Resources\\Attack tile.png";
        private readonly string attackImageUri = projectDirectory + "\\Resources\\Attack.png";
        private readonly string attackFigureImageUri = projectDirectory + "\\Resources\\Attack figure.png";
        private readonly string checkImageUri = projectDirectory + "\\Resources\\Check tile.png";
        // Enumerators
        public enum PlayerColor
        {
            White, Black
        }
        private PlayerColor Next(PlayerColor color)
        {
            if (color == PlayerColor.Black)
                return PlayerColor.White;
            else
                return PlayerColor.Black;
        }
        public enum FigureType
        {
            Rook, Bishop, Knight, Prince, King, Queen, Pawn
        }
        public enum MoveType
        {
            Regular, Check, Checkmate, Inauguration, Throne, Rokash, ThroneMine
        }

        public Chess(Grid grid, Image boardPlane, Figure[,] figures, Label labelBlack, Label labelWhite)
        {
            this.grid = grid;
            this.boardPlane = boardPlane;
            this.figures = figures;
            this.labelBlack = labelBlack;
            this.labelWhite = labelWhite;

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
            InitializeImages(figures);
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
        private void InitializeImages(Figure[,] figures)
        {
            for (int i = 0; i < figures.GetLength(0); i++)
            {
                for (int j = 0; j < figures.GetLength(1); j++)
                {
                    if (figures[i, j].Color == Chess.PlayerColor.Black)
                        figures[i, j].Image.Tag = new Point(j, i);
                    else
                        figures[i, j].Image.Tag = new Point(j, i + 5);

                }
            }
        }

        public void FindMoves(Image imageFigure)
        {
            // Finds the figure which is represented by imageFigure
            int rowClicked = (int)((Point)imageFigure.Tag).Y;
            int columnClicked = (int)((Point)imageFigure.Tag).X;
            Figure figure = chessBoard[rowClicked, columnClicked];
            // If not turn of current figure
            if (currentColor != figure.Color)
                return;
            if (choosedFigure != null)
                ClearMoves();

            choosedFigure = figure;
            movesBoard[rowClicked, columnClicked] = NewImage(choosedFigureUri, rowClicked, columnClicked, 1);

            Moves[,] moves = choosedFigure.Moves();
            // Special case for pawn
            if (choosedFigure.Type == FigureType.Pawn)
            {
                PawnMoves(moves, rowClicked, columnClicked);
                return;
            }
            // Finds accessable moves for figure
            for (int i = 0; i < moves.GetLength(0); i++)
            {
                for (int j = 0; j < moves.GetLength(1); j++)
                {
                    int row = rowClicked + moves[i, j].Y;
                    int column = columnClicked + moves[i, j].X;
                    if (row < 0 || row > 8 || column < 0 || column > 8) break; /// Borders case
                    if (figure.Type != FigureType.King && figure.Type != FigureType.Prince &&
                        row == 4 && column == 4)    /// All figures except the prince and knight cannot move over the throne
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
            if (movesBoard[oldRow, oldColumn] != null)
            {
                grid.Children.Remove(movesBoard[oldRow, oldColumn]);
                movesBoard[oldRow, oldColumn] = null;
            }
            chessBoard[oldRow, oldColumn] = null;
            Figure oldFigure = chessBoard[row, column];
            // Moves the choosed figure
            if (chessBoard[row, column] != null)
            {
                chessBoard[row, column].Image.Visibility = Visibility.Hidden;
            }
            chessBoard[row, column] = choosedFigure;
            chessBoard[row, column].Image.Tag = new Point(column, row);
            chessBoard[row, column].Image.Margin = new Thickness(xMargin + column * edge, yMargin + row * edge, 0, 0);
            currentColor = Next(currentColor);
            CheckForSpecialCases(oldFigure);
        }
        private void CheckForSpecialCases(Figure figure)
        {
            string message = "";
            if (IsInauguration(figure) == true)
            {
                message += "Інавгурація";
            }
            if (IsCheck(figure) == true)
            {
                if (message != "")
                    message += "; ";
                message += "Шах!";

                Figure king;
                if (currentColor == PlayerColor.Black)
                    king = figures[0, 4];
                else
                    king = figures[3, 4];

                int row = (int)((Point)king.Image.Tag).Y;
                int column = (int)((Point)king.Image.Tag).X;
                movesBoard[row, column] = NewImage(checkImageUri, row, column, 1);
            }
            if (IsThrone() == true)
            {
                if (movesSinceThroneOrRokash == 1)
                    message = "Трон мій!";
                else
                    message = "Трон!";
            }

            if (currentColor == PlayerColor.Black)
                labelWhite.Content = message;
            else
                labelBlack.Content = message;
        }

        private bool IsInauguration(Figure figure)
        {
            // Checks whether two contenders (претенденти) of throne are both alive (king and prince)
            if (figure != null && figure.Color == PlayerColor.Black && isOnlyKingOrPrinceBlack == false &&
                (figure.Type == FigureType.King || figure.Type == FigureType.Prince))
            {
                isOnlyKingOrPrinceBlack = true;
                // If black king was killed
                if (figure.Type == FigureType.King)
                {
                    int rowKing = (int)((Point)figures[0, 4].Image.Tag).Y;
                    int columnKing = (int)((Point)figures[0, 4].Image.Tag).X;
                    int rowPrince = (int)((Point)figures[0, 5].Image.Tag).Y;
                    int columnPrince = (int)((Point)figures[0, 5].Image.Tag).X;

                    figure.Image.Tag = figures[0, 5].Image.Tag;
                    figure.Image.Margin = new Thickness(xMargin + columnPrince * edge, yMargin + rowPrince * edge, 0, 0);
                    chessBoard[rowPrince, columnPrince].Image.Visibility = Visibility.Hidden;
                    chessBoard[rowPrince, columnPrince] = figure;
                    chessBoard[rowPrince, columnPrince].Image.Visibility = Visibility.Visible;
                    return true;
                }
            }
            else if (figure != null && figure.Color == PlayerColor.White && isOnlyKingOrPrinceWhite == false &&
                     (figure.Type == FigureType.King || figure.Type == FigureType.Prince))
            {
                isOnlyKingOrPrinceWhite = true;
                // If white king was killed
                if (figure.Type == FigureType.King)
                {
                    int rowKing = (int)((Point)figures[0, 4].Image.Tag).Y;
                    int columnKing = (int)((Point)figures[0, 4].Image.Tag).X;
                    int rowPrince = (int)((Point)figures[3, 3].Image.Tag).Y;
                    int columnPrince = (int)((Point)figures[3, 3].Image.Tag).X;

                    figure.Image.Tag = chessBoard[rowPrince, columnPrince].Image.Tag;
                    figure.Image.Margin = new Thickness(xMargin + columnPrince * edge, yMargin + rowPrince * edge, 0, 0);
                    chessBoard[rowPrince, columnPrince].Image.Visibility = Visibility.Hidden;
                    chessBoard[rowPrince, columnPrince] = figure;
                    chessBoard[rowPrince, columnPrince].Image.Visibility = Visibility.Visible;
                    return true;

                }
            }
            return false;
        }
        private bool IsCheck(Figure figure)
        {
            King king;
            if (currentColor == PlayerColor.Black && isOnlyKingOrPrinceBlack == true)
            {
                king = (King)figures[0, 4];
            }
            else if (currentColor == PlayerColor.White && isOnlyKingOrPrinceWhite == true)
            {
                king = (King)figures[3, 4];
            }
            else
                return false;
            
            Moves[,] moves = king.AttackDirections();
            int rowKing = (int)((Point)king.Image.Tag).Y;
            int columnKing = (int)((Point)king.Image.Tag).X;
            for (int i = 0; i < moves.GetLength(0); i++)
            {
                for (int j = 0; j < moves.GetLength(1); j++)
                {
                    int row = rowKing + moves[i, j].Y;
                    int column = columnKing + moves[i, j].X;
                    if (row < 0 || row > 8 || column < 0 || column > 8)
                        break;              // Borders case
                    if (row == 4 && column == 4)     // Simple figures cannot move over the throne
                    {
                        if (chessBoard[row, column] != null && chessBoard[row, column].Color != currentColor)
                        {
                            /*if (chessBoard[row, column].Type == )*/
                            return true;
                        }
                        break;
                    }
                    if (chessBoard[row, column] != null)
                    {
                        if (chessBoard[row, column].Color != currentColor)
                        {
                            return true;
                        }
                        break;
                    }
                }
            }
            return false;                
        }
        private bool IsThrone()
        {
            if (choosedFigure.Type == FigureType.King && ((Point)choosedFigure.Image.Tag).X == 4
                                                      && ((Point)choosedFigure.Image.Tag).X == 4)
            {
                return true;
            }
            else
                return false;
        }

            /*/ Finds the figure which is represented by imageFigure
            int newRow = (int)((Point)choosedFigure.Image.Tag).Y;
            int newColumn = (int)((Point)choosedFigure.Image.Tag).X;
            Figure figure = chessBoard[newRow, newColumn];
            // If not turn of current figure
            if (currentColor != figure.Color)
                return false;

            bool result = false;*/

        private void ClearMoves()
        {
            grid.Children.Remove(tempTile);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (movesBoard[i, j] != null && movesBoard[i, j].Tag.ToString() != checkImageUri)
                    {
                        grid.Children.Remove(movesBoard[i, j]);
                        movesBoard[i, j] = null;
                    }
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
