using System;
using tabuleiro;

namespace xadrez_console
{
    internal static class Program
    {
        private static void Main()
        {
            try
            {
                var tab = new Tabuleiro(8, 8);
                Tela.ImprimirTabuleiro(tab);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
