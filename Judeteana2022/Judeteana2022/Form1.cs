using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Judeteana2022
{
    public partial class Autentificare : Form
    {
        public Autentificare()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Db.Init();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Inregistrare inregistrare = new Inregistrare();
            inregistrare.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var query = from u in Db.utilizatori
                        where u.NumeUtilizator == textBox1.Text && u.Parola == textBox2.Text
                        select u;
            if(query.Any())
            {
                this.Hide();
                Vizualizare v = new Vizualizare();
                v.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show(@"Nume de utilizator si/\r\nsau parola invalida!");
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
