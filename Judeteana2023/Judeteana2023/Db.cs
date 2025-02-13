using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judeteana2023
{
    public class Db
    {
        public static List<Utilizator> utilizatori = new List<Utilizator>();
        public static List<Rezultat> rezultate = new List<Rezultat>();
        public static List<Item> itemi = new List<Item>();
        public static Utilizator utilizatorLogat= new Utilizator("popescu.petre@ojti2023.ro", "otipopescu", "abc123@A");
        //popescu.petre@ojti2023.ro;otipopescu;abc123@A

        public static string GetNumeFromEmail(string email)
        {
            var uwu = from u in utilizatori
                      where u.EmailUtilizator == email
                      select u.NumeUtilizator;
            return uwu.First().ToString();  
        }

        public static void GetData()
        {
            utilizatori.Clear();
            rezultate.Clear();  
            itemi.Clear();  

            StreamReader streamReader = new StreamReader("Utilizatori.txt");
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] fields = line.Split(';');
                utilizatori.Add(new Utilizator(fields[0], fields[1], fields[2]));
            }
            streamReader.Close();

            streamReader = new StreamReader("Rezultate.txt");
            int index = 1;
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] fields = line.Split(';');
                rezultate.Add(new Rezultat(index++, Convert.ToInt16(fields[1]), fields[2], Convert.ToInt16(fields[3])));
            }
            streamReader.Close();

            index = 1;
            streamReader = new StreamReader("Itemi.txt");
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] fields = line.Split(';');
                itemi.Add(new Item(index++, fields[1], fields[2], fields[3], fields[4], fields[5], Convert.ToInt16(fields[6])));
            }
            streamReader.Close();

        }

        public static void SaveData()
        {
            using(StreamWriter writer = new StreamWriter("Utilizatori.txt",false))
            {
                foreach(Utilizator utilizator in utilizatori)
                {
                    writer.WriteLine(utilizator.ToString());    
                }
            }
            using (StreamWriter writer = new StreamWriter("Rezultate.txt", false))
            {
                foreach (Rezultat rez in rezultate)
                {
                    writer.WriteLine(rez.ToString());
                }
            }
            using (StreamWriter writer = new StreamWriter("Itemi.txt", false))
            {
                foreach (Item i in itemi)
                {
                    writer.WriteLine(i.ToString());
                }
            }
        }
    }
}
