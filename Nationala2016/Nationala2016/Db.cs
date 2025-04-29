using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nationala2016
{
    public static class Db
    {
        public static List<Utilizator> utilizatori = new List<Utilizator>();   
        public static List<Rebus> rebusuri = new List<Rebus>();
        public static List<Rezolvare> rezolvari = new List<Rezolvare>();
        public static List<Statistica > statistici = new List<Statistica>();

        public static void Init()
        {
            utilizatori.Add(new Utilizator(0, "admin", "1234", "oti2016@bacau.ro", 1));
            rebusuri.Add(new Rebus(1, "dorinte", 10, 10, 2450));
            rebusuri.Add(new Rebus(2, "scriitori", 10, 8, 1200));
            rebusuri.Add(new Rebus(3, "sport", 11, 11, 3245));
        }
    }
}
