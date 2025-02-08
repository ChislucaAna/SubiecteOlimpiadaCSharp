using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judeteana2019
{
    public class Imprumut
    {
        public int id_imprumut;
        public int id_carte;
        public string email;
        public DateTime data_imprumut;

        public Imprumut(int id_imprumut, int id_carte, string email, DateTime data_imprumut)
        {
            this.id_imprumut = id_imprumut;
            this.id_carte = id_carte;
            this.email = email;
            this.data_imprumut = data_imprumut;
        }

        public override string ToString()
        {
            var queryResults = from c in Db.carti
                               where c.id_carte == id_carte
                               select c.titlu;
            //tu in fisier ai titlu nu id
            return queryResults.First() + "*" + email + "*" + data_imprumut;
        }
    }
}
