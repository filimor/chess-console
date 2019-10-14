using static System.Console;
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
                    try
                    {
                        Clear();
                        Tela.ImprimirPartida(partida);

                        Write("\nOrigem: ");
                        var origem = Tela.LerPosicaoXadrez().ToPosicao();
                        partida.ValidarPosicaoOrigem(origem);
                        bool[,] posicoesPossiveis = partida.Tab.Peca(origem).MovimentosPossiveis();
                        Clear();
                        Tela.ImprimirTabuleiro(partida.Tab, posicoesPossiveis);

                        Write("\nDestino: ");
                        var destino = Tela.LerPosicaoXadrez().ToPosicao();
                        partida.ValidarPosicaoDestino(origem, destino);
                        partida.RealizarJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        WriteLine(e.Message);
                        ReadLine();
                    }
                }
            }
            catch (TabuleiroException e)
            {
                WriteLine(e.Message);
            }

            ReadLine();
        }
    }
}