using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judeteana2016
{
    public class Subcomanda
    {
        public int id_subcomanda;
        public string id_comanda;
        public int id_produs;
        public int cantitate;

        public Subcomanda(int id_subcomanda, string id_comanda, int id_produs, int cantitate)
        {
            this.id_subcomanda = id_subcomanda;
            this.id_comanda = id_comanda;
            this.id_produs = id_produs;
            this.cantitate = cantitate;
        }   
    }
}
