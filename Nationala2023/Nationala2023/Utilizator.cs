using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nationala2023
{
    public class Utilizator
    {
        public string email;
        public string nume;
        public string parola;

        public Utilizator(string email, string nume, string parola)
        {
            this.email = email;
            this.nume = nume;
            this.parola = parola;
        }
    }
}
