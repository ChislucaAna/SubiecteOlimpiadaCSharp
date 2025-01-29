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
    public partial class Creare_cont_client : Form
    {
        public Creare_cont_client()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!(textBox6.Text.Contains("@") && textBox6.Text.Contains(".")))
            {
                MessageBox.Show("Email invalid");
                textBox6.Clear();
                return;
            }
            if (!textBox5.Text.Equals(textBox4.Text))
            {
                MessageBox.Show("Parolele nu corespund");
                textBox5.Clear();
                textBox4.Clear();
                return;
            }
            var Query =
            from client in Form1.clienti
            where (client.email == textBox6.Text)
            select client;
            if(Query.Any())
            {
                MessageBox.Show("Email ul exista deja in baza de date");
                textBox6.Clear();
                return;
            }    
            Form1.clienti.Add(new Client(Form1.clienti.Count,textBox4.Text,textBox1.Text,textBox2.Text,textBox3.Text,textBox6.Text));

            StreamWriter streamWriter = new StreamWriter("clienti.txt");
            streamWriter.WriteLine(Form1.clienti.Last().ToString());
            streamWriter.Close();

            MessageBox.Show("Cont creeat cu succes");
            this.Close();
        }
    }
}
