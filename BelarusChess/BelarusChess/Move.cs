namespace BelarusChess
{
    /// <summary> Describes the legal move of a chess figure as an offset of rows and columns </summary>
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
