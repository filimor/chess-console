using System;
using tabuleiro;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (tab.Peca(i, j) == null) //não tem peça
                    {
                        Console.Write("-");
                    }
                    else // tem peça
                    {
                        Console.Write(tab.Peca(i, j));
                    }
                    if (j == tab.Colunas - 1) //não é última linha
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
