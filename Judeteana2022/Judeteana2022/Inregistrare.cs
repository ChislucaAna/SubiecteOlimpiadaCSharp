using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judeteana2022
{
    public partial class Inregistrare : Form
    {
        public Inregistrare()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 4)
            {
                MessageBox.Show("Numele e prea scurt");
                return;
            }

            var query = from u in Db.utilizatori
                        where u.NumeUtilizator == textBox1.Text
                        select u;
            if (query.Any())
            {
                MessageBox.Show("Numele nu e unic la nivelul bazei de date");
                return;
            }

            if(textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Parolele nu corespund");
                return;
            }

            if (textBox2.Text.Length<6)
            {
                MessageBox.Show("Parola e prea scurta");
                return;
            }

            if (!textBox4.Text.Contains("@") && textBox4.Text.Contains("."))
            {
                MessageBox.Show("Emailul nu este valid");
                return;
            }

            Db.utilizatori.Add(new Utilizator(Db.utilizatori.Count,textBox1.Text,textBox2.Text,textBox4.Text,DateTime.Now));
            MessageBox.Show("Utilizator adaugat cu succes");
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
