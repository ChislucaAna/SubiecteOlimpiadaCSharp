using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Nationala2024_peClase
{
    public class Db
    {

        public static List<Utilizator> Utilizatori = new List<Utilizator>();
        public static List<Inregistrare> Inregistrari = new List<Inregistrare>();       

        public static void Load()
        {
            StreamReader sr = new StreamReader("Utilizatori.txt");
            string line;
            while((line = sr.ReadLine()) != null)
            {
                string[] fields = line.Split(';');

                //12.21.1995
                DateTime dt = DateTime.ParseExact(fields[4], "MM/dd/yyyy",null);
                Utilizatori.Add(new Utilizator(fields[0], fields[1], fields[2], fields[3],dt));
            }
            sr.Close();

            sr = new StreamReader("Inregistrari.txt");
            //22/06/2024
            while ((line = sr.ReadLine()) != null)
            {
                string[] fields = line.Split(';');

                DateTime dt = DateTime.ParseExact(fields[2], "MM/dd/yyyy",null);
                Inregistrari.Add(new Inregistrare(Inregistrari.Count(), fields[0], dt, Convert.ToInt32(fields[1]), Convert.ToInt32(fields[3])));
            }
            sr.Close();
        }

        public static void Save()
        {
            using (StreamWriter sw = new StreamWriter("Utilizatori.txt",false))
            {
                foreach (Utilizator i in Utilizatori)
                { sw.WriteLine(i.ToString()); }
                sw.Close();
            }
            using (StreamWriter sw = new StreamWriter("Inregistrari.txt",false))
            {
                foreach (Inregistrare i in Inregistrari)
                { sw.WriteLine(i.ToString()); }
                sw.Close();
            }
        }
    }
}
