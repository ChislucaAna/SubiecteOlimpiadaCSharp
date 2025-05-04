using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nationala2013
{
    public static class Db
    {
        public static List<Utilizator> utilizatori = new List<Utilizator>();
        public static List<Scor> scoruri = new List<Scor>();

        public static void Init()
        {
            try
            {
                StreamReader reader = new StreamReader("Utilizatori.txt");
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(';');
                    utilizatori.Add(new Utilizator(Convert.ToInt32(fields[0]), fields[1]
                        , fields[2], fields[3], fields[4], Convert.ToInt32(fields[5])));
                }
                reader.Close();

                reader = new StreamReader("Scoruri.txt");
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(';');
                    scoruri.Add(new Scor(Convert.ToInt32(fields[0]), Convert.ToInt32(fields[1]),
                        Convert.ToInt32(fields[2]), Convert.ToInt32(fields[3])));
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }        
        }

        public static void Save()
        {

            try
            {
                StreamWriter writer = new StreamWriter("Utilizatori.txt", false);
                using (writer)
                {
                    foreach (Utilizator v in utilizatori)
                    {
                        writer.WriteLine(v.ToString());
                    }
                }
                writer.Close();

                writer = new StreamWriter("Scoruri.txt", false);
                using (writer)
                {
                    foreach (Scor v in scoruri)
                    {
                        writer.WriteLine(v.ToString());
                    }
                }
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
