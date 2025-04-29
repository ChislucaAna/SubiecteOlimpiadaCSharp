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
    public partial class FrmInregistrare : Form
    {
        public FrmInregistrare()
        {
            InitializeComponent();
        }

        public static bool allowAdmin = true;

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void FrmInregistrare_Load(object sender, EventArgs e)
        {
            if (!allowAdmin)
            {
                comboBox1.Items.Remove("Admin");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var numeQuery = from u in Db.utilizatori
                            where u.NumeUtilizator == textBox1.Text
                            select u;
            var emailQuery = from u in Db.utilizatori
                             where u.Email == textBox4.Text
                             select u;
            if (emailQuery.Any() || numeQuery.Any())
            {
                MessageBox.Show("Utilizatorul există în baza de date!");
            }
            else
            {
                Db.utilizatori.Add(new Utilizator(Db.utilizatori.Count, textBox2.Text, textBox1.Text, textBox4.Text, 0));
                MessageBox.Show("Utilizator adaugat cu succes");
                this.Close();
            }
        }
    }
}
