using System.Collections.Generic;
using System.Linq;
using ChessGame.BoardElements;
using ChessGame.ChessElements;

namespace ChessGame.GameEngine
{
    public class ChessMatch
    {
        private readonly HashSet<Piece> _captured = new HashSet<Piece>();
        private readonly HashSet<Piece> _pieces = new HashSet<Piece>();

        public ChessMatch()
        {
            PutPieces();
        }

        public int Round { get; private set; } = 1;
        public Color CurrentPlayer { get; private set; }
        public Board Tab { get; } = new Board(8, 8);
        public bool Finished { get; private set; }
        public bool Check { get; private set; }
        public Piece EnPassantVulnerable { get; private set; }

        public Piece DoMove(Position origin, Position destination)
        {
            Piece p = Tab.RemovePiece(origin);
            p.IncreaseMoves();
            Piece capturedPiece = Tab.RemovePiece(destination);
            Tab.PutPiece(p, destination);
            if (capturedPiece != null)
            {
                _captured.Add(capturedPiece);
            }

            Position rookOrigin;
            Position rookDestination;
            Piece tempPiece;

            switch (p)
            {
                // #SpecialMove Castling Short
                case King _ when destination.Column == origin.Column + 2:
                    rookOrigin = new Position(origin.Line, origin.Column + 3);
                    rookDestination = new Position(origin.Line, origin.Column + 1);
                    tempPiece = Tab.RemovePiece(rookOrigin);
                    tempPiece.IncreaseMoves();
                    Tab.PutPiece(tempPiece, rookDestination);
                    break;

                // #SpecialMove Castling Long
                case King _ when destination.Column == origin.Column - 2:
                {
                    rookOrigin = new Position(origin.Line, origin.Column - 4);
                    rookDestination = new Position(origin.Line, origin.Column - 1);
                    tempPiece = Tab.RemovePiece(rookOrigin);
                    tempPiece.IncreaseMoves();
                    Tab.PutPiece(tempPiece, rookDestination);
                    break;
                }
                // #SpecialMove En Passant
                case Pawn _ when origin.Column != destination.Column && capturedPiece == null:
                {
                    Position pawnPos = p.Color == Color.White
                        ? new Position(destination.Line + 1, destination.Column)
                        : new Position(destination.Line - 1, destination.Column);
                    capturedPiece = Tab.RemovePiece(pawnPos);
                    _captured.Add(capturedPiece);
                    break;
                }
            }

            return capturedPiece;
        }

        public void UndoMove(Position origin, Position destination, Piece capturedPiece)
        {
            Piece p = Tab.RemovePiece(destination);
            p.DecreaseMoves();
            if (capturedPiece != null)
            {
                Tab.PutPiece(capturedPiece, destination);
                _captured.Remove(capturedPiece);
            }

            Tab.PutPiece(p, origin);

            Position rookOrigin;
            Position rookDestination;
            Piece tempPiece;

            switch (p)
            {
                // #SpecialMove Castling Short
                case King _ when destination.Column == origin.Column + 2:

                    rookOrigin = new Position(origin.Line, origin.Column + 3);
                    rookDestination = new Position(origin.Line, origin.Column + 1);
                    tempPiece = Tab.RemovePiece(rookDestination);
                    tempPiece.DecreaseMoves();
                    Tab.PutPiece(tempPiece, rookOrigin);
                    break;
                // #SpecialMove Castling Long
                case King _ when destination.Column == origin.Column - 2:
                {
                    rookOrigin = new Position(origin.Line, origin.Column - 4);
                    rookDestination = new Position(origin.Line, origin.Column - 1);
                    tempPiece = Tab.RemovePiece(rookDestination);
                    tempPiece.DecreaseMoves();
                    Tab.PutPiece(tempPiece, rookOrigin);
                    break;
                }
                // #SpecialMove En Passant
                case Pawn _ when origin.Column != destination.Column && capturedPiece == EnPassantVulnerable:
                {
                    Piece pawn = Tab.RemovePiece(destination);
                    Position pawnPos = p.Color == Color.White
                        ? new Position(3, destination.Column)
                        : new Position(4, destination.Column);
                    Tab.PutPiece(pawn, pawnPos);
                    break;
                }
            }
        }

        public void Play(Position origin, Position destination)
        {
            Piece capturedPiece = DoMove(origin, destination);

            if (InCheck(CurrentPlayer))
            {
                UndoMove(origin, destination, capturedPiece);
                throw new BoardException(Resources.ChessMatch_AutoCheck);
            }

            Piece p = Tab.Piece(destination);

            // #SpecialMove Promotion
            if (p is Pawn)
            {
                if ((p.Color == Color.White && destination.Line == 0) ||
                    (p.Color == Color.Black && destination.Line == 7))
                {
                    p = Tab.RemovePiece(destination);
                    _pieces.Remove(p);
                    Piece queen = new Queen(Tab, p.Color);
                    Tab.PutPiece(queen, destination);
                    _pieces.Add(queen);
                }
            }

            Check = InCheck(Competitor(CurrentPlayer));
            if (CheckMateTest(Competitor(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Round++;
                SwitchPlayer();
            }

            //#SpecialMove En Passant

            EnPassantVulnerable =
                (p is Pawn && destination.Line == origin.Line - 2) || destination.Line == origin.Line + 2
                    ? p
                    : null;
        }

        public void ValidateOriginPosition(Position pos)
        {
            if (Tab.Piece(pos) == null)
            {
                throw new BoardException(Resources.ChessMatch_NoPiece);
            }

            if (CurrentPlayer != Tab.Piece(pos).Color)
            {
                throw new BoardException(Resources.ChessMatch_NotYourPiece);
            }

            if (!Tab.Piece(pos).CanMove())
            {
                throw new BoardException(Resources.ChessMatch_NoPossibleMoves);
            }
        }

        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!Tab.Piece(origin).LegalMove(destination))
            {
                throw new BoardException(Resources.ChessMatch_InvalidDestination);
            }
        }

        private void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            var aux = new HashSet<Piece>();
            foreach (Piece piece in _captured.Where(piece => piece.Color == color))

            {
                aux.Add(piece);
            }

            return aux;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            var aux = new HashSet<Piece>();
            foreach (Piece item in _pieces.Where(item => item.Color == color))
            {
                aux.Add(item);
            }

            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        private static Color Competitor(Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }

        private Piece Rei(Color color)
        {
            return PiecesInGame(color).OfType<King>().FirstOrDefault();
        }

        public bool InCheck(Color color)
        {
            Piece king = Rei(color);
            return PiecesInGame(color).Select(piece => piece.LegalMoves())
                .Any(mat => mat[king.Position.Line, king.Position.Column]);
        }

        public bool CheckMateTest(Color color)
        {
            if (!InCheck(color))
            {
                return false;
            }

            foreach (Piece piece in PiecesInGame(color))
            {
                bool[,] mat = piece.LegalMoves();
                for (var i = 0; i < Tab.Lines; i++)
                {
                    for (var j = 0; j < Tab.Columns; j++)
                    {
                        if (!mat[j, j])
                        {
                            continue;
                        }

                        Position origin = piece.Position;
                        var destination = new Position(i, j);
                        Piece capturedPiece = DoMove(origin, destination);
                        bool inCheck = InCheck(color);
                        UndoMove(origin, destination, capturedPiece);
                        if (!inCheck)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public void PutNewPiece(char column, int line, Piece piece)
        {
            Tab.PutPiece(piece, new ChessPosition(column, line).ToPosition());
            _pieces.Add(piece);
        }

        private void PutPieces()
        {
            PutNewPiece('a', 1, new Rook(Tab, Color.White));
            PutNewPiece('b', 1, new Knight(Tab, Color.White));
            PutNewPiece('c', 1, new Bishop(Tab, Color.White));
            PutNewPiece('d', 1, new Queen(Tab, Color.White));
            PutNewPiece('e', 1, new King(Tab, Color.White, this));
            PutNewPiece('f', 1, new Bishop(Tab, Color.White));
            PutNewPiece('g', 1, new Knight(Tab, Color.White));
            PutNewPiece('h', 1, new Rook(Tab, Color.White));
            PutNewPiece('a', 2, new Pawn(Tab, Color.White, this));
            PutNewPiece('b', 2, new Pawn(Tab, Color.White, this));
            PutNewPiece('c', 2, new Pawn(Tab, Color.White, this));
            PutNewPiece('d', 2, new Pawn(Tab, Color.White, this));
            PutNewPiece('e', 2, new Pawn(Tab, Color.White, this));
            PutNewPiece('f', 2, new Pawn(Tab, Color.White, this));
            PutNewPiece('g', 2, new Pawn(Tab, Color.White, this));
            PutNewPiece('h', 2, new Pawn(Tab, Color.White, this));

            PutNewPiece('a', 8, new Rook(Tab, Color.Black));
            PutNewPiece('b', 8, new Knight(Tab, Color.Black));
            PutNewPiece('c', 8, new Bishop(Tab, Color.Black));
            PutNewPiece('d', 8, new Queen(Tab, Color.Black));
            PutNewPiece('e', 8, new King(Tab, Color.Black, this));
            PutNewPiece('f', 8, new Bishop(Tab, Color.Black));
            PutNewPiece('g', 8, new Knight(Tab, Color.Black));
            PutNewPiece('h', 8, new Rook(Tab, Color.Black));
            PutNewPiece('a', 7, new Pawn(Tab, Color.Black, this));
            PutNewPiece('b', 7, new Pawn(Tab, Color.Black, this));
            PutNewPiece('c', 7, new Pawn(Tab, Color.Black, this));
            PutNewPiece('d', 7, new Pawn(Tab, Color.Black, this));
            PutNewPiece('e', 7, new Pawn(Tab, Color.Black, this));
            PutNewPiece('f', 7, new Pawn(Tab, Color.Black, this));
            PutNewPiece('g', 7, new Pawn(Tab, Color.Black, this));
            PutNewPiece('h', 7, new Pawn(Tab, Color.Black, this));
        }
    }
}