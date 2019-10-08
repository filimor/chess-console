﻿using System;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        private int _turno;
        private Cor _jogadorAtual;

        public Tabuleiro Tab { get; }
        public bool Terminada { get; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            _turno = 1;
            _jogadorAtual = Cor.Branca;
            Terminada = false;
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
        }

        private void ColocarPecas()
        {
            Tab.ColocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('c', 1).toPosicao());
            Tab.ColocarPeca(new Rei(Tab, Cor.Branca), new PosicaoXadrez('e', 2).toPosicao());
        }
    }
}
