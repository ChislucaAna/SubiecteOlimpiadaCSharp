using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nationala2016
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmInregistrare.allowAdmin = false;
            FrmInregistrare frmInregistrare = new FrmInregistrare();
            frmInregistrare.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var rez = from u in Db.utilizatori
                      where u.NumeUtilizator==textBox2.Text && u.Parola == textBox1.Text
                      select u;
            if (rez.Any())
            {
                this.Hide();
                FrmRebus frmRebus = new FrmRebus();
                frmRebus.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Eroare autentificare!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Db.Init();
        }
    }
}
