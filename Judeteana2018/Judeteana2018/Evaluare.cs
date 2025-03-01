using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judeteana2018
{
    public class Evaluare
    {

        public int IdEvaluare;
        public int IdElev;
        public DateTime DataEvaluare;
        public int NotaEvaluare;

        public Evaluare(int idEvaluare, int idElev, DateTime dataEvaluare, int notaEvaluare)
        {
            IdEvaluare = idEvaluare;
            IdElev = idElev;
            DataEvaluare = dataEvaluare;
            NotaEvaluare = notaEvaluare;
        }   
    }
}
