using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Judeteana2016
{
    public partial class Autentificare_client : Form
    {
        public Autentificare_client()
        {
            InitializeComponent();
        }
        public static Client client_Autentificat;
        private void button1_Click(object sender, EventArgs e)
        {
            var Query =
            from client in Form1.clienti
            where (client.email == textBox1.Text && client.parola == textBox2.Text)
            select client;
            if (Query.Any())
            {
                client_Autentificat=Query.First();
                this.Hide();
                Optiuni optiuni = new Optiuni();
                optiuni.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Eroare autentificare!");
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private void Autentificare_client_Load(object sender, EventArgs e)
        {

        }
    }
}
