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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Db.Load();
            Db.Save();
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
            string parolaCriptata = Criptare(textBox2.Text);
            var query = from u in Db.Utilizatori
                        where u.Email == textBox1.Text && u.Parola == parolaCriptata
                        select u;
            if(query.Any())
            {
                CosmosImagini cosmosImagini = new CosmosImagini();
                cosmosImagini.ShowDialog();
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
    }
}
