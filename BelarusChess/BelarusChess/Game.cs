using BelarusChess.Figures;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace BelarusChess
{
    public class Game
    {
        private readonly MainWindow window;
        private readonly Timer oneSecond;
        private int time;
        private Figure choosedFigure;

        public Chessboard Chessboard { get; }
        public PlayerColor CurrentColor { get; private set; }
        public bool IsGameStarted { get; set; } = false;

        public Game(MainWindow window)
        {
            this.window = window;
            Chessboard = new Chessboard(StartGameFigureCollection());
            oneSecond = new System.Timers.Timer { Interval = 1000 };
            oneSecond.Elapsed += OneSecond_Elapsed;
        }
        private Figure[,] StartGameFigureCollection()
        {
            Figure[,] figures = new Figure[9, 9];
            // Black figures
            figures[0, 0] = new Rook(window.imageBlackRook1, PlayerColor.Black, Cell.Create(0, 0));
            figures[0, 1] = new Knight(window.imageBlackKnight1, PlayerColor.Black, Cell.Create(0, 1));
            figures[0, 2] = new Bishop(window.imageBlackBishopW, PlayerColor.Black, Cell.Create(0, 2));
            figures[0, 3] = new Queen(window.imageBlackQueen, PlayerColor.Black, Cell.Create(0, 3));
            figures[0, 4] = new King(window.imageBlackKing, PlayerColor.Black, Cell.Create(0, 4));
            figures[0, 5] = new Prince(window.imageBlackPrince, PlayerColor.Black, Cell.Create(0, 5));
            figures[0, 6] = new Knight(window.imageBlackKnight2, PlayerColor.Black, Cell.Create(0, 6));
            figures[0, 7] = new Bishop(window.imageBlackBishopB, PlayerColor.Black, Cell.Create(0, 7));
            figures[0, 8] = new Rook(window.imageBlackRook2, PlayerColor.Black, Cell.Create(0, 8));
                   
            figures[1, 0] = new BlackPawn(window.imageBlackPawnA, Cell.Create(1, 0));
            figures[1, 1] = new BlackPawn(window.imageBlackPawnB, Cell.Create(1, 1));
            figures[1, 2] = new BlackPawn(window.imageBlackPawnC, Cell.Create(1, 2));
            figures[1, 3] = new BlackPawn(window.imageBlackPawnD, Cell.Create(1, 3));
            figures[1, 4] = new BlackPawn(window.imageBlackPawnE, Cell.Create(1, 4));
            figures[1, 5] = new BlackPawn(window.imageBlackPawnF, Cell.Create(1, 5));
            figures[1, 6] = new BlackPawn(window.imageBlackPawnG, Cell.Create(1, 6));
            figures[1, 7] = new BlackPawn(window.imageBlackPawnH, Cell.Create(1, 7));
            figures[1, 8] = new BlackPawn(window.imageBlackPawnI, Cell.Create(1, 8));

            // Clears middle part of the chess board
            for (int i = 2; i < 7; i++)
                for (int j = 0; j < 9; j++)
                    figures[i, j] = null;

            // White figures
            figures[7, 0] = new WhitePawn(window.imageWhitePawnA, Cell.Create(7, 0));
            figures[7, 1] = new WhitePawn(window.imageWhitePawnB, Cell.Create(7, 1));
            figures[7, 2] = new WhitePawn(window.imageWhitePawnC, Cell.Create(7, 2));
            figures[7, 3] = new WhitePawn(window.imageWhitePawnD, Cell.Create(7, 3));
            figures[7, 4] = new WhitePawn(window.imageWhitePawnE, Cell.Create(7, 4));
            figures[7, 5] = new WhitePawn(window.imageWhitePawnF, Cell.Create(7, 5));
            figures[7, 6] = new WhitePawn(window.imageWhitePawnG, Cell.Create(7, 6));
            figures[7, 7] = new WhitePawn(window.imageWhitePawnH, Cell.Create(7, 7));
            figures[7, 8] = new WhitePawn(window.imageWhitePawnI, Cell.Create(7, 8));

            figures[8, 0] = new Rook(window.imageWhiteRook1, PlayerColor.White, Cell.Create(8, 0));
            figures[8, 1] = new Bishop(window.imageWhiteBishopB, PlayerColor.White, Cell.Create(8, 1));
            figures[8, 2] = new Knight(window.imageWhiteKnight1, PlayerColor.White, Cell.Create(8, 2));
            figures[8, 3] = new Prince(window.imageWhitePrince, PlayerColor.White, Cell.Create(8, 3));
            figures[8, 4] = new King(window.imageWhiteKing, PlayerColor.White, Cell.Create(8, 4));
            figures[8, 5] = new Queen(window.imageWhiteQueen, PlayerColor.White, Cell.Create(8, 5));
            figures[8, 6] = new Bishop(window.imageWhiteBishopW, PlayerColor.White, Cell.Create(8, 6));
            figures[8, 7] = new Knight(window.imageWhiteKnight2, PlayerColor.White, Cell.Create(8, 7));
            figures[8, 8] = new Rook(window.imageWhiteRook2, PlayerColor.White, Cell.Create(8, 8));

            return figures;
        }

        public void Start()
        {
            IsGameStarted = true;
            time = 0;
            oneSecond.Start();
            Chessboard.Reset();
        }

        private PlayerColor Next(PlayerColor color)
        {
            return (color == PlayerColor.White ? PlayerColor.Black : PlayerColor.White);
        }

        public void FindLegalMoves(Figure figure)
        {
            // If not turn of current figure
            if (CurrentColor != figure.Color)
                return;

            choosedFigure = figure;

            // If figure is a pawn
            if (figure.Type == FigureType.BlackPawn || figure.Type == FigureType.WhitePawn)
            {
                FindPawnLegalMoves(figure);
                return;
            }

            Move[,] moves = figure.Moves;
            /// Finds legal moves for figure (except pawn)
            for (int i = 0; i < moves.GetLength(0); i++)
            {
                for (int j = 0; j < moves.GetLength(1); j++)
                {
                    Cell newCell = Cell.Create(figure.Cell.Row + moves[i, j].Rows, figure.Cell.Col + moves[i, j].Cols);

                    /// If cell not legal or contains friendly figure
                    if (newCell == null || (Chessboard[newCell] != null && Chessboard[newCell].Color == CurrentColor))
                        break;

                    string imageUri = (Chessboard[newCell] == null ? window.attackCellImageUri : window.attackFigureImageUri);
                    window.CreateHighlight(window.NewImage(imageUri, newCell));
                        
                    /// If cell contains opponents figure
                    if (Chessboard[newCell] != null || (newCell.Row == 4 && newCell.Col == 4 && figure.Type != FigureType.Prince))
                        break;
                }
            }
        }

        private void FindPawnLegalMoves(Figure figure)
        {
            Move[,] moves = figure.Moves;
            // Move up
            Cell cellUp = Cell.Create(figure.Cell.Row + moves[0, 0].Rows, figure.Cell.Col + moves[0, 0].Cols);
            if (cellUp != null && Chessboard[cellUp] == null)
            {
                window.CreateHighlight(window.NewImage(window.attackCellImageUri, cellUp));

                if ((figure.Color == PlayerColor.White && figure.Cell.Row == 7) || (figure.Color == PlayerColor.Black && figure.Cell.Row == 1))
                {
                    // Double move
                    Cell cellDoubleUp = Cell.Create(figure.Cell.Row + moves[0, 1].Rows, figure.Cell.Col + moves[0, 1].Cols);
                    if (cellDoubleUp != null && Chessboard[cellDoubleUp] == null)
                        window.CreateHighlight(window.NewImage(window.attackCellImageUri, cellDoubleUp));
                }
            }

            // Beat up-left
            Cell cellUpLeft = Cell.Create(figure.Cell.Row + moves[1, 0].Rows, figure.Cell.Col + moves[1, 0].Cols);
            if (cellUpLeft != null && Chessboard[cellUpLeft] != null && Chessboard[cellUpLeft].Color != CurrentColor)
                window.CreateHighlight(window.NewImage(window.attackFigureImageUri, cellUpLeft));

            // Beat up-right
            Cell cellUpRight = Cell.Create(figure.Cell.Row + moves[2, 0].Rows, figure.Cell.Col + moves[2, 0].Cols);
            if (cellUpRight != null && Chessboard[cellUpRight] != null && Chessboard[cellUpRight].Color != CurrentColor)
                window.CreateHighlight(window.NewImage(window.attackFigureImageUri, cellUpRight));
        }

        public void MakeMove(Cell cell)
        {
            Chessboard[choosedFigure.Cell] = null;
            Figure beatenFigure = Chessboard[cell];

            // Moves the choosed figure
            if (Chessboard[cell] != null)
                Chessboard[cell].Image.Visibility = Visibility.Hidden;
            Chessboard[cell] = choosedFigure;
            CurrentColor = Next(CurrentColor);
        }

        public bool IsCheck(Chessboard chessboard)
        {
            return false;
        }

        public void Finish() 
        {
            IsGameStarted = false;
            oneSecond.Stop();
        }

        private void OneSecond_Elapsed(object sender, ElapsedEventArgs e)
        {
            time++;
            int minutes = (time / 60) % 60;
            int seconds = time % 60;
            window.Dispatcher.Invoke(() =>
            {
                window.labelTime.Content = string.Format($"{minutes:00}:{seconds:00}");
            });
        }

        
    }
}
