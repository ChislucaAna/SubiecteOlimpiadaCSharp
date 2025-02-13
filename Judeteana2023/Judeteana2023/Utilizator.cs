using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judeteana2023
{
    public class Utilizator
    {
        public string EmailUtilizator;
        public string NumeUtilizator;
        public string Parola;

        public Utilizator(string emailUtilizator, string numeUtilizator, string parola)
        {
            EmailUtilizator = emailUtilizator;
            NumeUtilizator = numeUtilizator;
            Parola = parola;
        }

        public override string ToString()
        {
            return EmailUtilizator+";"+NumeUtilizator+";"+Parola;   
        }
    }
}
