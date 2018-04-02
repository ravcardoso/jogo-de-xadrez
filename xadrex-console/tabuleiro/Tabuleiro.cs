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

        //sobrecarga do método peca
        public Peca peca(Posicao pos) {
            return pecas[pos.linha, pos.coluna];
        }
        
        //método para colocar as peças no tabuleiro
        public void colocarPeca(Peca p, Posicao pos) {
            if (existePeca(pos)) {
                throw new TabuleiroException("Já existe uma peça nessa posição");
            }

            //coloca a peça 'p' na matriz 'pecas' na posição '[pos.linha, pos.coluna]'
            pecas[pos.linha, pos.coluna] = p;
            p.posicao = pos;
        }

        //método para testar se uma posição é válida
        public bool posicaoValida(Posicao pos) {
            if (pos.linha < 0 || pos.linha >= linhas || pos.coluna < 0 || pos.coluna >= colunas) {
                return false;
            }
            return true;
        }

        //método que recebe uma posição e caso ela não seja válida, ele retorna uma exceção personalizada 
        public void validarPosicao(Posicao pos) {
            if (!posicaoValida(pos)) {
                throw new TabuleiroException("Posição inválida!");
            }
        }
        
        //método para testar se existe uma peça em uma determinada posição
        public bool existePeca(Posicao pos) {
            //verifica se a posição é válida
            validarPosicao(pos);

            //se o retorno é diferente de null, existe uma peça naquela posição
            return peca(pos) != null;
        }

    }
}
