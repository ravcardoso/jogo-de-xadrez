using System;
using System.Collections.Generic;
using System.Linq;
using xadrez;
using tabuleiro;

namespace xadrex_console {
    class Program {
        static void Main(string[] args) {

            PosicaoXadrez pos = new PosicaoXadrez('c', 7);

            Console.WriteLine(pos);

            Console.WriteLine(pos.toPosicao());

            Console.ReadLine();
        }
    }
}
