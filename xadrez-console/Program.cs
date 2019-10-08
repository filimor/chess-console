using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    internal static class Program
    {
        private static void Main()
        {
            try
            {
                var partida = new PartidaDeXadrez();

                while (!partida.Terminada)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tab);

                    Console.Write("\nOrigem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().toPosicao();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().toPosicao();

                    partida.ExecutaMovimento(origem, destino);

                }
                
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
