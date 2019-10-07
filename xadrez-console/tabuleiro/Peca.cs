namespace tabuleiro
{
    class Peca
    {
        // protected set:
        // só pode ser acessada por ela e pelas subclasses
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            Tab = tab;
            Cor = cor;
            QtdMovimentos = 0;
        }
    }
}
