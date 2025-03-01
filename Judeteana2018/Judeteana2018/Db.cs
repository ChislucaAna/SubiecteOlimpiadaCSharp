using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judeteana2018
{
    public class Db
    {
        public static List<Utilizator> utilizatori=new List<Utilizator>(); 
        public static List<Item> itemi=new List<Item>();
        public static List<Evaluare> evaluari=new List<Evaluare>();

        public static void Load()
        {
            StreamReader streamReader = new StreamReader("date.txt");
            string line;
            int index =0 ;
            while ((line = streamReader.ReadLine()) != null)
            {
                if(!line.Contains(';'))
                {
                    index++;
                }
                else
                {
                    string[] fields = line.Split(';');
                    switch (index)
                    {
                        case 1:
                            utilizatori.Add(new Utilizator(utilizatori.Count, fields[0], fields[1], fields[2], fields[3]));
                            break;
                        case 2:
                            itemi.Add(new Item(itemi.Count, Convert.ToInt32(fields[0]), fields[1], fields[2], fields[3], fields[4], fields[5], fields[6]));
                            break;
                        case 3:
                            Console.WriteLine(fields[1]);
                            DateTime dt = DateTime.ParseExact(fields[1], "M/d/yyyy hh:mm:ss tt",null);
                            evaluari.Add(new Evaluare(evaluari.Count, Convert.ToInt32(fields[0]), dt, Convert.ToInt32(fields[2])));
                            break;
                    }
                }
            }
        }
    }
}
