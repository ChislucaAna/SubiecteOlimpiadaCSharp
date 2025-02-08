using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judeteana2019
{
    public class Carte
    {
        public int id_carte;
        public string titlu;
        public string autor;
        public string gen;

        public Carte(int id_carte, string titlu, string autor, string gen)
        {
            this.id_carte = id_carte;
            this.titlu = titlu;
            this.autor = autor;
            this.gen = gen;
        }

        public override string ToString()
        {
            return titlu + "*" + autor + "*" + gen;
        }
    }
}
