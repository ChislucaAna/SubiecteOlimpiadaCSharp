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

namespace Judeteana2016
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static List<Client> clienti = new List<Client>();
        public static List<Produs> meniu = new List<Produs>();
        public static List<Comanda> comenzi = new List<Comanda>();
        public static List<Subcomanda> subcomenzi = new List<Subcomanda>();
        private void button1_Click(object sender, EventArgs e)
        {
            Creare_cont_client creare_Cont_Client = new Creare_cont_client();
            creare_Cont_Client.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string line;
            StreamReader streamReader = new StreamReader("clienti.txt");
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] bucati = line.Split(';');
                clienti.Add(new Client(Convert.ToInt32(bucati[0]), bucati[1], bucati[2], bucati[3], bucati[4], bucati[5]));
            }
            streamReader.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Autentificare_client autentificare_Client = new Autentificare_client();
            autentificare_Client.Show();
        }
    }
}
