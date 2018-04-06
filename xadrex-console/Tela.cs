using System;
using xadrez;
using tabuleiro;

namespace xadrex_console {
    class Tela {

        public static void imprimirTabuleiro(Tabuleiro tab) {
            for (int i = 0; i < tab.linhas; i++) {
                //coloca a numeração decrescente no início das linhas
                Console.Write(8 - i + " ");

                for (int j = 0; j < tab.colunas; j++) {
                    imprimirPeca(tab.peca(i, j));                  
                }
                Console.WriteLine();
            }
            //coloca as letras no fim das colunas
            Console.WriteLine("  a b c d e f g h");
        }

        //sobrecarga do método imprimirTabuleiro para mostrar as posições possíveis da peça
        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis) {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkBlue;


            for (int i = 0; i < tab.linhas; i++) {
                //coloca a numeração decrescente no início das linhas
                Console.Write(8 - i + " ");

                for (int j = 0; j < tab.colunas; j++) {
                    if(posicoesPossiveis[i,j] == true) {
                        Console.BackgroundColor = fundoAlterado;
                    } else {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    imprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            //coloca as letras no fim das colunas
            Console.WriteLine("  a b c d e f g h");

            //garantindo que o fundo volte a cor original
            Console.BackgroundColor = fundoOriginal;
        }

        public static void imprimirPeca(Peca peca) {

            if (peca == null) {
                Console.Write("- ");
            }
            else {

                //se a cor da peça for branca, apenas imprime a peça que veio de argumento
                if (peca.cor == Cor.Branca) {
                    Console.Write(peca);
                }
                else {
                    /* a variável aux guarda a cor original das peças do tabuleiro
                     * realizamos a mudança da cor e imprimimos a peça (ela sairá na cor desejada)
                     * depois voltamos para a cor original do tabuleiro (Console.ForegroundColor = aux)
                     * assim só as peças pretas receberão a nova cor
                     */
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

        /* método que recebe a posição da peça que o usuário deseja movimentar
         * exemplo: c2
         */
        public static PosicaoXadrez lerPosicaoXadrez() {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
    }
}
