using ChessGame.BoardElements;

namespace ChessGame.ChessElements
{
    internal class Queen : Piece
    {
        public Queen(Board tab, Color color) : base(tab, color)
        {
        }

        private bool CanMove(Position pos)
        {
            Piece p = Tab.Piece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] LegalMoves()
        {
            var mat = new bool[Tab.Lines, Tab.Columns];
            var pos = new Position();

            //up
            pos.SetValues(Position.Line - 1, Position.Column);
            while (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                {
                    break;
                }

                pos.Line--;
            }

            //down
            pos.SetValues(Position.Line + 1, Position.Column);
            while (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                {
                    break;
                }

                pos.Line++;
            }

            //right
            pos.SetValues(Position.Line, Position.Column + 1);
            while (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                {
                    break;
                }

                pos.Column++;
            }

            //left
            pos.SetValues(Position.Line, Position.Column - 1);
            while (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                {
                    break;
                }

                pos.Column--;
            }

            //NE
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            while (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                {
                    break;
                }

                pos.SetValues(pos.Line - 1, pos.Column + 1);
            }

            //SE
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            while (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                {
                    break;
                }

                pos.SetValues(pos.Line + 1, pos.Column + 1);
            }

            //SO
            pos.SetValues(Position.Line + 1, Position.Column - 1);
            while (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                {
                    break;
                }

                pos.SetValues(pos.Line + 1, pos.Column - 1);
            }

            //NO
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            while (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Tab.Piece(pos) != null && Tab.Piece(pos).Color != Color)
                {
                    break;
                }

                pos.SetValues(pos.Line - 1, pos.Column - 1);
            }

            return mat;
        }

        public override string ToString()
        {
            return Resources.Queen;
        }
    }
}