using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nationala2024_peClase
{
    public class Utilizator
    {
        public string Email;
        public string Nume;
        public string Prenume;
        public string Parola;
        public DateTime DataNastere;

        public Utilizator(string email, string nume, string prenume, string parola, DateTime dataNastere)
        {
            Email = email;
            Nume = nume;
            Prenume = prenume;
            Parola = parola;
            DataNastere = dataNastere;
        }

        public override string ToString()
        {
            string formattedDate = DataNastere.ToString("MM/dd/yyyy");
            return Email + ";" + Nume + ";" + Prenume + ";" + Parola + ";" + formattedDate;  
        }
    }
}
