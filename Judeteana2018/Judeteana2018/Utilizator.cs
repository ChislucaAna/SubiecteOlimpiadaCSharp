using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judeteana2018
{
    public class Utilizator
    {

        public int IdUtilizator;
        public string NumePrenumeUtilizator;
        public string ParolaUtilizator;
        public string EmailUtilizator;
        public string ClasaUtilizator;

        public Utilizator(int idUtilizator, string numePrenumeUtilizator, string parolaUtilizator, string emailUtilizator, string clasaUtilizator)
        {
            IdUtilizator = idUtilizator;
            NumePrenumeUtilizator = numePrenumeUtilizator;
            ParolaUtilizator = parolaUtilizator;
            EmailUtilizator = emailUtilizator;
            ClasaUtilizator = clasaUtilizator;
        }
    }
}
