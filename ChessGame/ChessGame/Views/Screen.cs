using System;
using System.Collections.Generic;
using ChessGame.BoardElements;
using ChessGame.ChessElements;
using ChessGame.GameEngine;
using static System.Console;

namespace ChessGame.Views
{
    public static class Screen
    {
        private const ConsoleColor WHITE_PIECE_COLOR = ConsoleColor.White;
        private const ConsoleColor BLACK_PIECE_COLOR = ConsoleColor.Yellow;
        private const ConsoleColor BOARD_COLOR = ConsoleColor.DarkGray;
        private const ConsoleColor HIGHLIGHT_COLOR = ConsoleColor.DarkGreen;
        private const ConsoleColor BACKGROUND_COLOR = ConsoleColor.Black;

        public static void ShowMatch(ChessMatch match)
        {
            ShowBoard(match.Tab);
            WriteLine();
            ShowCapturedPieces(match);
            WriteLine();
            WriteLine(Resources.Screen_Round, match.Round);
            if (!match.Finished)
            {
                WriteLine(Resources.Screen_WaitingPlayer, ColorLocalized(match.CurrentPlayer));
                if (match.Check)
                {
                    WriteLine(Resources.Screen_Check);
                }
            }
            else
            {
                WriteLine(Resources.Screen_Checkmate);
                WriteLine(Resources.Screen_Winner, ColorLocalized(match.CurrentPlayer));
            }
        }

        public static void ShowCapturedPieces(ChessMatch match)
        {
            WriteLine(Resources.Screen_CapturedPieces);
            Write(Resources.Screen_WhiteCaptured);
            ForegroundColor = WHITE_PIECE_COLOR;
            ShowHashSet(match.CapturedPieces(Color.White));
            ForegroundColor = BOARD_COLOR;
            WriteLine();
            Write(Resources.Screen_BlackCaptured);
            ForegroundColor = BLACK_PIECE_COLOR;
            ShowHashSet(match.CapturedPieces(Color.Black));
            ForegroundColor = BOARD_COLOR;
            WriteLine();
        }

        public static void ShowHashSet(HashSet<Piece> hashSet)
        {
            Write("[");
            foreach (Piece piece in hashSet)
            {
                Write(piece + " ");
            }

            Write("]");
        }

        public static void ShowBoard(Board tab)
        {
            ForegroundColor = BOARD_COLOR;
            for (var i = 0; i < tab.Lines; i++)
            {
                Write(8 - i + " ");
                for (var j = 0; j < tab.Columns; j++)
                {
                    ShowPiece(tab.Piece(i, j));
                    if (j <= tab.Columns - 1) // It's not last line
                    {
                        Write(" ");
                    }
                }

                WriteLine();
            }

            WriteLine(Resources.Screen_Columns);
        }

        public static void ShowBoard(Board tab, bool[,] legalPositions)
        {
            ForegroundColor = BOARD_COLOR;
            for (var i = 0; i < tab.Lines; i++)
            {
                Write(8 - i + " ");
                for (var j = 0; j < tab.Columns; j++)
                {
                    BackgroundColor = legalPositions[i, j] ? HIGHLIGHT_COLOR : BACKGROUND_COLOR;

                    ShowPiece(tab.Piece(i, j));
                    BackgroundColor = BACKGROUND_COLOR;
                    if (j <= tab.Columns - 1) // It's not last line
                    {
                        Write(" ");
                    }
                }

                WriteLine();
            }

            WriteLine(Resources.Screen_Columns);
        }

        public static ChessPosition ReadChessPosition()
        {
            string str = ReadLine();
            if (str == null)
            {
                return null;
            }

            char column = str[0];
            int.TryParse(str[1].ToString(), out int line);
            return new ChessPosition(column, line);
        }

        public static void ShowPiece(Piece piece)
        {
            if (piece == null)
            {
                Write("-");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    ForegroundColor = WHITE_PIECE_COLOR;
                    Write(piece);
                }
                else
                {
                    ForegroundColor = BLACK_PIECE_COLOR;
                    Write(piece);
                }
            }

            ForegroundColor = BOARD_COLOR;
        }

        private static string ColorLocalized(Color color)
        {
            return color == Color.White ? Resources.Screen_White : Resources.Screen_Black;
        }
    }
}