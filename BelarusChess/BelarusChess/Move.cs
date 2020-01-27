namespace BelarusChess
{
    /// <summary> Describes the valid move of a piece as an offset of rows and columns </summary>
    public struct Move
    {
        public int Rows { get; }
        public int Cols { get; }

        public Move(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
        }
    }
}
