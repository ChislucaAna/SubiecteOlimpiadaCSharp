using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judeteana2022
{
    public class Utilizator
    {
        public int IdUtilizator;
        public string NumeUtilizator;
        public string Parola;
        public string EmailUtilizator;
        public DateTime UltimaUtilizare;

        public Utilizator(int idUtilizator, string numeUtilizator, string parola, string emailUtilizator, DateTime ultimaUtilizare)
        {
            IdUtilizator = idUtilizator;
            NumeUtilizator = numeUtilizator;
            Parola = parola;
            EmailUtilizator = emailUtilizator;
            UltimaUtilizare = ultimaUtilizare;
        }   
    }
}
