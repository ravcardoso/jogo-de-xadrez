using tabuleiro;

namespace xadrez {
    class Rei : Peca {

        private PartidaDeXadrez partida;

        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor) {
            /* Este construtor recebe um Tabuleiro tab e uma Cor cor
             * e repassa essas instrução para o construtor da superclasse Peca
             */
            this.partida = partida;
        }

        public override string ToString() {
            return "R";
        }

        //verifica se a posição desejada está vazia ou se está com peça adversária
        private bool podeMover(Posicao pos) {
            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor;
        }

        //método teste se a torre é elegível para roque
        private bool testeTorreParaRoque(Posicao pos) {
            Peca p = tab.peca(pos);
            return p != null && p is Torre && p.cor == cor && p.qteMovimentos == 0;
        }

        //método sobrescrevendo o da superclasse Peca
        public override bool[,] movimentosPossiveis() {
            /* REGRA:
             * O rei pode se mover apenas 1 casa em todas as direções
             */

            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            //Posição acima da posição do Rei
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //Posição nordeste à posição do Rei
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //Posição direita à posição do Rei
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //Posição sudeste à posição do Rei
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //Posição abaixo da posição do Rei
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //Posição sudoeste da posição do Rei
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //Posição esquerda da posição do Rei
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //Posição noroeste da posição do Rei
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //---------Jogada Especial---------
            if (qteMovimentos == 0 && !partida.xeque) {
                
                //----------Roque pequeno----------
                Posicao posTorre1 = new Posicao(posicao.linha, posicao.coluna + 3);

                if (testeTorreParaRoque(posTorre1)) {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);

                    if(tab.peca(p1) == null && tab.peca(p2) == null) {
                        mat[posicao.linha, posicao.coluna + 2] = true;
                    }
                }

                //----------Roque grande-----------
                Posicao posTorre2 = new Posicao(posicao.linha, posicao.coluna - 4);

                if (testeTorreParaRoque(posTorre2)) {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);

                    if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null) {
                        mat[posicao.linha, posicao.coluna - 2] = true;
                    }
                }
            }

            return mat;
        }
    }
}
