using chess_console.board;

namespace chess_console.chess
{
    internal class Rook : Piece
    {
        public Rook(Board tab, Color color) : base(tab, color)
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

            return mat;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}