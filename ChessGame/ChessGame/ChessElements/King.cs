using ChessGame.BoardElements;
using ChessGame.GameEngine;

namespace ChessGame.ChessElements
{
    internal class King : Piece
    {
        private readonly ChessMatch _match;

        public King(Board tab, Color color, ChessMatch match) : base(tab, color)
        {
            _match = match;
        }

        private bool CanMove(Position pos)
        {
            Piece p = Tab.Piece(pos);
            return p == null || p.Color != Color;
        }

        private bool CastlingTest(Position pos)
        {
            Piece p = Tab.Piece(pos);
            return p is Rook && p.Color == Color && p.Moves == 0;
        }

        public override bool[,] LegalMoves()
        {
            var mat = new bool[Tab.Lines, Tab.Columns];
            var pos = new Position();

            //up
            pos.SetValues(Position.Line - 1, Position.Column);
            if (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //NE
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            if (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //right
            pos.SetValues(Position.Line, Position.Column + 1);
            if (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //SE
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            if (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //down
            pos.SetValues(Position.Line + 1, Position.Column);
            if (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //SO
            pos.SetValues(Position.Line + 1, Position.Column - 1);
            if (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //left
            pos.SetValues(Position.Line, Position.Column - 1);
            if (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            //NO
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            if (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            // #SpecialMove Castling
            if (Moves != 0 || _match.Check)
            {
                return mat;
            }

            // Castling Short
            var posT1 = new Position(Position.Line, Position.Column + 3);
            if (CastlingTest(posT1))
            {
                var p1 = new Position(Position.Line, Position.Column + 1);
                var p2 = new Position(Position.Line, Position.Column + 2);
                if (Tab.Piece(p1) == null && Tab.Piece(p2) == null)
                {
                    mat[Position.Line, Position.Column + 2] = true;
                }
            }

            // Castling Long
            var posT2 = new Position(Position.Line, Position.Column - 4);
            if (CastlingTest(posT2))
            {
                var p1 = new Position(Position.Line, Position.Column - 1);
                var p2 = new Position(Position.Line, Position.Column - 2);
                var p3 = new Position(Position.Line, Position.Column - 3);
                if (Tab.Piece(p1) == null && Tab.Piece(p2) == null && Tab.Piece(p3) == null)
                {
                    mat[Position.Line, Position.Column - 2] = true;
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return Resources.King;
        }
    }
}