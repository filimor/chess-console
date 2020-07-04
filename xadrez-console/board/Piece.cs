namespace chess_console.board
{
    public abstract class Piece
    {
        protected Piece(Board tab, Color color)
        {
            Tab = tab;
            Color = color;
        }

        public Position Position { get; set; }

        public Color Color { get; protected set; }
        public int Moves { get; protected set; }
        public Board Tab { get; protected set; }

        public void IncreaseMoves()
        {
            Moves++;
        }

        public void DecreaseMoves()
        {
            Moves--;
        }

        public bool CanMove()
        {
            var mat = LegalMoves();
            for (var i = 0; i < Tab.Lines; i++)
            {
                for (var j = 0; j < Tab.Columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool LegalMove(Position pos)
        {
            Tab.ValidatePosition(pos);
            return LegalMoves()[pos.Line, pos.Column];
        }

        public abstract bool[,] LegalMoves();
    }
}