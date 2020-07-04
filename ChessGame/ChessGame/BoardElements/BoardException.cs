using System;

namespace ChessGame.BoardElements
{
    public class BoardException : Exception
    {
        public BoardException(string msg) : base(msg)
        {
        }

        public BoardException()
        {
        }

        public BoardException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}