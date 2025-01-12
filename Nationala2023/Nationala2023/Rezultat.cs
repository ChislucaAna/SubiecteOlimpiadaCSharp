using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nationala2023
{
    public class Rezultat
    {

        public int TipJoc;
        public string EmailUtilizator;
        public int Punctajjoc;
        public DateTime data;

        public Rezultat(int tipJoc, string emailUtilizator, int punctajjoc, DateTime data)
        {
            TipJoc = tipJoc;
            EmailUtilizator = emailUtilizator;
            Punctajjoc = punctajjoc;
            this.data = data;
        }
    }
}
