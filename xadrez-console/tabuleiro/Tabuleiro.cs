namespace tabuleiro
{
    class Tabuleiro
    {
        private readonly Peca[,] _pecas;

        public int Linhas { get; set; }
        public int Colunas { get; set; }

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            _pecas = new Peca[linhas, colunas];
        }

        public Peca Peca(int linha, int coluna)
        {
            return _pecas[linha, coluna];
        }

        public void ColocarPeca(Peca p, Posicao pos)
        {
            _pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;
        }
    }
}
