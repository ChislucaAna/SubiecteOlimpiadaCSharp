using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judeteana2022
{
    public class Masurare
    {
        public int IdMasurare;
        public int IdHarta;
        public int PozitieX;
        public int PozitieY;
        public double ValoareMasurare;
        public DateTime DataMasurare;

        public Masurare(int IdMasurare, int IdHarta, int PozitieX, int PozitieY, double ValoareMasurare, DateTime DataMasurare)
        {
            this.DataMasurare = DataMasurare;
            this.IdMasurare = IdMasurare;
            this.IdHarta= IdHarta;
            this.PozitieX= PozitieX;
            this.PozitieY = PozitieY;
            this.ValoareMasurare= ValoareMasurare;  
        }

        public override string ToString()
        {
            return IdMasurare+"#"+IdHarta+"#"+PozitieX+"#"+PozitieY+"#"+ValoareMasurare+"#"+DataMasurare.ToString(); 
        }
    }
}
