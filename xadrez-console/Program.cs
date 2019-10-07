using System;
using tabuleiro;

namespace xadrez_console
{
    static class Program
    {
        private static void Main()
        {
            var P = new Posicao(3, 4);
            Console.WriteLine($"Posição: {P}");
        }
    }
}
