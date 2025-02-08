using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Judeteana2019
{
    public class Utilizator
    {
        public string email;
        public string parola;
        public string nume;
        public string prenume;

        public Utilizator(string email, string parola, string nume, string prenume)
        {
            this.email = email;
            this.parola = parola;
            this.nume = nume;
            this.prenume = prenume;
        }

        public override string ToString()
        {
            return email + "*" + parola + "*" + nume + "*" + prenume;
        }
    }
}
