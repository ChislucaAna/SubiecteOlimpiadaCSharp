using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judeteana2016
{
    public class Comanda
    {
        public string id_comanda;
        public int id_client;
        public DateTime data_comanda;

        public Comanda(string id_comanda, int id_client, DateTime data_comanda)
        {
            this.id_comanda = id_comanda;
            this.id_client = id_client;
            this.data_comanda = data_comanda;
        }
    }
}
