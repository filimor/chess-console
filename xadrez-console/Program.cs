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
                    Console.WriteLine($"\nTurno: {partida.Turno}");
                    Console.WriteLine($"\nAguardando jogada: {partida.JogadorAtual}");


                    Console.Write("\nOrigem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().toPosicao();
                    partida.ValidarPosicaoOrigem(origem);
                    bool[,] posicoesPossiveis = partida.Tab.Peca(origem).MovimentosPossiveis();
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tab, posicoesPossiveis);

                    Console.Write("\nDestino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().toPosicao();
                    partida.ValidarPosicaoDestino(origem, destino);
                    partida.RealizarJogada(origem, destino);

                }
                
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
