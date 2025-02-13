using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judeteana2023
{
    public class Rezultat
    {
        public int idRezultat;
        public int TipJoc;
        public string EmailUtilizator;
        public int PunctajJoc;

        public Rezultat(int idRezultat, int tipJoc, string emailUtilizator, int punctajJoc)
        {
            this.idRezultat = idRezultat;
            TipJoc = tipJoc;
            EmailUtilizator = emailUtilizator;
            PunctajJoc = punctajJoc;
        }

        public override string ToString()
        {
            return idRezultat + ";" + TipJoc + ";" + EmailUtilizator + ";" + PunctajJoc;
        }
    }
}
