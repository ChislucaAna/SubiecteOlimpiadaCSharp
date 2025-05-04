using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nationala2013
{
    public class Utilizator
    {
        public int ID;
        public string Nume;
        public string Prenume;
        public string Nickname;
        public string Parola;
        public int TipUtilizator;


        public Utilizator(int iD, string nume, string prenume, string nickname, string parola, int tipUtilizator)
        {
            ID = iD;
            Nume = nume;
            Prenume = prenume;
            Nickname = nickname;
            Parola = parola;
            TipUtilizator = tipUtilizator;
        }

        public override string ToString()
        {
            return ID + ";"+ Nume + ";" + Prenume + ";" + Nickname + ";" + Parola + ";" + TipUtilizator;
        }
    }
}
