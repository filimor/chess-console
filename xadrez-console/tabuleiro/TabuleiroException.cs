﻿using System;

namespace tabuleiro
{
    internal class TabuleiroException : Exception
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