﻿using System;
using System.Collections.Generic;
using xadrez;
using tabuleiro;

namespace xadrex_console {
    class Tela {

        public static void imprimirPartida(PartidaDeXadrez partida) {
            imprimirTabuleiro(partida.tab);
            Console.WriteLine();
            imprimirPecasCapturadas(partida);

            Console.WriteLine();
            Console.WriteLine("________________________");
            Console.WriteLine("Turno: " + partida.turno);

            if (!partida.terminada) {
                Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);

                if (partida.xeque) {
                    Console.WriteLine();
                    Console.WriteLine("CUIDADO! Seu rei está em xeque");
                }
            } else {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine();
                Console.WriteLine("Vencedor: " + partida.jogadorAtual);
            }
        }

        public static void imprimirPecasCapturadas(PartidaDeXadrez partida) {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branca));

            Console.WriteLine();
            Console.Write("Pretas: ");

            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            imprimirConjunto(partida.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
        }

        public static void imprimirConjunto(HashSet<Peca> conjunto) {
            Console.Write("[");
            foreach (Peca x in conjunto) {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

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
