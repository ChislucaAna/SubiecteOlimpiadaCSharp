using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Judeteana2019
{
    public partial class CreeazaContFreeBook : Form
    {
        public CreeazaContFreeBook()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox3.Text.Length == 0
                || textBox4.Text.Length == 0 || textBox5.Text.Length == 0)
            {
                MessageBox.Show("Toate campurile sunt obligatorii!");
                return;
            }

            var queryResults = from c in Db.utilizatori
                               where c.email == textBox1.Text.Trim()
                               select c;
            if (queryResults.Any())
            {
                MessageBox.Show("Email ul nu este unic la nivelul bazei de date!");
                return;
            }

            if(textBox4.Text != textBox5.Text)
            {
                MessageBox.Show("Parolele nu corespund!");
                return;
            }

            Db.utilizatori.Add(new Utilizator(textBox1.Text, textBox4.Text, textBox2.Text, textBox3.Text));
            Db.Refresh();
            MessageBox.Show("Cont creeat cu succes");
        }
    }
}
