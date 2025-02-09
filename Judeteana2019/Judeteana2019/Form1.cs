using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Judeteana2019
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        StreamReader streamReader;
        string line;

        private void Form1_Load(object sender, EventArgs e)
        {
            streamReader = new StreamReader("utilizatori.txt");
            while((line = streamReader.ReadLine()) != null)
            {
                string[] strings = line.Split('*');
                Db.utilizatori.Add(new Utilizator(strings[0], strings[1], strings[2], strings[3]));
            }
            streamReader.Close();
            streamReader = new StreamReader("carti.txt");
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] strings = line.Split('*');
                Db.carti.Add(new Carte(Db.carti.Count(), strings[0], strings[1], strings[2]));
            }
            streamReader.Close();
            streamReader = new StreamReader("imprumuturi.txt");
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] strings = line.Split('*');
                var queryResults = from c in Db.carti
                                   where c.titlu == strings[0]
                                   select c.id_carte;
                if (queryResults.Any())
                {
                    Db.imprumuturi.Add(new Imprumut(Db.imprumuturi.Count(), queryResults.First(), strings[1], Convert.ToDateTime(strings[2])));
                }
            }
            streamReader.Close();
            foreach (Utilizator i in Db.utilizatori)
            {
                Console.WriteLine(i.ToString());
            }
            foreach (Carte i in Db.carti)
            {
                Console.WriteLine(i.ToString());
            }
            foreach (Imprumut i in Db.imprumuturi)
            {
                Console.WriteLine(i.ToString());    
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CreeazaContFreeBook creeazaContFreeBook = new CreeazaContFreeBook();
            creeazaContFreeBook.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LogareFreeBook creeazaContFreeBook = new LogareFreeBook();
            creeazaContFreeBook.ShowDialog();
        }
    }
}
