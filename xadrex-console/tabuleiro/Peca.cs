﻿namespace tabuleiro {
    class Peca {

        public Posicao posicao { get; set; }
        //atributos que só serão acessados pela própria classe e suas subclasses
        public Cor cor { get; protected set; }
        public int qteMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Peca(Posicao posicao, Tabuleiro tabuleiro, Cor cor) {
            this.posicao = posicao;
            this.tab = tabuleiro;
            this.cor = cor;
            this.qteMovimentos = 0;
        }

       
    }
}
