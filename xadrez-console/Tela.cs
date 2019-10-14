using System;
using static System.Console;
using System.Collections.Generic;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    internal static class Tela
    {
        public const ConsoleColor CORPECABRANCA = ConsoleColor.White;
        public const ConsoleColor CORPECAPRETA = ConsoleColor.Yellow;
        public const ConsoleColor CORTABULEIRO = ConsoleColor.DarkGray;
        public const ConsoleColor CORDESTAQUE = ConsoleColor.DarkGreen;
        public const ConsoleColor CORFUNDO = ConsoleColor.Black;

        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            ImprimirTabuleiro(partida.Tab);
            WriteLine();
            ImprimirPecasCapturadas(partida);
            WriteLine();
            WriteLine($"Turno: {partida.Turno}");
            if (!partida.Terminada)
            {
                WriteLine($"Aguardando jogada: {partida.JogadorAtual}");
                if (partida.Xeque)
                {
                    WriteLine("XEQUE!");
                }
            }
            else
            {
                WriteLine("XEQUEMATE!");
                WriteLine($"Vencedor: {partida.JogadorAtual}");
            }
        }

        public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            WriteLine("Peças capturadas:");
            Write("Brancas: ");
            ForegroundColor = CORPECABRANCA;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            ForegroundColor = CORTABULEIRO;
            WriteLine();
            Write("Pretas: ");
            ForegroundColor = CORPECAPRETA;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            ForegroundColor = CORTABULEIRO;
            WriteLine();
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Write("[");
            foreach (Peca x in conjunto)
            {
                Write(x + " ");
            }
            Write("]");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            ForegroundColor = CORTABULEIRO;
            for (int i = 0; i < tab.Linhas; i++)
            {
                Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    ImprimirPeca(tab.Peca(i, j));
                    if (j <= tab.Colunas - 1) //não é última linha
                    {
                        Write(" ");
                    }
                }
                WriteLine();
            }
            WriteLine("  a b c d e f g h");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ForegroundColor = CORTABULEIRO;
            for (int i = 0; i < tab.Linhas; i++)
            {
                Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    BackgroundColor = posicoesPossiveis[i, j] ? CORDESTAQUE : CORFUNDO;

                    ImprimirPeca(tab.Peca(i, j));
                    BackgroundColor = CORFUNDO;
                    if (j <= tab.Colunas - 1) //não é última linha
                    {
                        Write(" ");
                    }
                }
                WriteLine();
            }
            WriteLine("  a b c d e f g h");
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string str = ReadLine();
            char coluna = str[0];
            int.TryParse(str[1].ToString(), out int linha);
            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Write("-");
            }
            else
            {
                if (peca.Cor == Cor.Branca)
                {
                    ForegroundColor = CORPECABRANCA;
                    Write(peca);
                }
                else
                {
                    ForegroundColor = CORPECAPRETA;
                    Write(peca);
                }
            }
            ForegroundColor = CORTABULEIRO;
        }
    }
}