using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judeteana2018
{
    public class Item
    {
        public int IdItem;
        public int TipItem;
        public string EnuntItem;
        public string Raspuns1Item;
        public string Raspuns2Item;
        public string Raspuns3Item;
        public string Raspuns4Item;
        public string RaspunsCorectItem;

        public Item(int idItem, int tipItem, string enuntItem, string raspuns1Item, string raspuns2Item, string raspuns3Item, string raspuns4Item, string raspunsCorectItem)
        {
            IdItem = idItem;
            TipItem = tipItem;
            EnuntItem = enuntItem;
            Raspuns1Item = raspuns1Item;
            Raspuns2Item = raspuns2Item;
            Raspuns3Item = raspuns3Item;
            Raspuns4Item = raspuns4Item;
            RaspunsCorectItem = raspunsCorectItem;
        }
    }
}
