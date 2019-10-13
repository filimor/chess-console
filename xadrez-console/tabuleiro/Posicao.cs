namespace tabuleiro
{
    internal class Posicao
    {
        // representa a posição de uma Peça no tabuleiro

        public int Linha { get; set; }
        public int Coluna { get; set; }

        public Posicao()
        {
            Linha = 0;
            Coluna = 0;
        }

        public Posicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public void DefinirValores(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public override string ToString()
        {
            return $"{Linha}, {Coluna}";
        }
    }
}