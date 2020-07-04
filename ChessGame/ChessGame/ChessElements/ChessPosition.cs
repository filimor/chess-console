using ChessGame.BoardElements;

namespace ChessGame.ChessElements
{
    public class ChessPosition
    {
        public ChessPosition(char column, int line)
        {
            Column = column;
            Line = line;
        }

        public char Column { get; set; }
        public int Line { get; set; }

        public Position ToPosition()
        {
            return new Position(8 - Line, Column - 'a');
        }

        public override string ToString()
        {
            return $"{Column}{Line}";
        }
    }
}