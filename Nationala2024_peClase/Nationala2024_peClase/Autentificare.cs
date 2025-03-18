using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nationala2024_peClase
{
    public partial class Autentificare : Form
    {
        public Autentificare()
        {
            InitializeComponent();
        }

        public static bool retine_email = false;
        public static Utilizator userDeRetinut= new Utilizator("sample@email.com", null, null,"sample", DateTime.Now);

        private void Form1_Load(object sender, EventArgs e)
        {
            Db.Load();
            if (Db.UltimulUtilizator != null)
            {
                textBox1.Text = Db.UltimulUtilizator.Email;
                textBox2.Text = Db.UltimulUtilizator.Parola;
            }
        }

        public static string Criptare(string s)
        {
            string result = "";
            foreach (char c in s)
            {
                if (char.IsDigit(c))
                {
                    result += (c % 10).ToString();
                }
                else
                {
                    if (char.IsUpper(c))
                    {
                        result += Convert.ToChar('A' + (c % 26)).ToString();
                    }
                    else
                    {
                        result += Convert.ToChar('a' + (c % 26)).ToString();
                    }
                }
            }
            Console.WriteLine(result);
            return result;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            userDeRetinut = new Utilizator(textBox1.Text, null, null, textBox2.Text, DateTime.Now);
            string parolaCriptata = Criptare(textBox2.Text);
            var query = from u in Db.Utilizatori
                        where u.Email == textBox1.Text && u.Parola == parolaCriptata
                        select u;
            if(query.Any())
            {
                this.Hide();
                CosmosImagini cosmosImagini = new CosmosImagini();
                cosmosImagini.ShowDialog();
                this.Show();
                if(CosmosImagini.selectatcorect)
                {
                    Db.Save();
                    this.Hide();
                    Cosmos_Calendar cosmos_Calendar = new Cosmos_Calendar();
                    cosmos_Calendar.ShowDialog();
                    this.Show();
                }
            }
            else
            {
                MessageBox.Show("Eroare de autentificare!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CosmonsInregistrare cosmonsInregistrare = new CosmonsInregistrare();
            cosmonsInregistrare.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            retine_email = checkBox1.Checked;
        }
    }
}
