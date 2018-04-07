namespace tabuleiro {
    abstract class Peca {

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

        public void decrementarQtdeMovimentos() {
            qteMovimentos--;
        }

        //verifica na matriz de movimentos possíveis, se existe pelo menos 1 valor verdadeiro
        public bool existeMovimentosPossiveis() {
            bool[,] mat = movimentosPossiveis();

            for (int i = 0; i < tab.linhas; i++) {
                for (int j = 0; j < tab.colunas; j++) {
                    if (mat[i, j] == true) {
                        return true;
                    }
                }
            }

            return false;
        }

        //valida se uma peça pode mover para uma dada posição
        public bool movimentoPossivel(Posicao pos) {
            return movimentosPossiveis()[pos.linha, pos.coluna];
        }
        
        //método abstrato para implementação obrigatória nas classes filhas
        public abstract bool[,] movimentosPossiveis();
    }
}
