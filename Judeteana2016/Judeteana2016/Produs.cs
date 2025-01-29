using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judeteana2016
{
    public class Produs
    {
        public int id_produs;
        public string denumire_produs;
        public string descriere;
        public int pret;
        public int kcal;
        public int felul;

        public Produs(int id_produs, string denumire_produs, string descriere, int pret, int kcal, int felul)
        {
            this.id_produs = id_produs;
            this.denumire_produs = denumire_produs;
            this.descriere = descriere;
            this.pret = pret;
            this.kcal = kcal;
            this.felul = felul;
        }
    }
}
