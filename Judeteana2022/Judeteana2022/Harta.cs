using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judeteana2022
{
    public class Harta
    {
        public int IdHarta;
        public string NumeHarta;
        public string FisierHarta;

        public Harta(int IdHarta, string NumeHarta, string FisierHarta)
        {
            this.IdHarta = IdHarta; 
            this.FisierHarta = FisierHarta;
            this.NumeHarta = NumeHarta;
        }
    }
}
