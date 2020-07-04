namespace chess_console.board
{
    public class Board
    {
        private readonly Piece[,] _pieces;

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            _pieces = new Piece[lines, columns];
        }

        public int Lines { get; set; }
        public int Columns { get; set; }

        public Piece Piece(int line, int column)
        {
            return _pieces[line, column];
        }

        public Piece Piece(Position pos)
        {
            ValidatePosition(pos);
            return _pieces[pos.Line, pos.Column];
        }

        public bool ExistsPiece(Position pos)
        {
            ValidatePosition(pos);
            return Piece(pos) != null;
        }

        public void PutPiece(Piece p, Position pos)
        {
            if (ExistsPiece(pos)) throw new BoardException("Já existe uma peça nessa posição!");

            _pieces[pos.Line, pos.Column] = p;
            p.Position = pos;
        }

        public Piece RemovePiece(Position pos)
        {
            if (Piece(pos) == null) return null;

            var aux = Piece(pos);
            _pieces[pos.Line, pos.Column] = null;
            return aux;
        }

        public bool LegalPosition(Position pos)
        {
            return pos.Line >= 0 && pos.Line < Lines &&
                   pos.Column >= 0 && pos.Column < Columns;
        }

        public void ValidatePosition(Position pos)
        {
            if (!LegalPosition(pos)) throw new BoardException("Posição inválida!");
        }
    }
}