using System;

namespace tabuleiro
{
    class TabuleiroException : Exception
    {
        public TabuleiroException(string msg) : base(msg)
        {

        }

        public TabuleiroException() : base()
        {
        }

        public TabuleiroException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
