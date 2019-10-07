namespace tabuleiro
{
    class Posicao
    {
        // representa a posição de uma Peça no tabuleiro

        public int Linha { get; set; }
        public int Coluna { get; set; }

        public Posicao(int linha, int coluna)
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
