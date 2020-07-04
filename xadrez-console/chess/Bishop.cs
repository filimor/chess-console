using chess_console.board;

namespace chess_console.chess
{
    internal class Bishop : Piece
    {
        public Bishop(Board tab, Color color) : base(tab, color)
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
            return "B";
        }
    }
}