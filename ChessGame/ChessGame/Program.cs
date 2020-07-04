using ChessGame.BoardElements;
using ChessGame.GameEngine;
using ChessGame.Views;
using static System.Console;

namespace ChessGame
{
    internal static class Program
    {
        private static void Main()
        {
            try
            {
                var match = new ChessMatch();

                while (!match.Finished)
                {
                    try
                    {
                        Clear();
                        Screen.ShowMatch(match);

                        Write(Resources.Screen_Origin);
                        var origin = Screen.ReadChessPosition().ToPosition();
                        match.ValidateOriginPosition(origin);
                        bool[,] legalPositions = match.Tab.Piece(origin).LegalMoves();
                        Clear();
                        Screen.ShowBoard(match.Tab, legalPositions);

                        Write(Resources.Screen_Destination);
                        var destination = Screen.ReadChessPosition().ToPosition();
                        match.ValidateDestinationPosition(origin, destination);
                        match.Play(origin, destination);
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