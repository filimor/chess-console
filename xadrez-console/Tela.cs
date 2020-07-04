using chess_console.board;
using chess_console.chess;
using System;
using System.Collections.Generic;
using static System.Console;

namespace chess_console
{
    public static class Tela
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
            WriteLine($"Round: {match.Round}");
            if (!match.Finished)
            {
                WriteLine($"Aguardando jogada: {match.CurrentPlayer}");
                if (match.Check) WriteLine("XEQUE!");
            }
            else
            {
                WriteLine("XEQUEMATE!");
                WriteLine($"Vencedor: {match.CurrentPlayer}");
            }
        }

        public static void ShowCapturedPieces(ChessMatch match)
        {
            WriteLine("Peças capturadas:");
            Write("Brancas: ");
            ForegroundColor = WHITE_PIECE_COLOR;
            ShowHashSet(match.CapturedPieces(Color.White));
            ForegroundColor = BOARD_COLOR;
            WriteLine();
            Write("Pretas: ");
            ForegroundColor = BLACK_PIECE_COLOR;
            ShowHashSet(match.CapturedPieces(Color.Black));
            ForegroundColor = BOARD_COLOR;
            WriteLine();
        }

        public static void ShowHashSet(HashSet<Piece> hashSet)
        {
            Write("[");
            foreach (var piece in hashSet) Write(piece + " ");
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
                        Write(" ");
                }

                WriteLine();
            }

            WriteLine("  a b c d e f g h");
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
                        Write(" ");
                }

                WriteLine();
            }

            WriteLine("  a b c d e f g h");
        }

        public static ChessPosition ReadChessPosition()
        {
            var str = ReadLine();
            if (str == null) return null;
            var column = str[0];
            int.TryParse(str[1].ToString(), out var line);
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
    }
}