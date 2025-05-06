using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nationala2013
{
    public static class Db
    {
        public static BindingList<Utilizator> utilizatori { get; set; }
        public static List<Scor> scoruri { get; set; }

        public static void Init()
        {
            try
            {
                utilizatori = new BindingList<Utilizator>();
                utilizatori.AllowNew = true; //!
                utilizatori.AllowRemove = true; 

                scoruri = new List<Scor>();

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
            using (StreamWriter writer = new StreamWriter("Utilizatori.txt", false))
            {
                foreach (Utilizator v in utilizatori)
                {
                    writer.WriteLine(v.ToString());
                }
            }

            using (StreamWriter writer = new StreamWriter("Scoruri.txt", false))
            {
                foreach (Scor s in scoruri)
                {
                    writer.WriteLine(s.ToString());
                }
            }
        }
    }
}
