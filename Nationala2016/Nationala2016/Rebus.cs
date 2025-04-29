using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nationala2016
{
    public class Rebus
    {
        public int IdRebus;
        public string DenumireRebus;
        public int NrColoane;
        public int NrLinii;
        public int TimpEstimat;

        public Rebus(int idRebus, string denumireRebus, int nrColoane, int nrLinii, int timpEstimat)
        {
            IdRebus = idRebus;
            DenumireRebus = denumireRebus;
            NrColoane = nrColoane;
            NrLinii = nrLinii;
            TimpEstimat = timpEstimat;
        }
    }
}
