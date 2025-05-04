using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nationala2013
{
    public class Scor
    {
        public int ID;
        public int Timp;
        public int NrMutari;
        public int NrPiese;


        public Scor(int iD, int timp, int nrMutari, int nrPiese)
        {
            ID = iD;
            Timp = timp;
            NrMutari = nrMutari;
            NrPiese = nrPiese;
        }

        public override string ToString()
        {
            return ID + ";" + Timp+ ";" + NrMutari + ";" + NrPiese;       
        }
    }
}
