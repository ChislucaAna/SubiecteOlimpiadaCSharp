using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Nationala2024_peClase
{
    public partial class CosmonsInregistrare : Form
    {
        public CosmonsInregistrare()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(textBox1.Text.Contains('@') && textBox1.Text.Contains('.')))
            {
                MessageBox.Show("Emailul nu este valid");
                return;
            }
            var query = from u in Db.Utilizatori
                        where u.Email == textBox1.Text
                        select u;
            if (query.Any())
            {
                MessageBox.Show("Emailul nu este unic la nivelul bazei de date");
                return;
            }
            if (DateTime.Now.Subtract(dateTimePicker1.Value).TotalDays < 7 * 365)
            {
                MessageBox.Show("Utilizatorul nu are varsta necesara");
                return;
            }
            if (textBox4.Text != textBox5.Text)
            {
                MessageBox.Show("Parolele nu corespund");
                return;
            }
            if (!(textBox4.Text.Length >= 6 || textBox4.Text.Any(char.IsUpper) && textBox4.Text.Any(char.IsLower) && textBox4.Text.Any(char.IsNumber)))
            {
                MessageBox.Show("Parola nu e destul de securizata");
                return;
            }
            Db.Utilizatori.Add(new Utilizator(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, dateTimePicker1.Value));
        }
    }
}
