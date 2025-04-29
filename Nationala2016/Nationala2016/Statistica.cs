using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nationala2016
{
    public class Statistica
    {
        public int IdUtilizator { get; set; }
        public int IdRebus { get; set; }
        public int TimpRezolvare { get; set; }
        public int NrLitereGresite { get; set; }
        public int StareRebus { get; set; }

        public Statistica(int idUtilizator, int idRebus, int timpRezolvare, int nrLitereGresite, int stareRebus)
        {
            IdUtilizator = idUtilizator;
            IdRebus = idRebus;
            TimpRezolvare = timpRezolvare;
            NrLitereGresite = nrLitereGresite;
            StareRebus = stareRebus;
        }
    }
}
