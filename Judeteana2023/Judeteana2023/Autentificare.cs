using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judeteana2023
{
    public partial class Autentificare : Form
    {
        public Autentificare()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Db.GetData();
            Db.SaveData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var utilizator = from u in Db.utilizatori
                             where u.EmailUtilizator==textBox1.Text && u.Parola==textBox2.Text    
                             select u;
            if (utilizator.Any())
            {
                Db.utilizatorLogat = utilizator.ToList().First();
            }
            if(Db.utilizatorLogat!=null)
            {
                this.Hide();
                AlegeJoc alegeJoc = new AlegeJoc();
                alegeJoc.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Date de autentificare invalide!");
                textBox1.Clear();
                textBox2.Clear();
            }
        }
    }
}
