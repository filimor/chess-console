namespace tabuleiro
{
    internal abstract class Peca
    {
        public Posicao Posicao { get; set; }

        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        protected Peca(Tabuleiro tab, Cor cor)
        {
            Tab = tab;
            Cor = cor;
        }

        public void IncrementarMovimentos()
        {
            QtdMovimentos++;
        }

        public void DecrementarMovimentos()
        {
            QtdMovimentos--;
        }

        public bool ExistemMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPossiveis();
            for (int i = 0; i < Tab.Linhas; i++)
            {
                for (int j = 0; j < Tab.Colunas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool MovimentoPossivel(Posicao pos)
        {
            Tab.ValidarPosicao(pos);
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}