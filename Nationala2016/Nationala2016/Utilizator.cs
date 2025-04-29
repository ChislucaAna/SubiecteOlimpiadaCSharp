using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nationala2016
{
    public class Utilizator
    {
        public int IdUtilizator;
        public string Parola;
        public string NumeUtilizator;
        public string Email;
        public int TipUtilizator;

        public Utilizator(int idUtilizator, string parola, string numeUtilizator, string email, int tipUtilizator)
        {
            IdUtilizator = idUtilizator;
            Parola = parola;
            NumeUtilizator = numeUtilizator;
            Email = email;
            TipUtilizator = tipUtilizator;
        }   
    }
}
