using System.Collections.Generic;
using tabuleiro;

namespace xadrez {
    class PartidaDeXadrez {

        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        public bool xeque { get; private set; }
        public Peca vulneravelEmPassant { get; private set; }
        //Controle das peças capturadas -> conjunto de peças da partida e das peças capturadas
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;


        public PartidaDeXadrez() {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            vulneravelEmPassant = null;
            //instaciação dos conjuntos deve ocorrer antes da colocação das peças
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executarMovimento(Posicao origem, Posicao destino) {
            Peca p = tab.retirarPeca(origem);
            p.incrimentarQtdeMovimentos();

            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);

            if (pecaCapturada != null) {
                capturadas.Add(pecaCapturada);
            }

            //---------Jogada Especial---------
            //-----Execução Roque pequeno------
            if (p is Rei && destino.coluna == origem.coluna + 2) {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna + 1);

                Peca T = tab.retirarPeca(origemTorre);
                T.incrimentarQtdeMovimentos();
                tab.colocarPeca(T, destinoTorre);
            }

            //---------Jogada Especial---------
            //-----Execução Roque grande-------
            if (p is Rei && destino.coluna == origem.coluna - 2) {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna - 1);

                Peca T = tab.retirarPeca(origemTorre);
                T.incrimentarQtdeMovimentos();
                tab.colocarPeca(T, destinoTorre);
            }

            //---------Jogada Especial---------
            //-----------Em Passant------------
            if(p is Peao) {
                if(origem.coluna != destino.coluna && pecaCapturada == null) {
                    Posicao posPeao;

                    if(p.cor == Cor.Branca) {
                        posPeao = new Posicao(destino.linha + 1, destino.coluna);
                    } else {
                        posPeao = new Posicao(destino.linha - 1, destino.coluna);
                    }

                    pecaCapturada = tab.retirarPeca(posPeao);
                    capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }
        
        //conta o turno da partida
        /* REGRA:
         * o usuário não pode mover uma peça de modo que seu rei fique em xeque
         */
        public void realizaJogada(Posicao origem, Posicao destino) {
            Peca pecaCapturada = executarMovimento(origem, destino);

            if (estaEmXeque(jogadorAtual)) {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (estaEmXeque(adversaria(jogadorAtual))) {
                xeque = true;
            } else {
                xeque = false;
            }

            if (testeXequeMate(adversaria(jogadorAtual))) {
                terminada = true;
            } else {
                turno++;
                mudarJogador();
            }

            //---------Jogada Especial---------
            //-----------Em Passant------------
            Peca p = tab.peca(destino);

            if (p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2)) {
                vulneravelEmPassant = p;
            } else {
                vulneravelEmPassant = null;  
            }
        }

        //método que desfaz o movimento realizado, caso ele tenha colocado o rei em xeque
        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQtdeMovimentos();

            if (pecaCapturada != null) {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);

            //---------Jogada Especial---------
            //-----Execução Roque Pequeno------
            if (p is Rei && destino.coluna == origem.coluna + 2) {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna + 1);

                Peca T = tab.retirarPeca(destinoTorre);
                T.decrementarQtdeMovimentos();
                tab.colocarPeca(T, origemTorre);
            }

            //---------Jogada Especial---------
            //-----Execução Roque Grande-------
            if (p is Rei && destino.coluna == origem.coluna - 2) {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna - 1);

                Peca T = tab.retirarPeca(destinoTorre);
                T.decrementarQtdeMovimentos();
                tab.colocarPeca(T, origemTorre);
            }

            //---------Jogada Especial---------
            //-----------Em Passant------------
            if(p is Peao) {
                if(origem.coluna != destino.coluna && pecaCapturada == vulneravelEmPassant) {
                    Peca peao = tab.retirarPeca(destino);
                    Posicao posPeao;

                    if(p.cor == Cor.Branca) {
                        posPeao = new Posicao(3, destino.coluna);
                    } else {
                        posPeao = new Posicao(4, destino.coluna);
                    }

                    tab.colocarPeca(peao, posPeao);
                }
            }

        }

        //muda a vez do jogador
        private void mudarJogador() {
            if(jogadorAtual == Cor.Branca) {
                jogadorAtual = Cor.Preta;
            } else {
                jogadorAtual = Cor.Branca;
            }
        }

        //método retorna um conjunto de peças capturadas de uma determinada cor
        public HashSet<Peca> pecasCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        //método retorna as peças em jogo de uma determinada cor
        public HashSet<Peca> pecasEmJogo(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            //retira todas as peças capturadas dessa cor
            aux.ExceptWith(pecasCapturadas(cor));

            return aux;
        }

        //método para devolver o rei de uma determinada cor
        private Peca rei(Cor cor) {
            foreach (Peca x in pecasEmJogo(cor)) {
                //se a peça x é uma instância da classe Rei, retorna x
                if(x is Rei) {
                    return x;
                }
            }
            return null;
        }

        /* método que testa todos os movimentos possíveis de todas as peças
         * Quando o rei está em xeque?
         * quando pelo menos uma peça adversária possui um movimento possível
         * para a casa desse rei.
         */
        public bool estaEmXeque(Cor cor) {
            Peca R = rei(cor);

            //tratamento de erro só pra garantir. Se o jogo estiver correto, ela não deve acontecer
            if(R == null) {
                throw new TabuleiroException("Não tem rei da cor " + cor + "no tabuleiro!");
            }

            /* Para cada peça x no conjunto das peças em jogo da cor adversária
             * preenche a matriz bool com seus movimentos possíveis
             * se mat[] tiver um movimento para posição (linha, coluna) do rei, retorna true
             * O rei está em xeque!
             */
            foreach(Peca x in pecasEmJogo(adversaria(cor))) {
                bool[,] mat = x.movimentosPossiveis();

                //if (mat[R.posicao.linha, R.posicao.coluna == true]) => dá no mesmo
                if (mat[R.posicao.linha, R.posicao.coluna]) {
                    return true;
                }
            }
            return false;
        }

        /* REGRA:
         * Xeque Mate => é quando um rei está em xeque e não existe nenhum movimento
         * possível que tire ele do xeque
         */
        public bool testeXequeMate (Cor cor) {
            if (!estaEmXeque(cor)) {
                return false;
            }

            foreach (Peca x in pecasEmJogo(cor)) {
                bool[,] mat = x.movimentosPossiveis();

                for(int i = 0; i < tab.linhas; i++) {
                    for (int j = 0; j < tab.colunas; j++) {
                        if (mat[i, j]) {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);

                            Peca pecaCapturada = executarMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);

                            if (!testeXeque) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        //método pra identificar a cor da peça adversária
        private Cor adversaria(Cor cor) {
            if(cor == Cor.Branca) {
                return Cor.Preta;
            } else {
                return Cor.Branca;
            }
        }

        public void validarPosiçãoDeOrigem(Posicao pos) {
            if(tab.peca(pos) == null) {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if(jogadorAtual != tab.peca(pos).cor) {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis()) {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino) {
            //se a peça não pode mover para posição de destino, lança a exceção
            if (!tab.peca(origem).movimentoPossivel(destino)) {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        //colocar uma peça em posição de xadrez (ex.: c1)
        public void colocarNovaPeca(char coluna, int linha, Peca peca) {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas() {

            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));
        }
    }
}
