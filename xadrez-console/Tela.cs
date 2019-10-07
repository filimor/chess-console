using System;
using tabuleiro;

namespace xadrez_console
{
    class Tela
    {
        public const ConsoleColor CORPRECABRANCA = ConsoleColor.White;
        public const ConsoleColor CORPECAPRETA = ConsoleColor.Yellow;
        public const ConsoleColor CORTABULEIRO = ConsoleColor.DarkGray;

        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            Console.ForegroundColor = CORTABULEIRO;
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (tab.Peca(i, j) == null) //não tem peça
                    {
                        Console.Write("-");
                    }
                    else // tem peça
                    {
                        ImprimirPeca(tab.Peca(i, j));
                    }
                    if (j == tab.Colunas - 1) //não é última linha
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirPeca(Peca peca)
        {
            if(peca.Cor == Cor.Branca)
            {
                Console.ForegroundColor = CORPRECABRANCA;
                Console.Write(peca);
            }
            else
            {
                Console.ForegroundColor = CORPECAPRETA;
                Console.Write(peca);
            }
            Console.ForegroundColor = CORTABULEIRO;
        }
    }
}
