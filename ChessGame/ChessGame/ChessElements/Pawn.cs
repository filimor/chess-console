using ChessGame.BoardElements;
using ChessGame.GameEngine;

namespace ChessGame.ChessElements
{
    internal class Pawn : Piece
    {
        private readonly ChessMatch _match;

        public Pawn(Board tab, Color color, ChessMatch match) : base(tab, color)
        {
            _match = match;
        }

        private bool ExistsEnemy(Position pos)
        {
            Piece p = Tab.Piece(pos);
            return p != null && p.Color != Color;
        }

        private bool FreePosition(Position pos)
        {
            return Tab.Piece(pos) == null;
        }

        public override bool[,] LegalMoves()
        {
            var mat = new bool[Tab.Lines, Tab.Columns];
            var pos = new Position();

            if (Color == Color.White)
            {
                pos.SetValues(Position.Line - 1, Position.Column);
                if (Tab.LegalPosition(pos) && FreePosition(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line - 2, Position.Column);
                if (Tab.LegalPosition(pos) && FreePosition(pos) && Moves == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line - 1, Position.Column - 1);
                if (Tab.LegalPosition(pos) && ExistsEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line - 1, Position.Column + 1);
                if (Tab.LegalPosition(pos) && ExistsEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                // #SpecialMove En Passant
                if (Position.Line == 3)
                {
                    var left = new Position(Position.Line, Position.Column - 1);
                    if (Tab.LegalPosition(left) && ExistsEnemy(left) && Tab.Piece(left) == _match.EnPassantVulnerable)
                    {
                        mat[left.Line - 1, left.Column] = true;
                    }

                    var right = new Position(Position.Line, Position.Column + 1);
                    if (Tab.LegalPosition(right) && ExistsEnemy(right) && Tab.Piece(right) == _match.EnPassantVulnerable
                    )
                    {
                        mat[right.Line - 1, right.Column] = true;
                    }
                }
            }
            else
            {
                pos.SetValues(Position.Line + 1, Position.Column);
                if (Tab.LegalPosition(pos) && FreePosition(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line + 2, Position.Column);
                if (Tab.LegalPosition(pos) && FreePosition(pos) && Moves == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line + 1, Position.Column - 1);
                if (Tab.LegalPosition(pos) && ExistsEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(Position.Line + 1, Position.Column + 1);
                if (Tab.LegalPosition(pos) && ExistsEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                // #SpecialMove En Passant
                if (Position.Line == 4)
                {
                    var left = new Position(Position.Line, Position.Column - 1);
                    if (Tab.LegalPosition(left) && ExistsEnemy(left) && Tab.Piece(left) == _match.EnPassantVulnerable)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }

                    var right = new Position(Position.Line, Position.Column + 1);
                    if (Tab.LegalPosition(right) && ExistsEnemy(right) && Tab.Piece(right) == _match.EnPassantVulnerable
                    )
                    {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return Resources.Pawn;
        }
    }
}