using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judeteana2022
{
    public class Db
    {
        public static List<Harta> harti = new List<Harta>();
        public static List<Masurare> masurari = new List<Masurare>();
        public static List<Utilizator> utilizatori = new List<Utilizator>();

        public static void Init()
        {
            //harti.txt
            StreamReader reader = new StreamReader("harti.txt");
            string line;

            while((line = reader.ReadLine()) != null)
            {
                string[] fields = line.Split('#');
                harti.Add(new Harta(harti.Count, fields[0], fields[1]));
            }
            reader.Close(); 
            reader = new StreamReader("masurari.txt");

            while ((line = reader.ReadLine()) != null)
            {
                string[] fields = line.Split('#');

                var query = from h in harti
                            where h.NumeHarta == fields[0]
                            select h;
                var harta = query.First();
                DateTime data = DateTime.ParseExact(fields[4], "dd/MM/yyyy HH:mm", null);
                masurari.Add(new Masurare(masurari.Count, harta.IdHarta, Convert.ToInt32(fields[1]), Convert.ToInt32(fields[2]), 
                    Convert.ToDouble(fields[3]), data));
            }

            utilizatori.Add(new Utilizator(utilizatori.Count, "oti2022", "oti2022@oti.com", "oti1234",DateTime.Now)); 

        }
    }
}
