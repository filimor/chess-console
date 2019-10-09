﻿using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Tela
    {
        public const ConsoleColor CORPRECABRANCA = ConsoleColor.White;
        public const ConsoleColor CORPECAPRETA = ConsoleColor.Yellow;
        public const ConsoleColor CORTABULEIRO = ConsoleColor.DarkGray;
        public const ConsoleColor CORDESTAQUE = ConsoleColor.DarkGreen;
        public const ConsoleColor CORFUNDO = ConsoleColor.Black;

        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            Console.ForegroundColor = CORTABULEIRO;
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    ImprimirPeca(tab.Peca(i, j));
                    if (j == tab.Colunas - 1) //não é última linha
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            Console.ForegroundColor = CORTABULEIRO;
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    Console.BackgroundColor = posicoesPossiveis[i, j] ? CORDESTAQUE : CORFUNDO;

                    ImprimirPeca(tab.Peca(i, j));
                    Console.BackgroundColor = CORFUNDO;
                    if (j == tab.Colunas - 1) //não é última linha
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string str = Console.ReadLine();
            char coluna = str[0];
            int.TryParse(str[1].ToString(), out int linha);
            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null) //não tem peça
            {
                Console.Write("- ");
            }
            else // tem peça
            {
                if (peca.Cor == Cor.Branca)
                {
                    Console.ForegroundColor = CORPRECABRANCA;
                    Console.Write(peca + " ");
                }
                else
                {
                    Console.ForegroundColor = CORPECAPRETA;
                    Console.Write(peca + " ");
                }
            }
            Console.ForegroundColor = CORTABULEIRO;
        }
    }
}
