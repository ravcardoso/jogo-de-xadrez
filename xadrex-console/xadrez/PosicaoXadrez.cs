using tabuleiro;

namespace xadrez {
    class PosicaoXadrez {

        public char coluna { get; set; }
        public int linha { get; set; }

        public PosicaoXadrez(char coluna, int linha) {
            this.coluna = coluna;
            this.linha = linha;
        }

        //método para converter a posição do xadrez real para a posição interna da matriz
        //A8 == (0,0) ; B8 == (0,1) ...
        public Posicao toPosicao() {
            return new Posicao(8 - linha, coluna - 'a');
            /* (coluna - 'a') funciona porque internamente o caracter A é um inteiro
             * então quando for (a - a) o resultado é 0
             * (b - a) o resultado é 1, porque o 'b' vem após o 'a'
             * e assim sucessivamente...
             */
        }

        public override string ToString() {
            return "" + coluna + linha;
        }
    }
}
