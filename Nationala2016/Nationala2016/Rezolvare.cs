using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nationala2016
{
    public class Rezolvare
    {
        public int IdRebus;
        public int ColoanaStart;
        public int LiniaStart;
        public string Orientare;
        public string Solutie;
        public string TextDefinitie;

        public Rezolvare(int idRebus, int coloanaStart, int liniaStart, string orientare, string solutie, string textDefinitie)
        {
            IdRebus = idRebus;
            ColoanaStart = coloanaStart;
            LiniaStart = liniaStart;
            Orientare = orientare;
            Solutie = solutie;
            TextDefinitie = textDefinitie;
        }
    }
}
