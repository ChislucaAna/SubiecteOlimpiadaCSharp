using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judeteana2023
{
    public class Item
    {
        public int idItem;
        public string EnuntItem;
        public string Raspuns1;
        public string Raspuns2;
        public string Raspuns3;
        public string RaspunsCorect;
        public int PunctajItem;

        public Item(int idItem, string enuntItem, string raspuns1, string raspuns2, string raspuns3, string raspunsCorect, int punctajItem)
        {
            this.idItem = idItem;
            EnuntItem = enuntItem;
            Raspuns1 = raspuns1;
            Raspuns2 = raspuns2;
            Raspuns3 = raspuns3;
            RaspunsCorect = raspunsCorect;
            PunctajItem = punctajItem;
        }

        public override string ToString()
        {
            return idItem+";"+EnuntItem+";"+Raspuns1+";"+Raspuns2+";"+Raspuns3+";"+RaspunsCorect+";"+PunctajItem;   
        }
    }
}
