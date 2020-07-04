using chess_console.board;

namespace chess_console.chess
{
    internal class Knight : Piece
    {
        public Knight(Board tab, Color color) : base(tab, color)
        {
        }

        private bool CanMove(Position pos)
        {
            var p = Tab.Piece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] LegalMoves()
        {
            var mat = new bool[Tab.Lines, Tab.Columns];
            var pos = new Position();

            pos.SetValues(Position.Line - 1, Position.Column - 2);
            if (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line - 2, Position.Column - 1);
            if (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line - 2, Position.Column + 1);
            if (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line - 1, Position.Column + 2);
            if (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line + 1, Position.Column + 2);
            if (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line + 2, Position.Column + 1);
            if (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line + 2, Position.Column - 1);
            if (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(Position.Line + 1, Position.Column - 2);
            if (Tab.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }

        public override string ToString()
        {
            return "N";
        }
    }
}