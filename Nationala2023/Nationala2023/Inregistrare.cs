using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nationala2023
{
    public partial class Inregistrare : Form
    {
        public Inregistrare()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(textBox1.Text.Contains("@") && textBox1.Text.Contains(".") && textBox1.Text.Length>4))
            {
                MessageBox.Show("Email invalid");
                return;
            }
            if (!(textBox3.Text == textBox4.Text && textBox3.Text.Length>1))
            {
                MessageBox.Show("Parolele nu corespund sau sunt invalide");
                return;
            }
            IEnumerable<Utilizator> cauta =
            from utilizator in Autentificare.utilizatori
            where (utilizator.email == textBox1.Text)
            select utilizator;
            if (cauta.Any())
            {
                MessageBox.Show("Emailul nu este unic la nivelul bazei de date");
                return;
            }
            Autentificare.utilizatori.Add(new Utilizator(textBox1.Text, textBox2.Text,textBox3.Text));
            MessageBox.Show("Creearea noului cont a fost realizata cu succes");
            this.Close();
        }
    }
}
