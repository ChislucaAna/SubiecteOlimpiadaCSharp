using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judeteana2019
{
    public class Db
    {
        public static Utilizator utilizatorLogat;
        public static List<Utilizator> utilizatori = new List<Utilizator>();
        public static List<Carte> carti = new List<Carte>();
        public static List<Imprumut> imprumuturi = new List<Imprumut>();
        public static void Refresh()
        {
            using (StreamWriter writer = new StreamWriter("utilizatori.txt", false)) // Overwrite mode
            {
                foreach(Utilizator utilizator in utilizatori)
                {
                    writer.WriteLine(utilizator.ToString());
                }
                writer.Close();
            }
            using (StreamWriter writer = new StreamWriter("carti.txt", false)) // Overwrite mode
            {
                foreach (Carte carte in carti)
                {
                    writer.WriteLine(carte.ToString());
                }
                writer.Close();
            }
            using (StreamWriter writer = new StreamWriter("imprumuturi.txt", false)) // Overwrite mode
            {
                foreach (Imprumut imprumut in imprumuturi)
                {
                    writer.WriteLine(imprumut.ToString());
                }
                writer.Close();
            }
        }
    }
}
