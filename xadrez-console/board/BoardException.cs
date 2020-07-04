using System;

namespace chess_console.board
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