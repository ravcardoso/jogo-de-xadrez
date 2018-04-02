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

    }
}
