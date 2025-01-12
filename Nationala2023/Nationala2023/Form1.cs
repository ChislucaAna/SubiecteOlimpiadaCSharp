using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nationala2023
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static List<Utilizator> utilizatori = new List<Utilizator>();
        public static List<Rezultat> rezultate = new List<Rezultat>();

        private void Form1_Load(object sender, EventArgs e)
        {
            string line;
            StreamReader streamReader = new StreamReader("Utilizatori.txt");
            while ((line = streamReader.ReadLine())!=null)
            {
                string[] strings = line.Split(';');
                utilizatori.Add(new Utilizator(strings[0], strings[1], strings[2]));
            }
            streamReader = new StreamReader("Rezultate.txt");
            while ((line = streamReader.ReadLine())!=null)
            {
                string[] strings = line.Split(';');
                DateTime dt = DateTime.ParseExact(strings[3], "dd.mm.yyyy", null);
                rezultate.Add(new Rezultat(Convert.ToInt16(strings[0]), strings[1], Convert.ToInt16(strings[2]),dt));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IEnumerable<Utilizator> cauta =
            from utilizator in utilizatori
            where (utilizator.email == textBox1.Text && utilizator.parola == textBox2.Text)
            select utilizator;
            if (cauta.Any())
            {
                AlegeJoc alegeJoc = new AlegeJoc();
                alegeJoc.ShowDialog();
            }

        }
    }
}
