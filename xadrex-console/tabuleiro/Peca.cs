namespace tabuleiro {
    class Peca {

        public Posicao posicao { get; set; }
        //atributos que só serão acessados pela própria classe e suas subclasses
        public Cor cor { get; protected set; }
        public int qteMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        //Ao criar uma peça, ela ainda não possui posição, por isso NULL. Quem determina sua posição é o tabuleiro
        public Peca(Tabuleiro tabuleiro, Cor cor) {
            this.posicao = null;
            this.tab = tabuleiro;
            this.cor = cor;
            this.qteMovimentos = 0;
        }

        public void incrimentarQtdeMovimentos() {
            qteMovimentos++;
        }

    }
}
