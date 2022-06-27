using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulowaneWyżarzanie
{
    class Zadanie
    {
        private int czas_p1;
        private int czas_p2;
        private int fx;
        public int Czas_p1
        {
            get => czas_p1;
        }
        public int Czas_p2
        {
            get => czas_p2;
        }
        public int Fx
        {
            get => fx;
            set => fx = value;
        }
        public Zadanie(int czas_p1,int czas_p2)
        {
            this.czas_p1 = czas_p1;
            this.czas_p2 = czas_p2;
            this.fx = 0;
        }
    }
}
