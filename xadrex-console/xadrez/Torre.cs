using tabuleiro;

namespace xadrez {
    class Torre : Peca {

        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor) {
            /* Este construtor recebe um Tabuleiro tab e uma Cor cor
             * e repassa essas instrução para o construtor da superclasse Peca
             */
        }

        public override string ToString() {
            return "T";
        }

        //verifica se a posição desejada está vazia ou se está com peça adversária
        private bool podeMover(Posicao pos) {
            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor;
        }

        //método sobrescrevendo o da superclasse Peca
        public override bool[,] movimentosPossiveis() {
            /* REGRA:
             * A torre pode se mover na horizontal e na vertical até encontrar uma peça ou acabar o tabuleiro
             */

            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            //Posição acima da posição da Torre
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            while(tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                
                //forçar a parada do while quando encontrar uma peça adversária
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }

                //continua verificando nas linhas acima da posição atual da torre
                pos.linha = pos.linha - 1;
            }

            //Posição abaixo da posição da Torre
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;

                //forçar a parada do while quando encontrar uma peça adversária
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }

                //continua verificando nas linhas acima da posição atual da torre
                pos.linha = pos.linha + 1;
            }

            //Posição direita da posição da Torre
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;

                //forçar a parada do while quando encontrar uma peça adversária
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }

                //continua verificando nas linhas acima da posição atual da torre
                pos.coluna = pos.coluna + 1;
            }

            //Posição esquerda da posição da Torre
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;

                //forçar a parada do while quando encontrar uma peça adversária
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }

                //continua verificando nas linhas acima da posição atual da torre
                pos.coluna = pos.coluna - 1;
            }

            return mat;
        }

        
    }
}
