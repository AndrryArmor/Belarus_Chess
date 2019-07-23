namespace BelarusChess
{
    /// <summary> Describes the legal move of a chess figure as an offset of rows and columns </summary>
    public struct Move
    {
        public int Rows;
        public int Cols;
        public Move(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
        }
    }

    /// <summary> Class that stores legal moves for all chess figures </summary>
    static class FigureMoves
    {
        /// <summary> Returns legal moves for some chess figure </summary>
        public static Move[,] GetFor(FigureType type, PlayerColor color)
        {
            Move[,] legalMoves = null;
            switch (type)
            {
                case FigureType.Rook:
                    legalMoves = Rook();
                    break;
                case FigureType.Bishop:
                    legalMoves = Bishop();
                    break;
                case FigureType.Knight:
                    legalMoves = Knight();
                    break;
                case FigureType.Prince:
                    legalMoves = Prince();
                    break;
                case FigureType.Queen:
                    legalMoves = Queen();
                    break;
                case FigureType.King:
                    legalMoves = King();
                    break;
                case FigureType.Pawn:
                    legalMoves = Pawn(color);
                    break;
            }
            return legalMoves;
        }
        private static Move[,] Rook()
        {
            Move[,] legalMoves = new Move[4, 8];
            for (int i = 0; i < 8; i++)
            {
                // Up
                legalMoves[0, i] = new Move(0, -(i + 1));
                // Left
                legalMoves[1, i] = new Move(-(i + 1), 0);
                // Down
                legalMoves[2, i] = new Move(0, i + 1);
                // Right
                legalMoves[3, i] = new Move(i + 1, 0);
            }
            return legalMoves;
        }
        private static Move[,] Bishop()
        {
            Move[,] legalMoves = new Move[4, 8];
            for (int i = 0; i < 8; i++)
            {
                // Up-left
                legalMoves[0, i] = new Move(-(i + 1), -(i + 1));
                // Down-left
                legalMoves[1, i] = new Move(-(i + 1), i + 1);
                // Down-right
                legalMoves[2, i] = new Move(i + 1, i + 1);
                // Up-right
                legalMoves[3, i] = new Move(i + 1, -(i + 1));
            }
            return legalMoves;
        }
        private static Move[,] Knight()
        {
            Move[,] legalMoves = new Move[8, 1];

            // Up-left
            legalMoves[0, 0] = new Move(-1, -2);
            // Left-up
            legalMoves[1, 0] = new Move(-2, -1);
            // Left-down
            legalMoves[2, 0] = new Move(-2, 1);
            // Down-left
            legalMoves[3, 0] = new Move(-1, 2);
            // Down-right
            legalMoves[4, 0] = new Move(1, 2);
            // Right-down
            legalMoves[5, 0] = new Move(2, 1);
            // Right-up
            legalMoves[6, 0] = new Move(2, -1);
            // Up-right
            legalMoves[7, 0] = new Move(1, -2);

            return legalMoves;
        }
        private static Move[,] Queen()
        {
            Move[,] legalMoves = new Move[8, 8];
            for (int i = 0; i < 8; i++)
            {
                // Up
                legalMoves[0, i] = new Move(0, -(i + 1));
                // Up-left
                legalMoves[1, i] = new Move(-(i + 1), -(i + 1));
                // Left
                legalMoves[2, i] = new Move(-(i + 1), 0);
                // Down-left
                legalMoves[3, i] = new Move(-(i + 1), i + 1);
                // Down
                legalMoves[4, i] = new Move(0, i + 1);
                // Down-right
                legalMoves[5, i] = new Move(i + 1, i + 1);
                // Right
                legalMoves[6, i] = new Move(i + 1, 0);
                // Up-right
                legalMoves[7, i] = new Move(i + 1, -(i + 1));
            }
            return legalMoves;
        }
        private static Move[,] King()
        {
            Move[,] legalMoves = new Move[8, 1];

            // Up
            legalMoves[0, 0] = new Move(0, -1);
            // Up-left
            legalMoves[1, 0] = new Move(-1, -1);
            // Left
            legalMoves[2, 0] = new Move(-1, 0);
            // Down-left
            legalMoves[3, 0] = new Move(-1, 1);
            // Down
            legalMoves[4, 0] = new Move(0, 1);
            // Down-right
            legalMoves[5, 0] = new Move(1, 1);
            // Right
            legalMoves[6, 0] = new Move(1, 0);
            // Up-right
            legalMoves[7, 0] = new Move(1, -1);

            return legalMoves;
        }
        private static Move[,] Prince()
        {
            Move[,] legalMoves = new Move[8, 2];
            for (int i = 0; i < 2; i++)
            {
                // Up
                legalMoves[0, i] = new Move(0, -(i + 1));
                // Up-left
                legalMoves[1, i] = new Move(-(i + 1), -(i + 1));
                // Left
                legalMoves[2, i] = new Move(-(i + 1), 0);
                // Down-left
                legalMoves[3, i] = new Move(-(i + 1), i + 1);
                // Down
                legalMoves[4, i] = new Move(0, i + 1);
                // Down-right
                legalMoves[5, i] = new Move(i + 1, i + 1);
                // Right
                legalMoves[6, i] = new Move(i + 1, 0);
                // Up-right
                legalMoves[7, i] = new Move(i + 1, -(i + 1));
            }
            return legalMoves;
        }
        private static Move[,] Pawn(PlayerColor color)
        {
            Move[,] legalMoves = new Move[3, 2];
            if (color == PlayerColor.White)
            {
                // Up
                legalMoves[0, 0] = new Move(-1, 0);
                // Double up
                legalMoves[0, 1] = new Move(-2, 0);
                // Up-left
                legalMoves[1, 0] = new Move(-1, -1);
                // Up-right
                legalMoves[2, 0] = new Move(-1, 1);
            }
            else
            {
                // Up
                legalMoves[0, 0] = new Move(1, 0);
                // Double up
                legalMoves[0, 1] = new Move(2, 0);
                // Up-left
                legalMoves[1, 0] = new Move(1, -1);
                // Up-right
                legalMoves[2, 0] = new Move(1, 1);
            }
            return legalMoves;
        }
    }
}
