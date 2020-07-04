using chess_console.board;
using chess_console.chess;
using static System.Console;

namespace chess_console
{
    internal static class Program
    {
        private static void Main()
        {
            try
            {
                var partida = new ChessMatch();

                while (!partida.Finished)
                {
                    try
                    {
                        Clear();
                        Tela.ShowMatch(partida);

                        Write("\nOrigem: ");
                        var origem = Tela.ReadChessPosition().ToPosition();
                        partida.ValidateOriginPosition(origem);
                        bool[,] posicoesPossiveis = partida.Tab.Piece(origem).LegalMoves();
                        Clear();
                        Tela.ShowBoard(partida.Tab, posicoesPossiveis);

                        Write("\nDestino: ");
                        var destino = Tela.ReadChessPosition().ToPosition();
                        partida.ValidateDestinationPosition(origem, destino);
                        partida.Play(origem, destino);
                    }
                    catch (BoardException e)
                    {
                        WriteLine(e.Message);
                        ReadLine();
                    }
                }
            }
            catch (BoardException e)
            {
                WriteLine(e.Message);
            }

            ReadLine();
        }
    }
}