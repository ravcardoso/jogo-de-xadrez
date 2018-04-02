using tabuleiro;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
