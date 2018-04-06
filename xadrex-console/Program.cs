using System;
using System.Collections.Generic;
using System.Linq;
using xadrez;
using tabuleiro;

namespace xadrex_console {
    class Program {
        static void Main(string[] args) {

            try {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.terminada) {
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.tab);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    //leio a posição do xadrez (a8) e converto pra posição de matriz (0,0)
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();

                    /* ao informar a peça que queremos mover, a tela será limpa e
                     * reexibida com as posições possíveis marcadas
                     */
                    bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);

                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

                    partida.executarMovimento(origem, destino);
                }
                Tela.imprimirTabuleiro(partida.tab);
            }
            catch (TabuleiroException e){
                Console.WriteLine(e.Message);
            }
            

            Console.ReadLine();
        }
    }
}