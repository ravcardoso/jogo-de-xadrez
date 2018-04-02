namespace tabuleiro {
    class Tabuleiro {

        public int linhas { get; set; }
        public int colunas { get; set; }
        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas) {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Peca[linhas, colunas]; 
        }

        //método para dar acesso à posição de uma peça
        public Peca peca(int linha, int coluna) {
            return pecas[linha, coluna];
        }

        //método para colocar as peças no tabuleiro
        public void colocarPeca(Peca p, Posicao pos) {

            //coloca a peça 'p' na matriz 'pecas' na posição '[pos.linha, pos.coluna]'
            pecas[pos.linha, pos.coluna] = p;
            p.posicao = pos;

        }
    }
}
