using tabuleiro;

namespace xadrez {
    class Peao : Peca {

        private PartidaDeXadrez partida;

        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor) {
            this.partida = partida;
        }

        public override string ToString() {
            return "P";
        }

        private bool existeInimigo(Posicao pos) {
            Peca p = tab.peca(pos);
            return p != null && p.cor != cor;
        }

        private bool livre(Posicao pos) {
            return tab.peca(pos) == null;
        }

        //método sobrescrevendo o da superclasse Peca
        public override bool[,] movimentosPossiveis() {
            /* REGRA:
             * O peão tem uma lógica uma pouco diferente das demais peças
             * Se o peão é branco, ele só pode se mover pra cima
             * Se o peão é preto, ele só pode se mover pra baixo
             */

            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            if(cor == Cor.Branca) {

                //o peão pode andar duas casas para cima, caso seja seu primeiro movimento
                pos.definirValores(posicao.linha - 2, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0) {
                    mat[pos.linha, pos.coluna] = true;
                }

                //a partir do segundo movimento, ele só pode andar uma casa
                pos.definirValores(posicao.linha - 1, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos)){
                    mat[pos.linha, pos.coluna] = true;
                }

                //ele pode andar uma casa nas diagonais, caso possua uma peça adversária
                pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                //---------Jogada Especial---------
                //-----------Em Passant------------
                if(posicao.linha == 3) {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEmPassant) {
                        mat[esquerda.linha - 1, esquerda.coluna] = true;
                    }

                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEmPassant) {
                        mat[direita.linha - 1, direita.coluna] = true;
                    }
                }

            }
            else {
                //primeiro movimento
                pos.definirValores(posicao.linha + 2, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0) {
                    mat[pos.linha, pos.coluna] = true;
                }

                //a partir do segundo movimento
                pos.definirValores(posicao.linha + 1, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                //ele pode andar uma casa nas diagonais, caso possua uma peça adversária
                pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                //---------Jogada Especial---------
                //-----------Em Passant------------
                if (posicao.linha == 4) {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEmPassant) {
                        mat[esquerda.linha + 1, esquerda.coluna] = true;
                    }

                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEmPassant) {
                        mat[direita.linha + 1, direita.coluna] = true;
                    }
                }
            }

            return mat;
        }
    }
}