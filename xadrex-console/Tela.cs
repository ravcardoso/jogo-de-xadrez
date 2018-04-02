using System;
using System.Collections.Generic;
using System.Linq;
using xadrez;
using tabuleiro;

namespace xadrex_console {
    class Tela {

        public static void imprimirTabuleiro(Tabuleiro tab) {

            for (int i = 0; i < tab.linhas; i++) {
                //coloca a numeração decrescente no início das linhas
                Console.Write(8 - i + " ");

                for (int j = 0; j < tab.colunas; j++) {

                    if (tab.peca(i,j) == null) {
                        Console.Write("- ");
                    } else {
                        imprimirPeca(tab.peca(i, j));
                        Console.Write(" ");
                    }
                    
                }
                Console.WriteLine();
                
            }
            //coloca as letras no fim das colunas
            Console.WriteLine("  a b c d e f g h");
        }

        public static void imprimirPeca(Peca peca) {
            //se a cor da peça for branca, apenas imprime a peça que veio de argumento
            if(peca.cor == Cor.Branca) {
                Console.Write(peca);
            } else {
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
